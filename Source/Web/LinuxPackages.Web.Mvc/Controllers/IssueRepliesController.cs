namespace LinuxPackages.Web.Mvc.Controllers
{
    using System.Web.Mvc;

    using Infrastructure.ActionFilters;
    using Infrastructure.Helpers;
    using Infrastructure.Helpers.Contracts;

    using Microsoft.AspNet.Identity;

    using Services.Data.Contracts;
    using ViewModels.IssueReplies;
    using Infrastructure.Mappings;
    using System.Linq;

    public class IssueRepliesController : BaseController
    {
        private readonly IIssuesService issues;
        private readonly ISanitizer sanitizer;

        public IssueRepliesController(IIssuesService issues, ISanitizer sanitizer)
        {
            this.issues = issues;
            this.sanitizer = sanitizer;
        }

        [HttpGet]
        [HashCheck("id")]
        public ActionResult ByIssueId(string id)
        {
            int requestedIssueId = int.Parse(QueryStringUrlHelper.GetEntityIdFromUrlHash(id));

            var replies = this.issues
                .GetRepliesByIssueId(requestedIssueId)
                .To<ListedIssueRepliesViewModel>()
                .ToList();

            return PartialView("_AllRepliesPartial", replies);
        }

        [Authorize]
        [HttpGet]
        [HashCheck("id")]
        public ActionResult Add(string id)
        {
            var model = new AddIssueReplyViewModel
            {
                IssueId = id
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HashCheck("id")]
        public ActionResult Add(AddIssueReplyViewModel model, string id)
        {
            int requestedIssueId = int.Parse(QueryStringUrlHelper.GetEntityIdFromUrlHash(id));

            if (!ModelState.IsValid)
            {
                model.IssueId = id;
                return View(model);
            }

            this.issues.CreateReply(this.sanitizer.Sanitize(model.Content), requestedIssueId, this.User.Identity.GetUserId());

            return this.RedirectToAction("Details", "Issues", new { id = id });
        }
    }
}