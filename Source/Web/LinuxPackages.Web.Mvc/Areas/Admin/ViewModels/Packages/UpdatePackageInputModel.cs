namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels.Packages
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Common.Constants;

    public class UpdatePackageInputModel
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [MinLength(PackageConstants.MinNameLength)]
        [MaxLength(PackageConstants.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [AllowHtml]
        [MinLength(PackageConstants.MinDescriptionLength)]
        [MaxLength(PackageConstants.MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public int DistributionId { get; set; }

        [Required]
        public int RepositoryId { get; set; }

        [Required]
        public int ArchitectureId { get; set; }

        [Required]
        public int LicenseId { get; set; }
    }
}