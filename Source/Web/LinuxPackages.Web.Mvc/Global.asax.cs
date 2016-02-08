namespace LinuxPackages.Web.Mvc
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Infrastructure.Helpers;
    using Common.Constants;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            HttpContext.Current.Application[GlobalConstants.UrlSaltKeyName] = QueryStringUrlHelper.GenerateUrlSalt(10);

            AreaRegistration.RegisterAllAreas();
            DatabaseConfig.Initialize();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
