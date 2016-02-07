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
            var directoryToSave = packageId % PackageConstants.PackagesPerDirectory;

            var directoryToSavePath = Path.Combine(this.rootPath, directoryToSave.ToString());
            if (!Directory.Exists(directoryToSavePath))
            {
                Directory.CreateDirectory(directoryToSavePath);
            }
       
            var packageDirectoryPath = Path.Combine(directoryToSavePath, Path.GetFileNameWithoutExtension(packageName));
            if (!Directory.Exists(packageDirectoryPath))
            {
                Directory.CreateDirectory(packageDirectoryPath);
            }

            var screenshotsDirPath = Path.Combine(packageDirectoryPath, this.screenshotsFolderName);
            if (!Directory.Exists(screenshotsDirPath))
            {
                Directory.CreateDirectory(screenshotsDirPath);
            }

            string finalPath = Path.Combine(screenshotsDirPath, screenshotFilename);
            File.WriteAllBytes(finalPath, contents);
        }
    }
}
