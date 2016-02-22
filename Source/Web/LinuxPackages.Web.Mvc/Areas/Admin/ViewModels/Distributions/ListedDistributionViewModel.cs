namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels.Distributions
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Common.Constants;
    using Data.Models;
    using Infrastructure.Mappings;

    public class ListedDistributionViewModel : IMapFrom<Distribution>
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(DistributionConstants.MaxDistributionName, MinimumLength = DistributionConstants.MinDistributionName)]
        [UIHint("SingleLineText")]
        public string Name { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(DistributionConstants.MaxVersionLength, MinimumLength = DistributionConstants.MinVersionLength)]
        [UIHint("SingleLineText")]
        public string Version { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(DistributionConstants.MaxMaintainerLength, MinimumLength = DistributionConstants.MinMaintainerLength)]
        [UIHint("SingleLineText")]
        public string Maintainer { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Url)]
        [UIHint("SingleLineText")]
        public string Url { get; set; }
    }
}