namespace LinuxPackages.Services.Data
{
    using System.Linq;

    using Contracts;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;

    public class PackagesService : IPackagesService
    {
        private readonly IRepository<Package> packages;

        public PackagesService(IRepository<Package> packages)
        {
            this.packages = packages;
        }

        public IQueryable<Package> GetById(int id)
        {
            return this.packages
                .All()
                .Where(p => p.Id == id);
        }
    }
}