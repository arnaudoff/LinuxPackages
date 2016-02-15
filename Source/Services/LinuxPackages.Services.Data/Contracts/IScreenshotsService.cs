namespace LinuxPackages.Services.Data.Contracts
{
    using System.Linq;
    using LinuxPackages.Data.Models;

    public interface IScreenshotsService
    {
        IQueryable<Screenshot> GetById(int id);

        Screenshot Create(string fileName, byte[] contents, int packageId, string packageName);

        string GetFileNameById(int id);
    }
}