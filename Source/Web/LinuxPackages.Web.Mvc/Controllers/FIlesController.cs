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
    using Services.Data.Contracts.Savers;

    public class FilesController : BaseController
    {
        private readonly IPackagesService packages;
        private readonly IUsersService users;
        private readonly IPackageSaver packageSaver;
        private readonly IScreenshotSaver screenshotSaver;
        private readonly IAvatarSaver avatarSaver;

        public FilesController(
            IPackagesService packages,
            IUsersService users,
            IPackageSaver packageSaver,
            IScreenshotSaver screenshotSaver,
            IAvatarSaver avatarSaver)
        {
            this.packages = packages;
            this.users = users;
            this.packageSaver = packageSaver;
            this.screenshotSaver = screenshotSaver;
            this.avatarSaver = avatarSaver;
        }

        [HashCheck("id", "resource")]
        public ActionResult Screenshots(string id, string resource, string size)
        {
            int requestedPackageId = this.UrlIdentifierProvider.DecodeEntityId(id);
            int requestedScreenshotId = this.UrlIdentifierProvider.DecodeEntityId(resource);

            string screenshotPath = null;
            if (size == string.Empty)
            {
                screenshotPath = this.screenshotSaver.GetScreenshotPath(requestedPackageId, requestedScreenshotId);
            }
            else
            {
                if (this.IsMatchingSizeConstaint(size))
                {
                    int[] screenshotDimensions = this.ParseImageDimensions(size);
                    screenshotPath = this.screenshotSaver.GetScreenshotPath(requestedPackageId, requestedScreenshotId, screenshotDimensions[0], screenshotDimensions[1]);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid screenshot size parameter");
                }
            }

            byte[] contents = screenshotSaver.Read(screenshotPath);
            return new FileContentResult(contents, MimeMapping.GetMimeMapping(screenshotPath));
        }

        [HashCheck("resource")]
        public ActionResult Avatars(string id, string resource, string size)
        {
            int requestedAvatarId = this.UrlIdentifierProvider.DecodeEntityId(resource);

            byte[] contents = null;
            if (size == string.Empty)
            {
                contents = this.avatarSaver.Read(id, requestedAvatarId);
            }
            else
            {
                if (this.IsMatchingSizeConstaint(size))
                {
                    int[] avatarDimensions = this.ParseImageDimensions(size);
                    contents = this.avatarSaver.Read(id, requestedAvatarId, avatarDimensions[0], avatarDimensions[1]);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid avatar size parameter");
                }
            }

            return new FileContentResult(contents, MimeMapping.GetMimeMapping(this.users.GetAvatarFileNameById(requestedAvatarId)));
        }

        [HashCheck("id")]
        public ActionResult Packages(string id)
        {
            int requestedPackageId = this.UrlIdentifierProvider.DecodeEntityId(id);
            this.packages.IncrementDownloads(requestedPackageId);

            var package = this.packages
                .GetById(requestedPackageId)
                .Select(p => new { Name = p.Name, FileName = p.FileName })
                .FirstOrDefault();

            byte[] contents = this.packageSaver.Read(requestedPackageId, package.Name, package.FileName);

            Response.AppendHeader("Content-Disposition", new ContentDisposition { FileName = package.FileName, Inline = false }.ToString());
            return File(contents, MimeMapping.GetMimeMapping(package.FileName));
        }

        private bool IsMatchingSizeConstaint(string size)
        {
            Match match = Regex.Match(size, GlobalConstants.ImageSizeUrlPattern, RegexOptions.IgnoreCase);
            return match.Success;
        }

        private int[] ParseImageDimensions(string size)
        {
            Match match = Regex.Match(size, GlobalConstants.ImageSizeUrlPattern, RegexOptions.IgnoreCase);

            int width = int.Parse(match.Groups[1].Value);
            int height = int.Parse(match.Groups[2].Value);

            return new int[] { width, height };
        }
    }
}