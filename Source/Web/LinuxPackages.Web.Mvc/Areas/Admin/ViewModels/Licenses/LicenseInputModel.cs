namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels.Licenses
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Common.Constants;

    public class LicenseInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(LicenseConstants.MaxLicenseNameLength, MinimumLength = LicenseConstants.MinLicenseNameLength)]
        public string Name { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(LicenseConstants.MaxLicenseDescriptionLength, MinimumLength = LicenseConstants.MinLicenseDescriptionLength)]
        public string Description { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Url)]
        public string Url { get; set; }
    }
}