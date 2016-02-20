namespace LinuxPackages.Web.Mvc.App_Start
{
    using LinuxPackages.Common.Constants;
    using LinuxPackages.Web.Mvc.Infrastructure.Helpers;
    using System.Web;

    public class IdentifyConfig
    {
        public static void RegisterIdentifiers(HttpContext context)
        {
            context.Application[GlobalConstants.UrlSaltKeyName] = 
                (new UrlIdentifierProvider()).GenerateIdentifierProviderSalt(10);
        }
    }
}