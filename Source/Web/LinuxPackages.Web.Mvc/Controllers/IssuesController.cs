
namespace LinuxPackages.Web.Mvc.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Data.Models;
    using Infrastructure.Helpers;
    using Infrastructure.Helpers.Contracts;
    using Services.Data.Contracts;
    using ViewModels.Issues;

    public class IssuesController : Controller
    {
        private readonly IPackagesService packages;
        private readonly IIssuesService issues;
        private readonly ISanitizer sanitizer;

        public IssuesController(IPackagesService packages, IIssuesService issues, ISanitizer sanitizer)
        {
            this.packages = packages;
            this.issues = issues;
            this.sanitizer = sanitizer;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Add(string id)
        {
            if (!QueryStringUrlHelper.IsHashValid(id))
            {
                return new HttpNotFoundResult("The requested package was not found.");
            }

            int requestedPackageId = int.Parse(QueryStringUrlHelper.GetEntityIdFromUrlHash(id));

            var model = new AddIssueViewModel
            {
                PackageId = id,
                PackageName = this.packages.GetById(requestedPackageId).Select(p => p.Name).FirstOrDefault(),
                SeverityLevels = Conversions.EnumToSelectList<IssueSeverityType>()
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddIssueViewModel model, string id)
        {
            int requestedPackageId = int.Parse(QueryStringUrlHelper.GetEntityIdFromUrlHash(id));

            if (!ModelState.IsValid)
            {
                model.SeverityLevels = Conversions.EnumToSelectList<IssueSeverityType>();
                model.PackageName = this.packages.GetById(requestedPackageId).Select(p => p.Name).FirstOrDefault();

                return View(model);
            }

            var newIssue = this.issues.Create(
                model.Title,
                (IssueSeverityType)model.Severity,
                this.sanitizer.Sanitize(model.Content),
                requestedPackageId);

            return this.RedirectToAction("Index", "Home");
        }
    }
}