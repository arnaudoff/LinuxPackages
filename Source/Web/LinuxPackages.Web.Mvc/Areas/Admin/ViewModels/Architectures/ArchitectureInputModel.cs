namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels.Architectures
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Common.Constants;
    using Data.Models;
    using Infrastructure.Mappings;

    public class ArchitectureInputModel : IMapFrom<Architecture>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(ArchitectureConstants.MaxArchitectureNameLength, MinimumLength = ArchitectureConstants.MinArchitectureNameLength)]
        public string Name { get; set; }
    }
}