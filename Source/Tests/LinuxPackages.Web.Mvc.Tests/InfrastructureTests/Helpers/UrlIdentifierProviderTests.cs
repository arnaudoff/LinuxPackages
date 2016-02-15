namespace LinuxPackages.Web.Mvc.Tests.InfrastructureTests.Helpers
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    using Infrastructure.Helpers;
    using LinuxPackages.Common.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UrlIdentifierProviderTests
    {
        [TestMethod]
        public void GenerateSaltShouldGenerateSaltCorrectly()
        {
            int saltSize = 10;

            string result = (new UrlIdentifierProvider()).GenerateIdentifierProviderSalt(saltSize);
            byte[] source = Convert.FromBase64String(result);

            Assert.AreEqual(saltSize, source.Length);
        }

        [TestMethod]
        public void GenerateUrlHashShouldGenerateCorrectHash()
        {
            // TODO: Mock application context
            int entityId = 1337;
            string expectedHash = HashEntity(entityId, "");

            string result = (new UrlIdentifierProvider()).EncodeEntityId(entityId);

            Assert.AreEqual(GlobalConstants.UrlHashLength, result.Length);
            Assert.AreEqual(expectedHash, result);
        }

        [TestMethod]
        public void GetEntityIdFromHashShouldGetTheIdCorrectly()
        {
            // TODO: Mock application context
            int entityId = 1337;
            string urlHash = HashEntity(entityId, "");

            int decodedId = (new UrlIdentifierProvider()).DecodeEntityId(urlHash);

            Assert.AreEqual(1337, decodedId);
        }

        private string HashEntity(int entityId, string salt)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(entityId.ToString(), salt)));
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
