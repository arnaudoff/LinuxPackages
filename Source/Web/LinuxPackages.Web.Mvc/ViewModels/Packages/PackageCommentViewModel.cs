namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using System;

    using Account;
    using AutoMapper;
    using Infrastructure.Mappings;
    using LinuxPackages.Data.Models;

    public class PackageCommentViewModel : IMapFrom<PackageComment>
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public ListedUserViewModel Author { get; set; }
    }
}