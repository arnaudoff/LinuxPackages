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

    public class ListedIssueViewModel : IMapFrom<Issue>
    {
        private string hashedId;

        public string Id
        {
            get
            {
                return this.hashedId;
            }

            set
            {
                this.hashedId = value.ToString() + QueryStringUrlHelper.GenerateUrlHash(value.ToString(), (string)HttpContext.Current.Application[GlobalConstants.UrlSaltKeyName]);
            }
        }

        public string Title { get; set; }

        public IssueSeverityType Severity { get; set; }

        public DateTime OpenedOn { get; set; }

        public ListedPackageViewModel Package { get; set; }
    }
}