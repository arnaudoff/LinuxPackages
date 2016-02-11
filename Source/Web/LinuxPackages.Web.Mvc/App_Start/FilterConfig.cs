namespace LinuxPackages.Web.Mvc
{
    using System.Web;
    using System.Web.Mvc;
    using Infrastructure.ActionFilters;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new HashCheck());
        }
    }
}
