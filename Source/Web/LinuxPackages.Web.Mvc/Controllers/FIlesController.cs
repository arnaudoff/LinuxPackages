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
        private readonly IScreenshotSaver screenshotSaver;

        public FilesController(IPackagesService packages, IScreenshotSaver screenshotSaver)
        {
            this.packages = packages;
            this.screenshotSaver = screenshotSaver;
        }

        public ActionResult Screenshots(string id, string resource)
        {
            string packageHash = id.Substring(Math.Max(0, id.Length - GlobalConstants.UrlHashLength));
            string parsedPackageId = id.Substring(0, Math.Max(0, id.Length - GlobalConstants.UrlHashLength));

            string packageIdHashed = 
                QueryStringUrlHelper.GenerateUrlHash(parsedPackageId, (string)this.ControllerContext.HttpContext.Application[GlobalConstants.UrlSaltKeyName]);

            if (packageIdHashed != packageHash)
            {
                return new HttpNotFoundResult("The requested package was not found.");
            }

            string screenshotHash = resource.Substring(Math.Max(0, resource.Length - GlobalConstants.UrlHashLength));
            string parsedScreenshotId = resource.Substring(0, Math.Max(0, resource.Length - GlobalConstants.UrlHashLength));

            string screenshotIdHashed = 
                QueryStringUrlHelper.GenerateUrlHash(parsedScreenshotId,(string)this.ControllerContext.HttpContext.Application[GlobalConstants.UrlSaltKeyName]);

            if (screenshotIdHashed != screenshotHash)
            {
                return new HttpNotFoundResult("The requested screenshot was not found.");
            }

            int requestedScreenshotId = int.Parse(parsedScreenshotId);

            var package = this.packages
                .GetById(int.Parse(parsedPackageId))
                .Select(p => new
                {
                    Name = p.Name,
                    Screenshot = p.Screenshots.Where(s => s.Id == requestedScreenshotId).FirstOrDefault()
                })
                .FirstOrDefault();

            string fullFilename = string.Format("{0}{1}", package.Screenshot.FileName, package.Screenshot.FileExtension);

            byte[] contents = this.screenshotSaver.Read(int.Parse(parsedPackageId), package.Name, fullFilename);
            string mimeType = MimeMapping.GetMimeMapping(fullFilename);

            return new FileContentResult(contents, mimeType);
        }
    }
}