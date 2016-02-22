namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels.Home
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class HomeViewModel
    {
        [UIHint("StatsCountPanel")]
        public int Packages { get; set; }

        [UIHint("StatsCountPanel")]
        public int Issues { get; set; }

        [UIHint("StatsCountPanel")]
        public int Users { get; set; }

        public IEnumerable<ListedLatestIssueViewModel> LatestIssues { get; set; }

        public IEnumerable<ListedLatestPackageViewModel> LatestPackages { get; set; }

        public IEnumerable<ListedLatestIssueReplyViewModel> LatestIssueReplies { get; set; }

        public IEnumerable<ListedLatestCommentViewModel> LatestComments { get; set; }
    }
}