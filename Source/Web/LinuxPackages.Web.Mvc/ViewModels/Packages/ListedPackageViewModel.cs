namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using System;
    using System.Linq;

    using AutoMapper;
    using Infrastructure.Helpers;
    using Infrastructure.Mappings;
    using LinuxPackages.Data.Models;

    public class ListedPackageViewModel : IMapFrom<Package>, IHaveCustomMappings
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

        public string Distribution { get; set; }

        public string Repository { get; set; }

        public string Architecture { get; set; }

        public double AverageRating { get; set; }

        public DateTime UploadedOn { get; set; }

        public int Downloads { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Package, ListedPackageViewModel>()
                .ForMember(p => p.Id, opts => opts.MapFrom(p => p.Id))
                .ForMember(p => p.Distribution, opts => opts.MapFrom(p => p.Distribution.Name + " " + p.Distribution.Version))
                .ForMember(p => p.Repository, opts => opts.MapFrom(p => p.Repository.Name))
                .ForMember(p => p.Architecture, opts => opts.MapFrom(p => p.Architecture.Name))
                .ForMember(p => p.AverageRating, opts => opts.MapFrom(p => p.Ratings.Select(r => r.Value).DefaultIfEmpty(0).Average()));
        }
    }
}