namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using LinuxPackages.Data.Models;
    using Infrastructure.Mappings;

    public class ListedScreenshotViewModel : IMapFrom<Screenshot>
    {
        public int Id { get; set; }
    }
}