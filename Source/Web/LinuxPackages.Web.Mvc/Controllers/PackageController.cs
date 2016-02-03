namespace LinuxPackages.Web.Mvc.Controllers
{
    using System.Web.Mvc;

    public class PackageController : Controller
    {
        public ActionResult All()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }
    }
}