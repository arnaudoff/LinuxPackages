namespace LinuxPackages.Web.Mvc.Tests.InfrastructureTests.Helpers
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using Infrastructure.Helpers;
    using LinuxPackages.Common.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UrlIdentifierProviderTests
    {
        private const string HttpContextSalt = "foobar";

        [TestInitialize]
        public void InitializeContext()
        {
            HttpContext.Current = new HttpContext(new HttpRequest(null, "http://tempuri.org", null), new HttpResponse(null));
            HttpContext.Current.Application[GlobalConstants.UrlSaltKeyName] = HttpContextSalt;
        }

        [TestCleanup]
        public void DisposeContext()
        {
            HttpContext.Current = null;
        }

        [TestMethod]
        public void GenerateSaltShouldGenerateSaltCorrectly()
        {
            int saltSize = 10;

            string result = new UrlIdentifierProvider().GenerateIdentifierProviderSalt(saltSize);
            byte[] source = Convert.FromBase64String(result);

            Assert.AreEqual(saltSize, source.Length);
        }

        [TestMethod]
        public void GenerateUrlHashShouldGenerateCorrectHash()
        {
            var salt = (string)HttpContext.Current.Application[GlobalConstants.UrlSaltKeyName];
            int entityId = 1337;
            string expectedHash = this.HashEntity(entityId, HttpContextSalt);

            string result = new UrlIdentifierProvider().EncodeEntityId(entityId);

            Assert.AreEqual(expectedHash, result);
        }

        [TestMethod]
        public void GetEntityIdFromHashShouldGetTheIdCorrectly()
        {
            int entityId = 1337;
            string urlHash = this.HashEntity(entityId, HttpContextSalt);

            int decodedId = new UrlIdentifierProvider().DecodeEntityId(urlHash);

            Assert.AreEqual(1337, decodedId);
        }

        private string HashEntity(int entityId, string salt)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(entityId.ToString(), salt)));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return string.Concat(entityId.ToString(), new string(sb.ToString().Take(GlobalConstants.UrlHashLength).ToArray()));
            }
        }
    }
}