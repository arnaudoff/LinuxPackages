namespace LinuxPackages.Web.Mvc.ViewModels.IssueReplies
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Data.Models;
    using Infrastructure.Mappings;
    using LinuxPackages.Common.Constants;

    public class AddIssueReplyViewModel : IMapFrom<IssueReply>
    {
        [Required]
        [AllowHtml]
        [MinLength(IssueConstants.MinIssueReplyContentLength)]
        [MaxLength(IssueConstants.MaxIssueReplyContentLength)]
        [UIHint("KendoEditor")]
        public string Content { get; set; }

        public string IssueId { get; set; }
    }
}