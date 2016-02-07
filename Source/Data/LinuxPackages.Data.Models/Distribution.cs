namespace LinuxPackages.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class Distribution
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(DistributionConstants.MinDistributionName)]
        [MaxLength(DistributionConstants.MaxDistributionName)]
        public string Name { get; set; }

        [Required]
        [MinLength(DistributionConstants.MinVersionLength)]
        [MaxLength(DistributionConstants.MaxVersionLength)]
        public string Version { get; set; }

        [Required]
        [MinLength(DistributionConstants.MinMaintainerLength)]
        [MaxLength(DistributionConstants.MaxMaintainerLength)]
        public string Maintainer { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }
    }
}