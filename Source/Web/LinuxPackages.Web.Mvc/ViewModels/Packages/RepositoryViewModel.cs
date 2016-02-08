
namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using System.Collections.Generic;

    using LinuxPackages.Data.Models;
    using Infrastructure.Mappings;


    public class RepositoryViewModel : IMapFrom<Repository>
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public ICollection<ListedPackageViewModel> Packages { get; set; }
    }
}