namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using Infrastructure.Helpers;
    using Infrastructure.Mappings;
    using LinuxPackages.Data.Models;

    public class ListedMostDownloadedPackagesViewModel : IMapFrom<Package>
    {
        public int Id { get; set; }

        public string Url
        {
            get
            {
                return new UrlIdentifierProvider().EncodeEntityId(this.Id);
            }
        }

        public string Name { get; set; }

        public int Downloads { get; set; }
    }
}