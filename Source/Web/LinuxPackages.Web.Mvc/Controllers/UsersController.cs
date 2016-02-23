namespace LinuxPackages.Web.Mvc.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Kendo.Mvc.Extensions;

    public class UsersController : BaseController
    {
        public ActionResult Filter(string text)
        {
            if (text == null || text.Length < 2)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "The text length should be equal to or more than 2 characters.");
            }

            var result = this.Users
                .GetAll()
                .Where(u => u.UserName.Contains(text))
                .Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.UserName
                })
                .ToList();

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}