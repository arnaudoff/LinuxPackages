namespace LinuxPackages.Web.Mvc.ViewModels.Issues
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using LinuxPackages.Data.Models;
    using LinuxPackages.Web.Mvc.Infrastructure.Helpers;
    using LinuxPackages.Web.Mvc.Infrastructure.Mappings;
    using Packages;

    public class IssueDetailsViewModel : IMapFrom<Issue>
    {
        public int Id { get; set; }

        public string Url
        {
            get
            {
                return (new UrlIdentifierProvider()).EncodeEntityId(this.Id);
            }
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public IssueSeverityType Severity { get; set; }

        public IssueStateType State { get; set; }

        public DateTime OpenedOn { get; set; }

        public int PositiveVotes { get; set; }

        public int NegativeVotes { get; set; }

        public User Author { get; set; }

        [Display(Name = "Package name")]
        public ListedPackageViewModel Package { get; set; }
    }
}