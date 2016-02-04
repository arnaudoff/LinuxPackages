namespace LinuxPackages.Services.Data.Contracts
{
    using System.Linq;
    using LinuxPackages.Data.Models;
    using System.Collections.Generic;

    public interface IPackagesService
    {
        IQueryable<Package> GetById(int id);

        Package Create(
            string name,
            string architecture,
            string description,
            string license,
            string fileName,
            byte[] contents,
            ICollection<Package> dependencies,
            ICollection<User> maintainers,
            ICollection<Screenshot> screenshots);
    }
}