namespace LinuxPackages.Web.Mvc.Infrastructure.Helpers
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Web.Security;
    using Common.Constants;
    using System.Web;

    public static class QueryStringUrlHelper
    {
        public static string GenerateUrlSalt(int size)
        {
            var random = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            random.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string GenerateUrlHash(string param, string salt)
        {
            string saltedParam = string.Concat(param, salt);
            string urlHash = FormsAuthentication.HashPasswordForStoringInConfigFile(saltedParam, "sha1");
            return new string(urlHash.Take(GlobalConstants.UrlHashLength).ToArray()); ;
        }

        public static bool IsHashValid(string urlHash)
        {
            string initialHash = urlHash.Substring(Math.Max(0, urlHash.Length - GlobalConstants.UrlHashLength));
            string parsedEntityId = GetEntityIdFromUrlHash(urlHash);

            string generatedHash = GenerateUrlHash(parsedEntityId, (string)HttpContext.Current.Application[GlobalConstants.UrlSaltKeyName]);

            return generatedHash == initialHash;
        }

        public static string GetEntityIdFromUrlHash(string urlHash)
        {
            return urlHash.Substring(0, Math.Max(0, urlHash.Length - GlobalConstants.UrlHashLength));
        }
    }
}