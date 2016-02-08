namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using System;
    using System.Linq;

    using LinuxPackages.Data.Models;
    using Infrastructure.Mappings;
    using AutoMapper;

    public class ListedPackageViewModel : IMapFrom<Package>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DistributionViewModel Distribution { get; set; }

        public RepositoryViewModel Repository { get; set; }

        public ArchitectureViewModel Architecture { get; set; }

        public double AverageRating { get; set; }

        public DateTime UploadedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Package, ListedPackageViewModel>()
                .ForMember(p => p.AverageRating, opts => opts.MapFrom(p => p.Ratings.Average(r => r.Value) == null ? "Not rated" : p.Ratings.Average(r => r.Value).ToString()));
        }
    }
}