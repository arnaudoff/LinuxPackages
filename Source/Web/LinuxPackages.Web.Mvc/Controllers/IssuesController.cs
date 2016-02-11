
namespace LinuxPackages.Web.Mvc.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Data.Models;
    using Infrastructure.Helpers;
    using Infrastructure.Helpers.Contracts;
    using Services.Data.Contracts;
    using ViewModels.Issues;
    using Infrastructure.ActionFilters;
    using Kendo.Mvc.UI;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;

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

        public ActionResult All()
        {
            return View();
        }

        [Authorize]
        [HashCheck("id")]
        public ActionResult Add(string id)
        {
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
        [HashCheck("id")]
        public ActionResult Add(AddIssueViewModel model, string id)
        {
            int requestedPackageId = int.Parse(QueryStringUrlHelper.GetEntityIdFromUrlHash(id));

            if (!ModelState.IsValid)
            {
                model.PackageId = id;
                model.PackageName = this.packages.GetById(requestedPackageId).Select(p => p.Name).FirstOrDefault();
                model.SeverityLevels = Conversions.EnumToSelectList<IssueSeverityType>();

                return View(model);
            }

            var newIssue = this.issues.Create(
                model.Title,
                (IssueSeverityType)model.Severity,
                this.sanitizer.Sanitize(model.Content),
                requestedPackageId);

            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult GetIssues([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.issues
                .GetAll()
                .ProjectTo<ListedIssueViewModel>()
                .ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}