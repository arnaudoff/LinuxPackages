namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using System;
    using System.Linq;

    using LinuxPackages.Data.Models;
    using Infrastructure.Mappings;
    using AutoMapper;
    using Infrastructure.Helpers;
    using System.Web;
    using Common.Constants;

    public class ListedPackageViewModel : IMapFrom<Package>, IHaveCustomMappings
    {
        private string id;

        public string Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value.ToString() + QueryStringUrlHelper.GenerateUrlHash(value.ToString(), (string)HttpContext.Current.Application[GlobalConstants.UrlSaltKeyName]);
            }
        }

        public string Name { get; set; }

        public string Distribution { get; set; }

        public string Repository { get; set; }

        public string Architecture { get; set; }

        public string AverageRating { get; set; }

        public DateTime UploadedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Package, ListedPackageViewModel>()
                .ForMember(p => p.Id, opts => opts.MapFrom(p => p.Id))
                .ForMember(p => p.Distribution, opts => opts.MapFrom(p => p.Distribution.Name + " " + p.Distribution.Version))
                .ForMember(p => p.Repository, opts => opts.MapFrom(p => p.Repository.Name))
                .ForMember(p => p.Architecture, opts => opts.MapFrom(p => p.Architecture.Name))
                .ForMember(p => p.AverageRating, opts => opts.MapFrom(p => p.Ratings.Average(r => r.Value) == null ? "Not rated" : p.Ratings.Average(r => r.Value).ToString()));
        }
    }
}