namespace LinuxPackages.Web.Mvc.ViewModels.Issues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using LinuxPackages.Common.Constants;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Web.Mvc.Infrastructure.Helpers;
    using LinuxPackages.Web.Mvc.Infrastructure.Mappings;
    using Packages;
    using System.ComponentModel.DataAnnotations;

    public class IssueDetailsViewModel : IMapFrom<Issue>
    {
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