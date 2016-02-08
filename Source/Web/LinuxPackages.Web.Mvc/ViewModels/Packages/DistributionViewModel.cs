
namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using LinuxPackages.Data.Models;
    using Infrastructure.Mappings;

    public class DistributionViewModel : IMapFrom<Distribution>
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public string Maintainer { get; set; }

        public string Url { get; set; }
    }
}