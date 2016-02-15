namespace LinuxPackages.Services.Data.Contracts.Savers
{
    public interface IScreenshotSaver
    {
        void Save(int packageId, string packageName, string fileName, byte[] contents);

        byte[] Read(string filePath);

        string GetScreenshotPath(int requestedPackageId, int requestedScreenshotId);

        string GetScreenshotPath(int requestedPackageId, int requestedScreenshotId, int width, int height);
    }
}