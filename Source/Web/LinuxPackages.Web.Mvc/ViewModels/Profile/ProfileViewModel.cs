namespace LinuxPackages.Web.Mvc.ViewModels.Profile
{
    using AutoMapper;
    using Infrastructure.Mappings;
    using LinuxPackages.Data.Models;

    public class ProfileViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int PackagesMaintainedCount { get; set; }

        public int IssuesCreatedCount { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<User, ProfileViewModel>()
                .ForMember(u => u.PackagesMaintainedCount, opts => opts.MapFrom(u => u.Packages.Count))
                .ForMember(u => u.IssuesCreatedCount, opts => opts.MapFrom(u => u.Issues.Count));
        }
    }
}