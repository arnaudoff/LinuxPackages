namespace LinuxPackages.Web.Mvc.ViewModels.Issues
{
    using System;
    using Data.Models;
    using Infrastructure.Helpers;
    using Infrastructure.Mappings;
    using Packages;

    public class ListedIssueViewModel : IMapFrom<Issue>
    {
        public int Id { get; set; }

        public string Url
        {
            get
            {
                return new UrlIdentifierProvider().EncodeEntityId(this.Id);
            }
        }

        public string Title { get; set; }

        public IssueSeverityType Severity { get; set; }

        public IssueStateType State { get; set; }

        public DateTime OpenedOn { get; set; }

        public ListedPackageViewModel Package { get; set; }
    }
}