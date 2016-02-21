namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels.Packages
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Common.Constants;

    public class UpdatePackageViewModel
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [MinLength(PackageConstants.MinNameLength)]
        [MaxLength(PackageConstants.MaxNameLength)]
        [UIHint("SingleLineText")]
        public string Name { get; set; }

        [Required]
        [AllowHtml]
        [MinLength(PackageConstants.MinDescriptionLength)]
        [MaxLength(PackageConstants.MaxDescriptionLength)]
        [UIHint("MultiLineText")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Distribution")]
        [UIHint("DropDownList")]
        public int DistributionId { get; set; }

        [Required]
        [Display(Name = "Repository")]
        [UIHint("DropDownList")]
        public int RepositoryId { get; set; }

        [Required]
        [Display(Name = "Architecture")]
        [UIHint("DropDownList")]
        public int ArchitectureId { get; set; }

        [Required]
        [Display(Name = "License")]
        [UIHint("DropDownList")]
        public int LicenseId { get; set; }
    }
}