namespace LinuxPackages.Services.Data.Contracts
{
    using LinuxPackages.Data.Models;

    public interface IScreenshotsService
    {
        Screenshot Create(string fileName, byte[] contents, string fileExtension, int packageId, string packageName);
    }
}