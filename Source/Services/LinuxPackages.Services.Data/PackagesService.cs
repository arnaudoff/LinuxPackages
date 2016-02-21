namespace LinuxPackages.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common.Utilities;
    using Contracts;
    using Contracts.Savers;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;

    public class PackagesService : IPackagesService
    {
        private readonly IRepository<Package> packages;
        private readonly IRepository<User> users;
        private readonly IPackageSaver packageSaver;
        private readonly IRepository<Dependency> dependencies;
        private readonly IRepository<PackageComment> comments;
        private readonly IRepository<Rating> ratings;

        public PackagesService(
            IRepository<Package> packages,
            IRepository<User> users,
            IRepository<Dependency> dependencies,
            IRepository<PackageComment> comments,
            IRepository<Rating> ratings,
            IPackageSaver packageSaver)
        {
            this.packages = packages;
            this.users = users;
            this.dependencies = dependencies;
            this.packageSaver = packageSaver;
            this.comments = comments;
            this.ratings = ratings;
        }

        public IQueryable<Package> GetAll()
        {
            return this.packages.All();
        }

        public IQueryable<Package> GetMostDownloaded(int n)
        {
            return this.packages
                .All()
                .OrderByDescending(p => p.Downloads)
                .Take(n);
        }

        public IQueryable<Package> GetLatest(int n)
        {
            return this.packages
                .All()
                .OrderByDescending(p => p.UploadedOn)
                .ThenBy(p => p.Id)
                .Take(n);
        }

        public IQueryable<Package> GetById(int id)
        {
            return this.packages
                .All()
                .Where(p => p.Id == id);
        }

        public Package Create(
            string name,
            string description,
            int distributionId,
            int repositoryId,
            int architectureId,
            int licenseId,
            string fileName,
            byte[] contents,
            IList<int> dependencyIds,
            IList<string> maintainerIds)
        {
            fileName = PathUtils.CleanFileName(fileName);

            var newPackage = new Package
            {
                Name = name,
                Description = description,
                DistributionId = distributionId,
                RepositoryId = repositoryId,
                ArchitectureId = architectureId,
                LicenseId = licenseId,
                FileName = fileName,
                Size = contents.Length,
                UploadedOn = DateTime.UtcNow
            };

            var maintainers = this.users
                .All()
                .Where(x => maintainerIds.Contains(x.Id))
                .ToList();

            if (maintainerIds != null && maintainerIds.Count > 0)
            {
                for (int i = 0; i < maintainers.Count; i++)
                {
                    newPackage.Maintainers.Add(maintainers[i]);
                }
            }

            this.packages.Add(newPackage);
            this.packages.SaveChanges();
            this.packageSaver.Save(newPackage.Id, fileName, contents);

            if (dependencyIds != null && dependencyIds.Count > 0)
            {
                foreach (int dependencyId in dependencyIds)
                {
                    var newDependency = new Dependency()
                    {
                        PackageId = newPackage.Id,
                        DependsOnId = dependencyId
                    };

                    this.dependencies.Add(newDependency);
                }
            }

            this.dependencies.SaveChanges();

            return newPackage;
        }

        public void Update(int packageId, string name, int distributionId, int repositoryId, int architectureId, int licenseId)
        {
            var package = this.packages.GetById(packageId);
            package.Name = name;
            package.DistributionId = distributionId;
            package.RepositoryId = repositoryId;
            package.ArchitectureId = architectureId;
            package.LicenseId = licenseId;

            this.packages.SaveChanges();
        }

        public void DeleteById(int packageId)
        {
            this.packages.Delete(packageId);
            this.packages.SaveChanges();
        }

        public PackageComment AddComment(string content, int packageId, string authorId)
        {
            var newComment = new PackageComment()
            {
                Content = content,
                PackageId = packageId,
                AuthorId = authorId,
                CreatedOn = DateTime.UtcNow
            };

            this.comments.Add(newComment);
            this.comments.SaveChanges();

            return newComment;
        }

        public IQueryable<PackageComment> GetCommentsByPackageId(int packageId)
        {
            return this.comments
                .All()
                .Where(c => c.PackageId == packageId);
        }

        public IQueryable<PackageComment> GetLatestComments(int n)
        {
            return this.comments
                .All()
                .OrderByDescending(c => c.CreatedOn)
                .ThenBy(c => c.Id)
                .Take(n);
        }

        public Rating AddRating(int value, int packageId, string ratedById)
        {
            var currentRating = this.ratings
                .All()
                .Where(r => r.PackageId == packageId && r.RatedById == ratedById)
                .FirstOrDefault();

            if (currentRating != null)
            {
                currentRating.Value = value;

                this.ratings.Update(currentRating);
                this.ratings.SaveChanges();

                return currentRating;
            }

            var newRating = new Rating()
            {
                Value = value,
                PackageId = packageId,
                RatedById = ratedById
            };

            this.ratings.Add(newRating);
            this.ratings.SaveChanges();

            return newRating;
        }

        public void IncrementDownloads(int packageId)
        {
            var package = this.packages.GetById(packageId);

            package.Downloads += 1;

            this.packages.Update(package);
            this.packages.SaveChanges();
        }

        public IDictionary<int, int> GetLastMonthUploadDayDistribution()
        {
            DateTime distributionSince = DateTime.Now.AddMonths(-1);
            var result = this.packages
                .All()
                .Where(p => p.UploadedOn > distributionSince)
                .GroupBy(p => p.UploadedOn.Day)
                .ToDictionary(g => g.Key, g => g.Count());

            for (int i = 1; i < DateTime.UtcNow.Day; i++)
            {
                if (!result.Keys.Contains(i))
                {
                    result.Add(i, 0);
                }
            }

            return result;
        }
    }
}