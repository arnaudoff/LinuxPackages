namespace LinuxPackages.Services.Data.Contracts
{
    using System.Linq;
    using LinuxPackages.Data.Models;
    using System.Collections.Generic;

    public interface IPackagesService
    {
        IQueryable<Package> GetAll();

        IQueryable<Package> GetById(int id);

        Package Create(
            string name,
            string description,
            int repositoryId,
            int architectureId,
            int licenseId,
            string fileName,
            byte[] contents,
            ICollection<Package> dependencies,
            ICollection<User> maintainers,
            ICollection<Screenshot> screenshots);
    }
}