namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    using LinuxPackages.Common.Constants;

    public class AddPackageViewModel
    {
        [Required]
        [MinLength(PackageConstants.MinNameLength)]
        [MaxLength(PackageConstants.MaxNameLength)]
        [UIHint("SingleLineText")]
        public string Name { get; set; }

        [Required]
        [MinLength(PackageConstants.MinDescriptionLength)]
        [MaxLength(PackageConstants.MaxDescriptionLength)]
        [UIHint("MultiLineText")]
        public string Description { get; set; } 

        [Required]
        [Display(Name = "Distribution")]
        [UIHint("DropDownList")]
        public int DistributionId { get; set; }

        public IEnumerable<SelectListItem> Distributions { get; set; }

        [Required]
        [Display(Name = "Repository")]
        [UIHint("DropDownList")]
        public int RepositoryId { get; set; }

        public IEnumerable<SelectListItem> Repositories { get; set; }

        [Required]
        [Display(Name = "Architecture")]
        [UIHint("DropDownList")]
        public int ArchitectureId { get; set; }

        public IEnumerable<SelectListItem> Architectures { get; set; }

        [Required]
        [Display(Name = "License")]
        [UIHint("DropDownList")]
        public int LicenseId { get; set; }

        public IEnumerable<SelectListItem> Licenses { get; set; }

        [Required]
        [Display(Name = "Package")]
        [UIHint("SingleFileUpload")]
        public HttpPostedFileBase Contents { get; set; }

        [Display(Name = "Dependencies")]
        [UIHint("MultiSelect")]
        public List<int> DependencyIds { get; set; }

        public IEnumerable<SelectListItem> Dependencies { get; set; }

        [Required]
        [Display(Name = "Maintainers")]
        [UIHint("StringMultiSelect")]
        public List<string> MaintainerIds { get; set; }

        public IEnumerable<SelectListItem> Maintainers { get; set; }

        [UIHint("MultiFileUpload")]
        public IEnumerable<HttpPostedFileBase> Screenshots { get; set; }
    }
}