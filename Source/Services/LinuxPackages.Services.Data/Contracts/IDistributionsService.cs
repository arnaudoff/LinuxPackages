namespace LinuxPackages.Services.Data.Contracts
{
    using System.Linq;
    using LinuxPackages.Data.Models;

    public interface IDistributionsService
    {
        IQueryable<Distribution> GetAll();

        IQueryable<Distribution> GetById(int id);

        Distribution Create(string name, string version, string maintainer, string url);

        void Update(int distroId, string name, string version, string maintainer, string url);

        void DeleteById(int distroId);
    }
}