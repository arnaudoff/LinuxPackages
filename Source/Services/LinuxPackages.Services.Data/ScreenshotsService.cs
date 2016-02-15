namespace LinuxPackages.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;
    using LinuxPackages.Services.Data.Contracts;
    using Contracts.Savers;

    public class ScreenshotsService : IScreenshotsService
    {
        private readonly IRepository<Screenshot> screenshots;
        private readonly IScreenshotSaver screenshotSaver;

        public ScreenshotsService(IRepository<Screenshot> screenshots, IScreenshotSaver screenshotSaver)
        {
            this.screenshots = screenshots;
            this.screenshotSaver = screenshotSaver;
        }

        public IQueryable<Screenshot> GetById(int id)
        {
            return this.screenshots.All().Where(s => s.Id == id);
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
