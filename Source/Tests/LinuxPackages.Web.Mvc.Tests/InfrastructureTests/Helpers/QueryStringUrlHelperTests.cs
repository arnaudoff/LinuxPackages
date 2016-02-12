
namespace LinuxPackages.Web.Mvc.Tests.InfrastructureTests.Helpers
{
    using Infrastructure.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LinuxPackages.Common.Constants;
    using System.Security.Cryptography;
    using System.Text;
    using System.Linq;
    using System;

    [TestClass]
    public class QueryStringUrlHelperTests
    {
        [TestMethod]
        public void GenerateSaltShouldGenerateSaltCorrectly()
        {
            int saltSize = 10;

            string result = QueryStringUrlHelper.GenerateUrlSalt(saltSize);
            byte[] source = Convert.FromBase64String(result);

            Assert.AreEqual(saltSize, source.Length);
        }

        [TestMethod]
        public void GenerateUrlHashShouldGenerateCorrectHash()
        {
            string entityId = "1337";
            string salt = "hello";
            string expectedHash = HashEntity(entityId, salt);

            string result = QueryStringUrlHelper.GenerateUrlHash(entityId, salt);

            Assert.AreEqual(GlobalConstants.UrlHashLength, result.Length);
            Assert.AreEqual(expectedHash, result);
        }

        [TestMethod]
        public void GetEntityIdFromHashShouldGetTheIdCorrectly()
        {
            string entityId = "1337";
            string salt = "hello";
            string urlHash = HashEntity(entityId, salt);

            string result = QueryStringUrlHelper.GetEntityIdFromUrlHash(string.Concat(entityId, urlHash));

            Assert.AreEqual("1337", result);
        }

        private string HashEntity(string entityId, string salt)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(entityId, salt)));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return new string(sb.ToString().Take(GlobalConstants.UrlHashLength).ToArray());
            }
        }
    }
}
