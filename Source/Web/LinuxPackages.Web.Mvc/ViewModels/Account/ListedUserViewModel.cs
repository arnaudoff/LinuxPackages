namespace LinuxPackages.Web.Mvc.ViewModels.Account
{
    using Data.Models;
    using Infrastructure.Helpers;
    using LinuxPackages.Web.Mvc.Infrastructure.Mappings;

    public class ListedUserViewModel : IMapFrom<User>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public int AvatarId { get; set; }

        public string AvatarUrl
        {
            get
            {
                return (new UrlIdentifierProvider()).EncodeEntityId(this.AvatarId);
            }
        }
    }
}