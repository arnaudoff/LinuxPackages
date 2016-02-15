namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Account;
    using Common.Constants;
    using Infrastructure.Mappings;
    using LinuxPackages.Data.Models;

    public class PackageCommentViewModel : IMapFrom<PackageComment>
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [MinLength(PackageConstants.MinCommentContentLength)]
        [MaxLength(PackageConstants.MaxCommentContentLength)]
        [StringLength(PackageConstants.MaxCommentContentLength, MinimumLength = PackageConstants.MinCommentContentLength)]
        [UIHint("KendoEditor")]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public ListedUserViewModel Author { get; set; }
    }
}