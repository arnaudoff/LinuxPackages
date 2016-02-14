namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Account;
    using AutoMapper;
    using Infrastructure.Mappings;
    using LinuxPackages.Data.Models;
    using Common.Constants;

    public class PackageCommentViewModel : IMapFrom<PackageComment>
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [MinLength(PackageConstants.MinCommentContentLength)]
        [MaxLength(PackageConstants.MaxCommentContentLength)]
        [UIHint("KendoEditor")]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public ListedUserViewModel Author { get; set; }
    }
}