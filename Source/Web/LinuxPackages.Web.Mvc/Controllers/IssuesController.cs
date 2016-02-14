
namespace LinuxPackages.Web.Mvc.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using Data.Models;
    using Infrastructure.ActionFilters;
    using Infrastructure.Helpers;
    using Infrastructure.Helpers.Contracts;
    using Infrastructure.Mappings;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using ViewModels.Issues;

    public class IssuesController : BaseController
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
        [HttpGet]
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
                this.sanitizer.Sanitize(model.Content),
                (IssueSeverityType)model.Severity,
                requestedPackageId,
                this.User.Identity.GetUserId());

            return this.RedirectToAction("Index", "Home");
        }

        [HashCheck("id")]
        public ActionResult Details(string id)
        {
            int requestedIssueId = int.Parse(QueryStringUrlHelper.GetEntityIdFromUrlHash(id));

            var issueModel = this.issues
                .GetById(requestedIssueId)
                .To<IssueDetailsViewModel>()
                .FirstOrDefault();

            return View(issueModel);
        }

        public ActionResult GetIssues([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.issues
                .GetAll()
                .To<ListedIssueViewModel>()
                .ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}