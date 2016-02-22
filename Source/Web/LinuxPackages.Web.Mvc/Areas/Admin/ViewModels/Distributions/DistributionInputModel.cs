namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels.Distributions
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Common.Constants;

    public class DistributionInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(DistributionConstants.MaxDistributionName, MinimumLength = DistributionConstants.MinDistributionName)]
        public string Name { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(DistributionConstants.MaxVersionLength, MinimumLength = DistributionConstants.MinVersionLength)]
        public string Version { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(DistributionConstants.MaxMaintainerLength, MinimumLength = DistributionConstants.MinMaintainerLength)]
        public string Maintainer { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Url)]
        public string Url { get; set; }
    }
}