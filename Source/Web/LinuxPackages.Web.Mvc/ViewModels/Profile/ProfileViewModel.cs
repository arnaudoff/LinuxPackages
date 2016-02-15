namespace LinuxPackages.Web.Mvc.ViewModels.Profile
{
    using AutoMapper;
    using Infrastructure.Mappings;
    using LinuxPackages.Data.Models;
    using Issues;
    using Packages;
    using System.Collections.Generic;
    using Infrastructure.Helpers;

    public class ProfileViewModel : IMapFrom<User>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public List<ListedPackageViewModel> PackagesMaintained { get; set; }

        public List<ListedIssueViewModel> IssuesCreated { get; set; }

        public int AvatarId { get; set; }

        public string AvatarUrl
        {
            get
            {
                return (new UrlIdentifierProvider()).EncodeEntityId(this.AvatarId);
            }
        }
    }
}