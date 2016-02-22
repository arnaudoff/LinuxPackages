namespace LinuxPackages.Web.Mvc.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Common.Constants;
    using Infrastructure.Mappings;
    using Mvc.Controllers;
    using Services.Data.Contracts;
    using ViewModels.Home;

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
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
            var viewModel = new HomeViewModel
            {
                Packages = this.packages.GetAll().Count(),
                Issues = this.issues.GetAll().Count(),
                Users = this.users.GetAll().Count(),
                LatestPackages = this.packages.GetLatest(10).To<ListedLatestPackageViewModel>().ToList(),
                LatestIssues = this.issues.GetLatest(10).To<ListedLatestIssueViewModel>().ToList(),
                LatestComments = this.packages.GetLatestComments(10).To<ListedLatestCommentViewModel>().ToList(),
                LatestIssueReplies = this.issues.GetLatestReplies(10).To<ListedLatestIssueReplyViewModel>().ToList()
            };

            return this.View(viewModel);
        }
    }
}