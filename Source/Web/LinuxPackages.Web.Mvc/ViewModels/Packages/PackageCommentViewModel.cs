namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using LinuxPackages.Data.Models;
    using Infrastructure.Mappings;
    using System;
    using AutoMapper;
    using Account;

    public class PackageCommentViewModel : IMapFrom<PackageComment>
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public ListedUserViewModel Author { get; set; }
    }
}