namespace LinuxPackages.Web.Mvc.ViewModels.IssueReplies
{
    using System;

    using Data.Models;
    using Infrastructure.Mappings;
    using LinuxPackages.Web.Mvc.ViewModels.Account;

    public class ListedIssueRepliesViewModel : IMapFrom<IssueReply>
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public ListedUserViewModel Author { get; set; }
    }
}