namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using System;
    using AutoMapper;
    using Data.Models;
    using LinuxPackages.Web.Mvc.Infrastructure.Mappings;
    using System.Collections.Generic;
    using System.Linq;
    using Account;

    public class PackageDetailsViewModel : IMapFrom<Package>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DistributionViewModel Distribution { get; set; }

        public RepositoryViewModel Repository { get; set; }

        public ArchitectureViewModel Architecture { get; set; }

        public LicenseViewModel License { get; set; }

        public string FileName { get; set; }

        public int Size { get; set; }

        public DateTime UploadedOn { get; set; }

        public double AverageRating { get; set; }

        public IEnumerable<ListedUserViewModel> Maintainers { get; set; }

        public IEnumerable<ListedScreenshotViewModel> Screenshot { get; set; }

        public IEnumerable<PackageCommentViewModel> Comments { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Package, PackageDetailsViewModel>()
                .ForMember(p => p.AverageRating, opts => opts.MapFrom(p => p.Ratings.Average(r => r.Value) == null ? "Not rated" : p.Ratings.Average(r => r.Value).ToString()));
        }
    }
}