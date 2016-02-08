namespace LinuxPackages.Web.Mvc.Infrastructure.Helpers
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Web.Security;
    using Common.Constants;

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
    }
}