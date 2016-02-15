namespace LinuxPackages.Services.Data
{
    using System;
    using System.Linq;

    using LinuxPackages.Data.Models;
    using LinuxPackages.Services.Data.Contracts;
    using LinuxPackages.Data.Repositories;
    using System.Collections.Generic;

    public class DependenciesService : IDependenciesService
    {
        private readonly IRepository<Dependency> dependencies;
        private readonly IPackagesService packages;

        public DependenciesService(IRepository<Dependency> dependencies, IPackagesService packages)
        {
            this.dependencies = dependencies;
            this.packages = packages;
        }

        public IQueryable<Package> GetAllById(int packageId)
        {
            // TODO: Try to optimize this query
            var depIds = this.dependencies
                .All()
                .Where(d => d.PackageId == packageId)
                .Select(d => d.DependsOnId)
                .ToList();

            return packages.GetAll().Where(p => depIds.Contains(p.Id));
        }
    }
}
