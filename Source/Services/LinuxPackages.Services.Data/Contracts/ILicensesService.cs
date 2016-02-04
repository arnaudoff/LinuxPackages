namespace LinuxPackages.Services.Data.Contracts
{
    using LinuxPackages.Data.Models;
    using System.Linq;

    public interface ILicensesService
    {
        IQueryable<License> GetAll();

        IQueryable<License> GetById(int id);

        License Create(string name, string description, string url);
    }
}