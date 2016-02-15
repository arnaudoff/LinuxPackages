namespace LinuxPackages.Services.Data.Contracts.Savers
{
    public interface IScreenshotSaver
    {
        void Save(int packageId, string packageName, string fileName, byte[] contents);

        byte[] Read(int requestedPackageId, int requestedScreenshotId, int? width = null, int? height = null);
    }
}