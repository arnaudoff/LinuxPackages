namespace LinuxPackages.Services.Data.Contracts
{
    using System.Linq;
    using LinuxPackages.Data.Models;

    public interface IDistributionsService
    {
        IQueryable<Distribution> GetAll();

        IQueryable<Distribution> GetById(int id);

        Distribution Create(string name, string maintainer, string url);
    }
}