namespace LinuxPackages.Web.Mvc.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Helpers.Contracts;
    using Infrastructure.Mappings;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Mvc.Controllers;
    using Services.Data.Contracts;
    using ViewModels.Issues;
    using Infrastructure.Helpers;
    using Data.Models;

    public class IssuesController : BaseController
    {
        private readonly IIssuesService issues;
        private readonly ISanitizer sanitizer;

        public IssuesController(IIssuesService issues, ISanitizer sanitizer)
        {
            this.issues = issues;
            this.sanitizer = sanitizer;
        }

        public ActionResult Manage()
        {
            if (this.issues.GetAll().Count() == 0)
            {
                return this.PartialView("_NoEntriesToManagePartial");
            }

            this.ViewData["severityTypes"] = Conversions.EnumToSelectList<IssueSeverityType>();
            this.ViewData["states"] = Conversions.EnumToSelectList<IssueStateType>();

            return this.View();
        }

        public ActionResult GetIssues([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.issues
                .GetAll()
                .To<AdminListedIssueViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateIssues([DataSourceRequest]DataSourceRequest request, UpdateIssueViewModel issue)
        {
            if (this.ModelState.IsValid)
            {
                this.issues.Update(
                    issue.Id,
                    issue.Title,
                    this.sanitizer.Sanitize(issue.Content),
                    (IssueSeverityType)issue.SeverityId,
                    (IssueStateType)issue.StateId);
            }

            var issueToDisplay = this.issues
                           .GetAll()
                           .To<AdminListedIssueViewModel>()
                           .FirstOrDefault(i => i.Id == issue.Id);

            return this.Json(new[] { issueToDisplay }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteIssue([DataSourceRequest]DataSourceRequest request, AdminListedIssueViewModel issue)
        {
            this.issues.DeleteById(issue.Id);
            return this.Json(new[] { issue }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Export(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);
            return this.File(fileContents, contentType, fileName);
        }
    }
}