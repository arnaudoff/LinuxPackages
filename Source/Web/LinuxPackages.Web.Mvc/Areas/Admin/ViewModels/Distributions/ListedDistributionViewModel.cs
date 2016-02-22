namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels.Distributions
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Common.Constants;
    using Data.Models;
    using Infrastructure.Mappings;

    public class AdminListedDistributionViewModel : IMapFrom<Distribution>
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(DistributionConstants.MaxDistributionName, MinimumLength = DistributionConstants.MinDistributionName)]
        [UIHint("SingleLineText")]
        public string Name { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(DistributionConstants.MaxDistributionName, MinimumLength = DistributionConstants.MinDistributionName)]
        [UIHint("SingleLineText")]
        public string Version { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(DistributionConstants.MaxDistributionName, MinimumLength = DistributionConstants.MinDistributionName)]
        [UIHint("SingleLineText")]
        public string Maintainer { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Url)]
        [StringLength(DistributionConstants.MaxDistributionName, MinimumLength = DistributionConstants.MinDistributionName)]
        [UIHint("SingleLineText")]
        public string Url { get; set; }
    }
}