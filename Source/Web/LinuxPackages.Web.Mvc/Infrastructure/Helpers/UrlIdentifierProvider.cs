namespace LinuxPackages.Web.Mvc.Infrastructure.Helpers
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;

    using Common.Constants;
    using Contracts;

    public class UrlIdentifierProvider : IUrlIdentifierProvider
    {
        public string GenerateIdentifierProviderSalt(int size)
        {
            var random = new RNGCryptoServiceProvider();
            var buffer = new byte[size];
            random.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        public string EncodeEntityId(int entityId)
        {
            string plainEntityId = entityId.ToString();
            string saltedEntityId = string.Concat(plainEntityId, (string)HttpContext.Current.Application[GlobalConstants.UrlSaltKeyName]);
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                byte[] urlHash = sha1.ComputeHash(Encoding.UTF8.GetBytes(saltedEntityId));
                var hashBuilder = new StringBuilder(urlHash.Length * 2);

                foreach (byte hashByte in urlHash)
                {
                    hashBuilder.Append(hashByte.ToString("x2"));
                }

                var resultHash = new string(hashBuilder.ToString().Take(GlobalConstants.UrlHashLength).ToArray());
                return string.Concat(plainEntityId, resultHash);
            }
        }

        public int DecodeEntityId(string urlHash)
        {
            string entityId = urlHash.Substring(0, Math.Max(0, urlHash.Length - GlobalConstants.UrlHashLength));
            return int.Parse(entityId);
        }

        public bool IsHashValid(string urlHash)
        {
            int entityId = DecodeEntityId(urlHash);
            return urlHash == EncodeEntityId(entityId);
        }
    }
}