namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using System.Web;

    using LinuxPackages.Data.Models;
    using Infrastructure.Mappings;
    using Infrastructure.Helpers;
    using Common.Constants;

    public class ListedMostDownloadedPackagesViewModel : IMapFrom<Package>
    {
        private string id;

        public string Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value.ToString() + QueryStringUrlHelper.GenerateUrlHash(value.ToString(), (string)HttpContext.Current.Application[GlobalConstants.UrlSaltKeyName]);
            }
        }

        public string Name { get; set; }

        public int Downloads { get; set; }
    }
}