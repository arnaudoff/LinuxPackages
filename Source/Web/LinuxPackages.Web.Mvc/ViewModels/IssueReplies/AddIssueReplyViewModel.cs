namespace LinuxPackages.Web.Mvc.ViewModels.IssueReplies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using LinuxPackages.Common.Constants;
    using Data.Models;
    using Infrastructure.Mappings;

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