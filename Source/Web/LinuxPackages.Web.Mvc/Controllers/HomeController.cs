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
    using ViewModels.Home;
    using System.Web;
    using Infrastructure.Extensions;

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
            var model = new IndexViewModel()
            {
                PackagesCount = HttpRuntime.Cache.GetOrStore<int>("packagesCount", () => this.packages.GetAll().Count()),
                MaintainersCount = HttpRuntime.Cache.GetOrStore<int>("maintainersCount", () => this.users.GetAll().Count()),
                IssuesCount = HttpRuntime.Cache.GetOrStore<int>("issuesCount", () => this.issues.GetAll().Count()),
            };

            return this.View(model);
        }

        public ActionResult GetRecentIssues([DataSourceRequest]DataSourceRequest request)
        {
            var result = HttpRuntime.Cache.GetOrStore<DataSourceResult>(
                "recentIssues",
                () => this.issues
                    .GetAll()
                    .To<ListedRecentIssuesViewModel>()
                    .ToDataSourceResult(request), 5);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMostDownloadedPackages([DataSourceRequest]DataSourceRequest request)
        {
            var result = HttpRuntime.Cache.GetOrStore<DataSourceResult>(
                "mostDownloadedPackages",
                () => this.packages
                    .GetAll()
                    .To<ListedMostDownloadedPackagesViewModel>()
                    .ToDataSourceResult(request), 30);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTopMaintainers([DataSourceRequest]DataSourceRequest request)
        {
            var result = HttpRuntime.Cache.GetOrStore<DataSourceResult>(
                "topMaintainers",
                () => this.users
                    .GetAll()
                    .To<ListedTopMaintainersViewModel>()
                    .ToDataSourceResult(request), 30);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}