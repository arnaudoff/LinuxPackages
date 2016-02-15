namespace LinuxPackages.Web.Mvc.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.ActionFilters;
    using Infrastructure.Extensions;
    using Infrastructure.Helpers;
    using Infrastructure.Mappings;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;

    using Services.Data.Contracts;
    using ViewModels.Account;
    using ViewModels.Packages;
    using Infrastructure.Helpers.Contracts;

    public class CommentsController : BaseController
    {
        private readonly IPackagesService packages;
        private readonly ISanitizer sanitizer;

        public CommentsController(IPackagesService packages, ISanitizer sanitizer)
        {
            this.packages = packages;
            this.sanitizer = sanitizer;
        }

        [HashCheck("packageId")]
        public ActionResult All([DataSourceRequest]DataSourceRequest request, string packageId)
        {
            int requestedPackageId = this.UrlIdentifierProvider.DecodeEntityId(packageId);

            var comments = this.packages
                .GetCommentsByPackageId(requestedPackageId)
                .To<PackageCommentViewModel>()
                .ToDataSourceResult(request);

            return Json(comments);
        }

        [Authorize]
        [HttpPost]
        [HashCheck("packageId")]
        public ActionResult Add([DataSourceRequest]DataSourceRequest request, PackageCommentViewModel comment, string packageId)
        {
            int requestedPackageId = this.UrlIdentifierProvider.DecodeEntityId(packageId);

            if (comment != null && ModelState.IsValid)
            {
                this.packages.AddComment(this.sanitizer.Sanitize(comment.Content), requestedPackageId, this.User.Identity.GetUserId());
            }

            comment.Author = this.Mapper.Map<ListedUserViewModel>(this.UserProfile);
            return Json(new[] { comment }.ToDataSourceResult(request, ModelState));
        }
    }
}