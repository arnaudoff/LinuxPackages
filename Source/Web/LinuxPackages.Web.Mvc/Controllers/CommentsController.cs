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

    public class CommentsController : BaseController
    {
        private readonly IPackagesService packages;

        public CommentsController(IPackagesService packages)
        {
            this.packages = packages;
        }

        [HashCheck("packageId")]
        public ActionResult All([DataSourceRequest]DataSourceRequest request, string packageId)
        {
            int requestedPackageId = int.Parse(QueryStringUrlHelper.GetEntityIdFromUrlHash(packageId));

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
            int requestedPackageId = int.Parse(QueryStringUrlHelper.GetEntityIdFromUrlHash(packageId));

            if (comment != null && ModelState.IsValid)
            {
                this.packages.AddComment(comment.Content, requestedPackageId, this.User.Identity.GetUserId());
            }

            comment.Author = this.Mapper.Map<ListedUserViewModel>(this.UserProfile);
            return Json(new[] { comment }.ToDataSourceResult(request, ModelState));
        }
    }
}