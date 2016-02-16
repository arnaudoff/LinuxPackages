namespace LinuxPackages.Web.Mvc.ViewModels.Profile
{
    using AutoMapper;
    using Infrastructure.Mappings;
    using LinuxPackages.Data.Models;
    using Issues;
    using Packages;
    using System.Collections.Generic;
    using Infrastructure.Helpers;
    using System;

    public class ProfileViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public List<ListedPackageViewModel> Packages { get; set; }

        public List<ListedIssueViewModel> Issues { get; set; }

        public int AvatarId { get; set; }

        public Avatar Avatar { get; set; }

        public string AvatarUrl
        {
            get
            {
                return (new UrlIdentifierProvider()).EncodeEntityId(this.AvatarId);
            }
        }
    }
}