namespace LinuxPackages.Services.Data.Contracts
{
    using System.Linq;
    using LinuxPackages.Data.Models;

    public interface IPackagesService
    {
        IQueryable<Package> GetById(int id);
    }
}