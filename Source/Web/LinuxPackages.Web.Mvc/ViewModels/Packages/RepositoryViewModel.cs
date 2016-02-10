namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using System.Collections.Generic;

    using Infrastructure.Mappings;
    using LinuxPackages.Data.Models;

    public class RepositoryViewModel : IMapFrom<Repository>
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public ICollection<ListedPackageViewModel> Packages { get; set; }
    }
}