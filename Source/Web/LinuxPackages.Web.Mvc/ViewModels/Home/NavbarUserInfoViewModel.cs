namespace LinuxPackages.Web.Mvc.ViewModels.Home
{
    using Data.Models;
    using Infrastructure.Helpers;
    using Infrastructure.Mappings;

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
                return new UrlIdentifierProvider().EncodeEntityId(this.AvatarId.GetValueOrDefault());
            }
        }
    }
}