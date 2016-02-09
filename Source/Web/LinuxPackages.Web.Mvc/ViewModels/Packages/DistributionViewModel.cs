namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using Infrastructure.Mappings;
    using LinuxPackages.Data.Models;

    public class DistributionViewModel : IMapFrom<Distribution>
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public string Maintainer { get; set; }

        public string Url { get; set; }
    }
}