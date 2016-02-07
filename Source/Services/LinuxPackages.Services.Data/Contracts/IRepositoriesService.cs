namespace LinuxPackages.Services.Data.Contracts
{
    using LinuxPackages.Data.Models;
    using System.Linq;

    public interface IRepositoriesService
    {
        IQueryable<Repository> GetAll();

        IQueryable<Repository> GetById(int id);

        Repository Create(string name, string url);
    }
}