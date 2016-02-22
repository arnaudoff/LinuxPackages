namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels.Licenses
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Common.Constants;

    public class ListedLicenseViewModel
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(LicenseConstants.MaxLicenseNameLength, MinimumLength = LicenseConstants.MinLicenseNameLength)]
        [UIHint("SingleLineText")]
        public string Name { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(LicenseConstants.MaxLicenseDescriptionLength, MinimumLength = LicenseConstants.MinLicenseDescriptionLength)]
        [UIHint("SingleLineText")]
        public string Description { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Url)]
        [UIHint("SingleLineText")]
        public string Url { get; set; }
    }
}