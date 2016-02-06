namespace LinuxPackages.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;

    public class PackagesService : IPackagesService
    {
        private readonly IRepository<Package> packages;
        private readonly IRepository<User> users;
        private readonly IPackageSaver packageSaver;
        private readonly IRepository<Dependency> dependencies;

        public PackagesService(IRepository<Package> packages, IRepository<User> users, IRepository<Dependency> dependencies, IPackageSaver packageSaver)
        {
            this.packages = packages;
            this.users = users;
            this.packageSaver = packageSaver;
            this.dependencies = dependencies;
        }

        public IQueryable<Package> GetAll()
        {
            return this.packages.All();
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

            if (maintainerIds != null && maintainerIds.Count > 0)
            {
                foreach (string maintainerId in maintainerIds)
                {
                    var maintainer = new User() { Id = maintainerId };
                    users.Attach(maintainer);
                    newPackage.Maintainers.Add(maintainer);
                }
            }

            this.packages.Add(newPackage);
            this.packages.SaveChanges();
            this.packageSaver.Save(newPackage.Id, newPackage.Name, fileName, contents);

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
    }
}