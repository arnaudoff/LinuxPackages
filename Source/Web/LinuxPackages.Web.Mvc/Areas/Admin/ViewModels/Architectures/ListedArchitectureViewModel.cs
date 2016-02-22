namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels.Architectures
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Common.Constants;
    using Data.Models;
    using Infrastructure.Mappings;

    public class ListedArchitectureViewModel : IMapFrom<Architecture>
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(ArchitectureConstants.MaxArchitectureNameLength, MinimumLength = ArchitectureConstants.MinArchitectureNameLength)]
        [UIHint("SingleLineText")]
        public string Name { get; set; }
    }
}