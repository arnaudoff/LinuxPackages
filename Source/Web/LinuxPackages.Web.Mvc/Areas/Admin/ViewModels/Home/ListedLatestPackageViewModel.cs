namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels.Home
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mappings;
    using LinuxPackages.Web.Mvc.Infrastructure.Helpers;

    public class ListedLatestPackageViewModel : IMapFrom<Package>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Url
        {
            get
            {
                return new UrlIdentifierProvider().EncodeEntityId(this.Id);
            }
        }

        public string Name { get; set; }

        public string Distribution { get; set; }

        public DateTime UploadedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Package, ListedLatestPackageViewModel>()
                .ForMember(p => p.Distribution, opts => opts.MapFrom(p => p.Distribution.Name + " " + p.Distribution.Version));
        }
    }
}