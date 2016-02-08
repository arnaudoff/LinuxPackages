
namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using LinuxPackages.Data.Models;
    using Infrastructure.Mappings;

    public class ArchitectureViewModel : IMapFrom<Architecture>
    {
        public string Name { get; set; }
    }
}