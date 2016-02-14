namespace LinuxPackages.Web.Mvc.Controllers
{
    using Kendo.Mvc.UI;
    using ViewModels.Packages;
    using System.Web.Mvc;
    using Services.Data.Contracts;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using Kendo.Mvc.Extensions;
    using Infrastructure.ActionFilters;
    using Infrastructure.Helpers;
    using System.Web;
    using Infrastructure.Extensions;
    using Infrastructure.Mappings;
    using ViewModels.Account;

    public class CommentsController : Controller
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

            // TODO: Extract BaseController and fetch user details to fix the UI bug with unknown comment author
            comment.Author = new ListedUserViewModel() { };
            return Json(new[] { comment }.ToDataSourceResult(request, ModelState));
        }
    }
}