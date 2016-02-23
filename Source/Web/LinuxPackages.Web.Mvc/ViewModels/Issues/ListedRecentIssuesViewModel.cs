namespace LinuxPackages.Web.Mvc.ViewModels.Issues
{
    using Data.Models;
    using Infrastructure.Helpers;
    using Infrastructure.Mappings;
    using Packages;

    public class ListedRecentIssuesViewModel : IMapFrom<Issue>
    {
        public int Id { get; set; }

        public string Url
        {
            get
            {
                return new UrlIdentifierProvider().EncodeEntityId(this.Id);
            }
        }

        public string Title { get; set; }

        public string ShortenedTitle
        {
            get
            {
                if (this.Title != null)
                {
                    return this.Title.Length > 15 ? this.Title.Substring(0, 15) + "..." : this.Title;
                }

                return null;
            }
        }

        public int PackageId { get; set; }

        public string PackageName { get; set; }

        public string ShortenedPackageName
        {
            get
            {
                if (this.PackageName != null)
                {
                    return this.PackageName.Length > 15 ? this.PackageName.Substring(0, 15) + "..." : this.PackageName;
                }

                return null;
            }
        }
    }
}