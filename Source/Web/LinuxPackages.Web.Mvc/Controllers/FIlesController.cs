namespace LinuxPackages.Web.Mvc.Controllers
{
    using Common.Constants;
    using Infrastructure.Helpers;
    using Services.Data;
    using Services.Data.Contracts;
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class FilesController : Controller
    {
        private readonly IPackagesService packages;
        private readonly IPackageSaver packageSaver;
        private readonly IScreenshotSaver screenshotSaver;

        public FilesController(IPackagesService packages, IPackageSaver packageSaver, IScreenshotSaver screenshotSaver)
        {
            this.packages = packages;
            this.packageSaver = packageSaver;
            this.screenshotSaver = screenshotSaver;
        }

        public ActionResult Screenshots(string id, string resource)
        {
            if (!QueryStringUrlHelper.IsHashValid(id))
            {
                return new HttpNotFoundResult("The requested package was not found.");
            }

            if (!QueryStringUrlHelper.IsHashValid(resource))
            {
                return new HttpNotFoundResult("The requested screenshot was not found.");
            }

            int requestedPackageId = int.Parse(QueryStringUrlHelper.GetEntityIdFromUrlHash(id));
            int requestedScreenshotId = int.Parse(QueryStringUrlHelper.GetEntityIdFromUrlHash(resource));

            var package = this.packages
                .GetById(requestedPackageId)
                .Select(p => new
                {
                    Name = p.Name,
                    Screenshot = p.Screenshots.Where(s => s.Id == requestedScreenshotId).FirstOrDefault()
                })
                .FirstOrDefault();

            string fullFilename = string.Format("{0}{1}", package.Screenshot.FileName, package.Screenshot.FileExtension);

            byte[] contents = this.screenshotSaver.Read(requestedPackageId, package.Name, fullFilename);
            string mimeType = MimeMapping.GetMimeMapping(fullFilename);

            return new FileContentResult(contents, mimeType);
        }
        
        public ActionResult Packages(string id)
        {
            if (!QueryStringUrlHelper.IsHashValid(id))
            {
                return new HttpNotFoundResult("The requested package was not found.");
            }

            int requestedPackageId = int.Parse(QueryStringUrlHelper.GetEntityIdFromUrlHash(id));

            var package = this.packages
                .GetById(requestedPackageId)
                .Select(p => new
                {
                    Name = p.Name,
                    FileName = p.FileName
                })
                .FirstOrDefault();

            byte[] contents = this.packageSaver.Read(requestedPackageId, package.Name, package.FileName);
            string mimeType = MimeMapping.GetMimeMapping(package.FileName);

            return new FileContentResult(contents, mimeType);
        }
    }
}