namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels.Issues
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Common.Constants;

    public class UpdateIssueInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [MinLength(IssueConstants.MinTitleLength)]
        [MaxLength(IssueConstants.MaxTitleLength)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [MinLength(IssueConstants.MinContentLength)]
        [MaxLength(IssueConstants.MaxContentLength)]
        public string Content { get; set; }

        [Required]
        public int SeverityId { get; set; }

        [Required]
        public int StateId { get; set; }
    }
}