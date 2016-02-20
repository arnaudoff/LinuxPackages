namespace LinuxPackages.Web.Mvc.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using Infrastructure.Extensions;
    using Infrastructure.Mappings;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data.Contracts;
    using ViewModels.Account;
    using ViewModels.Home;
    using ViewModels.Issues;
    using ViewModels.Packages;

    public class HomeController : BaseController
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
            DataSourceResult result = HttpRuntime.Cache.GetOrStore<DataSourceResult>(
                "recentIssues",
                () => this.issues
                    .GetAll()
                    .To<ListedRecentIssuesViewModel>()
                    .ToDataSourceResult(request), 5);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMostDownloadedPackages([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = HttpRuntime.Cache.GetOrStore<DataSourceResult>(
                "mostDownloadedPackages",
                () => this.packages
                    .GetAll()
                    .To<ListedMostDownloadedPackagesViewModel>()
                    .ToDataSourceResult(request), 30);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTopMaintainers([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = HttpRuntime.Cache.GetOrStore<DataSourceResult>(
                "topMaintainers",
                () => this.users
                    .GetAll()
                    .To<ListedTopMaintainersViewModel>()
                    .ToDataSourceResult(request), 30);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNavbarUserInfo()
        {
            NavbarUserInfoViewModel viewModel = this.Mapper.Map<NavbarUserInfoViewModel>(this.UserProfile);
            return this.PartialView("_NavbarUserInfoPartial", viewModel);
        }
    }
}