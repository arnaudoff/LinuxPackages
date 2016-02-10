namespace LinuxPackages.Services.Data.Contracts
{
    using System;
    using System.IO;
    using System.Drawing;
    using System.Drawing.Imaging;

    using Common.Constants;
    using Common.Utilities;
    using System.Linq;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;

    public class HardDriveScreenshotSaver : IScreenshotSaver
    {
        private readonly string rootPath;
        private readonly string screenshotsFolderName;
        private readonly IRepository<Screenshot> screenshots;

        public HardDriveScreenshotSaver(string rootPath, string screenshotsFolderName, IRepository<Screenshot> screenshots)
        {
            this.rootPath = rootPath;
            this.screenshotsFolderName = screenshotsFolderName;
            this.screenshots = screenshots;
        }

        public byte[] Read(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        public void Save(int packageId, string packageName, string screenshotFilename, byte[] contents)
        {
            // Save original screenshot
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

            // Save the thumbnail
            var thumbnailsFolderPath = Path.Combine(
                            screenshotsDirPath,
                            string.Format("{0}x{1}", PackageConstants.ScreenshotThumbnailWidth, PackageConstants.ScreenshotThumbnailHeight));

            if (!Directory.Exists(thumbnailsFolderPath))
            {
                Directory.CreateDirectory(thumbnailsFolderPath);
            }

            using (var memoryStream = new MemoryStream(contents))
            {
                using (var image = Image.FromStream(memoryStream))
                {
                    using (var newImage = ImageUtils.ScaleImage(image, 500, 500))
                    {
                        newImage.Save(Path.Combine(thumbnailsFolderPath, screenshotFilename), ImageFormat.Png);
                    }
                }
            }
        }

        public string GetScreenshotPath(int requestedPackageId, int requestedScreenshotId)
        {
            var screenshot = this.screenshots
                .All()
                .Where(s => s.Id == requestedScreenshotId)
                .Select(s => new
                {
                    FileName = s.FileName,
                    FileExtension = s.FileExtension,
                    PackageName = s.Package.Name
                })
                .FirstOrDefault();

            var filePath = Path.Combine(
                this.rootPath,
                (requestedPackageId % PackageConstants.PackagesPerDirectory).ToString(),
                screenshot.PackageName,
                this.screenshotsFolderName,
                string.Format("{0}{1}", screenshot.FileName, screenshot.FileExtension));

            return filePath;
        }

        public string GetScreenshotPath(int requestedPackageId, int requestedScreenshotId, int width, int height)
        {
            var screenshot = this.screenshots
                .All()
                .Where(s => s.Id == requestedScreenshotId)
                .Select(s => new
                {
                    FileName = s.FileName,
                    FileExtension = s.FileExtension,
                    PackageName = s.Package.Name
                })
                .FirstOrDefault();

            var filePath = Path.Combine(
                this.rootPath,
                (requestedPackageId % PackageConstants.PackagesPerDirectory).ToString(),
                screenshot.PackageName,
                this.screenshotsFolderName,
                string.Format("{0}x{1}", width, height),
                string.Format("{0}{1}", screenshot.FileName, screenshot.FileExtension));

            return filePath;
        }
    }
}
