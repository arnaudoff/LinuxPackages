namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Account;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Helpers;
    using LinuxPackages.Web.Mvc.Infrastructure.Mappings;

    public class PackageDetailsViewModel : IMapFrom<Package>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Url
        {
            get
            {
                return (new UrlIdentifierProvider()).EncodeEntityId(this.Id);
            }
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public DistributionViewModel Distribution { get; set; }

        public RepositoryViewModel Repository { get; set; }

        public ArchitectureViewModel Architecture { get; set; }

        public LicenseViewModel License { get; set; }

        public string FileName { get; set; }

        public int Size { get; set; }

        public DateTime UploadedOn { get; set; }

        public string AverageRating { get; set; }

        public IEnumerable<ListedUserViewModel> Maintainers { get; set; }

        public IEnumerable<ListedScreenshotViewModel> Screenshots { get; set; }

        public IEnumerable<PackageCommentViewModel> Comments { get; set; }

        public IEnumerable<ListedPackageViewModel> Dependencies { get; set; }

        public int IssuesCount { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Package, PackageDetailsViewModel>()
                .ForMember(p => p.AverageRating, opts => opts.MapFrom(p => p.Ratings.Average(r => r.Value) == null ? "Not rated" : p.Ratings.Average(r => r.Value).ToString()))
                .ForMember(p => p.IssuesCount, opts => opts.MapFrom(p => p.Issues.Count()));
        }
    }
}