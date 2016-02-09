namespace LinuxPackages.Web.Mvc
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Files",
                url: "Files/{action}/{id}/{resource}",
                defaults: new { controller = "Files", action = UrlParameter.Optional, id = "", resource = "" },
                namespaces: new[] { "LinuxPackages.Web.Mvc.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "LinuxPackages.Web.Mvc.Controllers" });
        }
    }
}
