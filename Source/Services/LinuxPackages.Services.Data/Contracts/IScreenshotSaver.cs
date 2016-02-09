namespace LinuxPackages.Services.Data
{
    public interface IScreenshotSaver
    {
        void Save(int packageId, string packageName, string fileName, byte[] contents);

        byte[] Read(int packageId, string packageName, string fileName);
    }
}