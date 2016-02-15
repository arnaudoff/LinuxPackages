namespace LinuxPackages.Web.Mvc.ViewModels.Account
{
    using Data.Models;
    using LinuxPackages.Web.Mvc.Infrastructure.Mappings;

    public class ListedUserViewModel : IMapFrom<User>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }
    }
}