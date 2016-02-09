namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using Infrastructure.Mappings;
    using LinuxPackages.Data.Models;

    public class ListedScreenshotViewModel : IMapFrom<Screenshot>
    {
        public int Id { get; set; }
    }
}