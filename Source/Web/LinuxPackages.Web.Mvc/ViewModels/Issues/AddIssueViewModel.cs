namespace LinuxPackages.Web.Mvc.ViewModels.Issues
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mappings;
    using LinuxPackages.Common.Constants;

    public class AddIssueViewModel : IMapFrom<Issue>
    {
        [Required]
        [AllowHtml]
        [MinLength(IssueConstants.MinTitleLength)]
        [MaxLength(IssueConstants.MaxTitleLength)]
        [UIHint("SingleLineText")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Severity")]
        [UIHint("DropDownList")]
        public int Severity { get; set; }

        public IEnumerable<SelectListItem> SeverityLevels { get; set; }

        [Required]
        [AllowHtml]
        [MinLength(IssueConstants.MinContentLength)]
        [MaxLength(IssueConstants.MaxContentLength)]
        [UIHint("KendoEditor")]
        public string Content { get; set; }

        public string PackageId { get; set; }

        [Display(Name = "Package")]
        [UIHint("SingleLineText")]
        public string PackageName { get; set; }
    }
}