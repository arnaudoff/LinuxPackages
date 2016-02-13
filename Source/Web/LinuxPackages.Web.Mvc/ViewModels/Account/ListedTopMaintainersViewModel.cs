namespace LinuxPackages.Web.Mvc.ViewModels.Account
{
    using LinuxPackages.Data.Models;
    using Infrastructure.Mappings;
    using AutoMapper;
    using System;

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