namespace LinuxPackages.Services.Data.Contracts
{
    using Common.Constants;
    using System.IO;

    public class HardDriveScreenshotSaver : IScreenshotSaver
    {
        private readonly string rootPath;
        private readonly string screenshotsFolderName;

        public HardDriveScreenshotSaver(string rootPath, string screenshotsFolderName)
        {
            this.rootPath = rootPath;
            this.screenshotsFolderName = screenshotsFolderName;
        }

        public void Save(int packageId, string packageName, string screenshotFilename, byte[] contents)
        {
            var packageDirectory = packageId % PackageConstants.PackagesPerDirectory;

            string finalPath = Path.Combine(
                                        this.rootPath,
                                        packageDirectory.ToString(),
                                        Path.GetFileNameWithoutExtension(packageName),
                                        this.screenshotsFolderName,
                                        screenshotFilename);

            File.WriteAllBytes(finalPath, contents);
        }
    }
}
