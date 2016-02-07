namespace LinuxPackages.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class License
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(LicenseConstants.MinLicenseNameLength)]
        [MaxLength(LicenseConstants.MaxLicenseNameLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(LicenseConstants.MinLicenseDescriptionLength)]
        [MaxLength(LicenseConstants.MaxLicenseDescriptionLength)]
        public string Description { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }
    }
}