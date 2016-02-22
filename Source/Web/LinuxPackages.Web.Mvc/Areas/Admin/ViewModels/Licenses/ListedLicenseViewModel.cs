namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels.Licenses
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Common.Constants;
    using Data.Models;
    using Infrastructure.Mappings;

    public class ListedLicenseViewModel : IMapFrom<License>
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
        [UIHint("MultiLineText")]
        public string Description { get; set; }

        public string ShortenedDescription
        {
            get
            {
                if (this.Description != null)
                {
                    return this.Description.Length > 100 ? this.Description.Substring(0, 100) + "..." : this.Description;
                }

                return null;
            }
        }

        [Required]
        [AllowHtml]
        [DataType(DataType.Url)]
        [UIHint("SingleLineText")]
        public string Url { get; set; }
    }
}