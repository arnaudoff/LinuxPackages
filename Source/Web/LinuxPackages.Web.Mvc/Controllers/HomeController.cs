namespace LinuxPackages.Web.Mvc.Controllers
{
    using Kendo.Mvc.UI;
    using ViewModels.Issues;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using Infrastructure.Mappings;
    using Kendo.Mvc.Extensions;
    using ViewModels.Packages;
    using ViewModels.Account;

    public class HomeController : Controller
    {
        private readonly IPackagesService packages;
        private readonly IIssuesService issues;
        private readonly IUsersService users;

        public HomeController(IPackagesService packages, IIssuesService issues, IUsersService users)
        {
            this.packages = packages;
            this.issues = issues;
            this.users = users;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult GetRecentIssues([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.issues
                .GetAll()
                .To<ListedRecentIssuesViewModel>()
                .ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMostDownloadedPackages([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.packages
                .GetAll()
                .To<ListedMostDownloadedPackagesViewModel>()
                .ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTopMaintainers([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.users
                .GetTopMaintainers(5)
                .To<ListedTopMaintainersViewModel>()
                .ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}