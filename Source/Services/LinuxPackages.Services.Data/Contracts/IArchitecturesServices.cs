namespace LinuxPackages.Services.Data.Contracts
{
    using LinuxPackages.Data.Models;
    using System.Linq;

    public interface IArchitecturesServices
    {
        IQueryable<Architecture> GetAll();

        IQueryable<Architecture> GetById(int id);

        Architecture Create(string name);
    }
}