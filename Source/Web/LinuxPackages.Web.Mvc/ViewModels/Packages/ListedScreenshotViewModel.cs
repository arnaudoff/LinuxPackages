namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using Infrastructure.Helpers;
    using Infrastructure.Mappings;
    using LinuxPackages.Data.Models;

    public class ListedScreenshotViewModel : IMapFrom<Screenshot>
    {
        public int Id { get; set; }

        public string Url
        {
            get
            {
                return (new UrlIdentifierProvider()).EncodeEntityId(this.Id);
            }
        }
    }
}