namespace LinuxPackages.Services.Data.Contracts
{
    using System.Linq;
    using LinuxPackages.Data.Models;

    public interface ILicensesService
    {
        IQueryable<License> GetAll();

        IQueryable<License> GetById(int id);

        License Create(string name, string description, string url);
    }
}