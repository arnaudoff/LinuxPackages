namespace LinuxPackages.Web.Mvc.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Common.Constants;

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public class HomeController
    {
    }
}