namespace LinuxPackages.Web.Mvc.ViewModels.Home
{
    using LinuxPackages.Data.Models;
    using LinuxPackages.Web.Mvc.Infrastructure.Helpers;
    using LinuxPackages.Web.Mvc.Infrastructure.Mappings;

    public class NavbarUserInfoViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? AvatarId { get; set; }

        public Avatar Avatar { get; set; }

        public string AvatarUrl
        {
            get
            {
                return (new UrlIdentifierProvider()).EncodeEntityId(this.AvatarId.GetValueOrDefault());
            }
        }
    }
}