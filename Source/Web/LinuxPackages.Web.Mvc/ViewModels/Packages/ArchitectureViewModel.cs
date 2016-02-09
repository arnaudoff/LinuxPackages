namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using Infrastructure.Mappings;
    using LinuxPackages.Data.Models;

    public class ArchitectureViewModel : IMapFrom<Architecture>
    {
        public string Name { get; set; }
    }
}