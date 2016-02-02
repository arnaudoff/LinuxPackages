namespace LinuxPackages.Web.Mvc.Models.Account
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }
}