namespace LinuxPackages.Services.Data.Contracts
{
    using LinuxPackages.Data.Models;
    using System.Linq;

    public interface IDistributionsService
    {
        IQueryable<Distribution> GetAll();

        IQueryable<Distribution> GetById(int id);

        Distribution Create(string name, string maintainer, string url);
    }
}