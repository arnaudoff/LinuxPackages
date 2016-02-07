namespace LinuxPackages.Services.Data.Contracts
{
    using LinuxPackages.Data.Models;

    public interface IScreenshotsService
    {
        Screenshot Create(string fileName, byte[] contents, int packageId, string packageName);
    }
}