namespace LinuxPackages.Services.Data
{
    using System;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;
    using LinuxPackages.Services.Data.Contracts;
    using System.IO;

    public class ScreenshotsService : IScreenshotsService
    {
        private readonly IRepository<Screenshot> screenshots;
        private readonly IScreenshotSaver screenshotSaver;

        public ScreenshotsService(IRepository<Screenshot> screenshots, IScreenshotSaver screenshotSaver)
        {
            this.screenshots = screenshots;
            this.screenshotSaver = screenshotSaver;
        }

        public Screenshot Create(string fileName, byte[] contents, int packageId, string packageName)
        {
            var newScreenshot = new Screenshot
            {
                FileName = Path.GetFileNameWithoutExtension(fileName),
                FileExtension = Path.GetExtension(fileName),
                Size = (uint)contents.Length,
                PackageId = packageId
            };

            this.screenshots.Add(newScreenshot);
            this.screenshots.SaveChanges();
            this.screenshotSaver.Save(packageId, packageName, fileName, contents);

            return newScreenshot;
        }
    }
}
