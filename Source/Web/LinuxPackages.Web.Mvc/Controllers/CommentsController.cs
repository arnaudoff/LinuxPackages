namespace LinuxPackages.Web.Mvc.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.ActionFilters;
    using Infrastructure.Helpers.Contracts;
    using Infrastructure.Mappings;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using ViewModels.Account;
    using ViewModels.Packages;

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

            return this.Json(comments);
        }

        [Authorize]
        [HttpPost]
        [HashCheck("packageId")]
        public ActionResult Add([DataSourceRequest]DataSourceRequest request, PackageCommentViewModel comment, string packageId)
        {
            int requestedPackageId = this.UrlIdentifierProvider.DecodeEntityId(packageId);

            if (comment != null && this.ModelState.IsValid)
            {
                this.packages.AddComment(this.sanitizer.Sanitize(comment.Content), requestedPackageId, this.User.Identity.GetUserId());
            }

            comment.Author = this.Mapper.Map<ListedUserViewModel>(this.UserProfile);
            return this.Json(new[] { comment }.ToDataSourceResult(request, this.ModelState));
        }
    }
}