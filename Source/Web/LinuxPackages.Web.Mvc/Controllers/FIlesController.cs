namespace LinuxPackages.Web.Mvc.Controllers
{
    using System;
    using System.Linq;
    using System.Net.Mime;
    using System.Web;
    using System.Web.Mvc;

    using Common.Constants;
    using Infrastructure.Helpers;
    using Services.Data;
    using Services.Data.Contracts;
    using System.Text.RegularExpressions;
    using System.Net;
    using Infrastructure.ActionFilters;

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

        [HashCheck("id", "resource")]
        public ActionResult Screenshots(string id, string resource, string size)
        {
            int requestedPackageId = int.Parse(QueryStringUrlHelper.GetEntityIdFromUrlHash(id));
            int requestedScreenshotId = int.Parse(QueryStringUrlHelper.GetEntityIdFromUrlHash(resource));

            string screenshotPath = null;
            if (size == string.Empty)
            {
                screenshotPath = this.screenshotSaver.GetScreenshotPath(requestedPackageId, requestedScreenshotId);
            }
            else
            {
                Match match = Regex.Match(size, PackageConstants.ScreenshotSizeUrlPattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    int width = int.Parse(match.Groups[1].Value);
                    int height = int.Parse(match.Groups[2].Value);
                    screenshotPath = this.screenshotSaver.GetScreenshotPath(requestedPackageId, requestedScreenshotId, width, height);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid screenshot size parameter");
                }
            }

            byte[] contents = screenshotSaver.Read(screenshotPath);
            return new FileContentResult(contents, MimeMapping.GetMimeMapping(screenshotPath));
        }
        
        [HashCheck("id")]
        public ActionResult Packages(string id)
        {
            int requestedPackageId = int.Parse(QueryStringUrlHelper.GetEntityIdFromUrlHash(id));
            this.packages.IncrementDownloads(requestedPackageId);

            var package = this.packages
                .GetById(requestedPackageId)
                .Select(p => new { Name = p.Name, FileName = p.FileName })
                .FirstOrDefault();

            byte[] contents = this.packageSaver.Read(requestedPackageId, package.Name, package.FileName);

            Response.AppendHeader("Content-Disposition", new ContentDisposition { FileName = package.FileName, Inline = false }.ToString());
            return File(contents, MimeMapping.GetMimeMapping(package.FileName));
        }
    }
}