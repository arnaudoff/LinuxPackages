namespace LinuxPackages.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;
    using System.IO;
    using Common.Utilities;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class PackagesService : IPackagesService
    {
        private readonly IRepository<Package> packages;
        private readonly IPackageSaver packageSaver;

        public PackagesService(IRepository<Package> packages, IPackageSaver packageSaver)
        {
            this.packages = packages;
            this.packageSaver = packageSaver;
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
            IList<User> maintainers)
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
                Maintainers = maintainers,
                Size = (uint)contents.Length,
                UploadedOn = DateTime.UtcNow
            };

            if (dependencyIds != null)
            {
                foreach (var dependencyId in dependencyIds)
                {
                    var currentDependency = packages.GetById(dependencyId);
                    newPackage.Dependencies.Add(currentDependency);
                }
            }

            this.packages.Add(newPackage);
            this.packages.SaveChanges();
            this.packageSaver.Save(newPackage.Id, newPackage.Name, fileName, contents);

            return newPackage;
        }
    }
}