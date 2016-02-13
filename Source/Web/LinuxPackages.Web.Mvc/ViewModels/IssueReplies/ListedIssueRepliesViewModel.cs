namespace LinuxPackages.Web.Mvc.ViewModels.IssueReplies
{
    using System;
    using LinuxPackages.Web.Mvc.ViewModels.Account;
    using Data.Models;
    using Infrastructure.Mappings;

    public class ListedIssueRepliesViewModel : IMapFrom<IssueReply>
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public ListedUserViewModel Author { get; set; }
    }
}