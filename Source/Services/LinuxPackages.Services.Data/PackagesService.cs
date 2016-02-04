namespace LinuxPackages.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;
    using System.IO;

    public class PackagesService : IPackagesService
    {
        private readonly IRepository<Package> packages;
        private readonly IPackageSaver packageSaver;

        public PackagesService(IRepository<Package> packages, IPackageSaver packageSaver)
        {
            this.packages = packages;
            this.packageSaver = packageSaver;
        }

        public Package Create(
            string name,
            string architecture,
            string description,
            string license,
            string fileName,
            byte[] contents,
            ICollection<Package> dependencies,
            ICollection<User> maintainers,
            ICollection<Screenshot> screenshots)
        {
            var newPackage = new Package
            {
                Name = name,
                Architecture = architecture,
                Description = description,
                License = license,
                FileName = fileName,
                Size = (uint)contents.Length,
                UploadedOn = DateTime.UtcNow,
                Dependencies = dependencies,
                Maintainers = maintainers,
                Screenshot = screenshots
            };

            this.packages.Add(newPackage);
            this.packages.SaveChanges();
            this.packageSaver.Save(newPackage.Id, fileName, contents);

            return newPackage;
        }

        public IQueryable<Package> GetById(int id)
        {
            return this.packages
                .All()
                .Where(p => p.Id == id);
        }
    }
}