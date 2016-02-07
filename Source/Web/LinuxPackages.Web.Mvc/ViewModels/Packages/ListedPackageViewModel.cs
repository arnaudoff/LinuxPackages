namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using System;
    using System.Linq;

    using LinuxPackages.Data.Models;
    using Infrastructure.Mappings;
    using AutoMapper;

    public class ListedPackageViewModel : IMapFrom<Package>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Distribution { get; set; }

        public string Repository { get; set; }

        public string Architecture { get; set; }

        public string License { get; set; }

        public string Rating { get; set; }

        public DateTime UploadedOn { get; set; }

        public string Size { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Package, ListedPackageViewModel>()
                .ForMember(p => p.Distribution, opts => opts.MapFrom(p => p.Distribution.Name + " " + p.Distribution.Version))
                .ForMember(p => p.Repository, opts => opts.MapFrom(p => p.Repository.Name))
                .ForMember(p => p.Architecture, opts => opts.MapFrom(p => p.Architecture.Name))
                .ForMember(p => p.License, opts => opts.MapFrom(p => p.License.Name))
                .ForMember(p => p.Rating, opts => opts.MapFrom(p => p.Ratings.Average(r => r.Value) == null ? "Not rated" : p.Ratings.Average(r => r.Value).ToString()))
                .ForMember(p => p.Size, opts => opts.MapFrom(p => p.Size.ToString() + "B"));
        }
    }
}