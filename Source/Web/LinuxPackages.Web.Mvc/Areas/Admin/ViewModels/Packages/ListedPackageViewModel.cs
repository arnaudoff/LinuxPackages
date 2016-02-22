namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels.Packages
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using Common.Constants;
    using Data.Models;
    using Infrastructure.Helpers;
    using Infrastructure.Mappings;

    public class ListedPackageViewModel : IMapFrom<Package>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Url
        {
            get
            {
                return new UrlIdentifierProvider().EncodeEntityId(this.Id);
            }
        }

        [Required]
        [AllowHtml]
        [StringLength(PackageConstants.MaxNameLength, MinimumLength = PackageConstants.MinNameLength)]
        [UIHint("SingleLineText")]
        public string Name { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(PackageConstants.MaxDescriptionLength, MinimumLength = PackageConstants.MinDescriptionLength)]
        [UIHint("MultiLineText")]
        public string Description { get; set; }

        public string Distribution { get; set; }

        [Required]
        [Display(Name = "Distribution")]
        [UIHint("DropDownList")]
        public int DistributionId { get; set; }

        public string Repository { get; set; }

        [Required]
        [Display(Name = "Repository")]
        [UIHint("DropDownList")]
        public int RepositoryId { get; set; }

        public string Architecture { get; set; }

        [Required]
        [Display(Name = "Architecture")]
        [UIHint("DropDownList")]
        public int ArchitectureId { get; set; }

        public string License { get; set; }

        [Required]
        [Display(Name = "License")]
        [UIHint("DropDownList")]
        public int LicenseId { get; set; }

        public double AverageRating { get; set; }

        public DateTime UploadedOn { get; set; }

        public int Downloads { get; set; }

        public string FileName { get; set; }

        public int Size { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Package, ListedPackageViewModel>()
                .ForMember(p => p.Id, opts => opts.MapFrom(p => p.Id))
                .ForMember(p => p.Distribution, opts => opts.MapFrom(p => p.Distribution.Name + " " + p.Distribution.Version))
                .ForMember(p => p.Repository, opts => opts.MapFrom(p => p.Repository.Name))
                .ForMember(p => p.Architecture, opts => opts.MapFrom(p => p.Architecture.Name))
                .ForMember(p => p.License, opts => opts.MapFrom(p => p.License.Name))
                .ForMember(p => p.AverageRating, opts => opts.MapFrom(p => p.Ratings.Select(r => r.Value).DefaultIfEmpty(0).Average()));
        }
    }
}