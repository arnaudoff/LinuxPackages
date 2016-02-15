namespace LinuxPackages.Web.Mvc.ViewModels.Issues
{
    using LinuxPackages.Data.Models;
    using LinuxPackages.Web.Mvc.Infrastructure.Helpers;
    using LinuxPackages.Web.Mvc.Infrastructure.Mappings;
    using Packages;

    public class ListedRecentIssuesViewModel : IMapFrom<Issue>
    {
        public int Id { get; set; }

        public string Url
        {
            get
            {
                return (new UrlIdentifierProvider()).EncodeEntityId(this.Id);
            }
        }

        public string Title { get; set; }

        public ListedPackageViewModel Package { get; set; }
    }
}