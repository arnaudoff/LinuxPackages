namespace LinuxPackages.Web.Mvc.App_Start
{
    using System.Web;
    using LinuxPackages.Common.Constants;
    using LinuxPackages.Web.Mvc.Infrastructure.Helpers;

    public class IdentifyConfig
    {
        public static void RegisterIdentifiers(HttpContext context)
        {
            context.Application[GlobalConstants.UrlSaltKeyName] =
                new UrlIdentifierProvider(context).GenerateIdentifierProviderSalt(10);
        }
    }
}