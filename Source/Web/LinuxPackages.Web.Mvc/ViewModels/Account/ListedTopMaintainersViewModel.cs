namespace LinuxPackages.Web.Mvc.ViewModels.Account
{
    using System;

    using AutoMapper;
    using Infrastructure.Mappings;
    using LinuxPackages.Data.Models;

    public class ListedTopMaintainersViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Username { get; set; }

        public string MaintainedPackagesCount { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<User, ListedTopMaintainersViewModel>()
                .ForMember(m => m.MaintainedPackagesCount, opts => opts.MapFrom(m => m.Packages.Count));
        }
    }
}