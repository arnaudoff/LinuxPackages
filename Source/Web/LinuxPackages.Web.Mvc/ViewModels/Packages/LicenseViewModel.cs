
namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using LinuxPackages.Data.Models;
    using Infrastructure.Mappings;

    public class LicenseViewModel : IMapFrom<License>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }
    }
}