namespace LinuxPackages.Web.Mvc.Tests.InfrastructureTests.Helpers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using Infrastructure.Helpers;
    using LinuxPackages.Common.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Http.TestLibrary;

    [TestClass]
    public class UrlIdentifierProviderTests
    {
        private HttpContext httpContext = null;
        private const string HttpContextSalt = "foobar";

        [TestInitialize]
        public void InitializeContext()
        {
            using (HttpSimulator simulator = new HttpSimulator())
            {
                simulator.SimulateRequest(new Uri("http://localhost/"));
                HttpContext.Current.Application.Add(GlobalConstants.UrlSaltKeyName, HttpContextSalt);
                this.httpContext = HttpContext.Current;
            }
        }

        [TestCleanup]
        public void DisposeContext()
        {
            this.httpContext = null;
        }

        [TestMethod]
        public void GenerateSaltShouldGenerateSaltCorrectly()
        {
            int saltSize = 10;

            string result = new UrlIdentifierProvider(this.httpContext).GenerateIdentifierProviderSalt(saltSize);
            byte[] source = Convert.FromBase64String(result);

            Assert.AreEqual(saltSize, source.Length);
        }

        [TestMethod]
        public void GenerateUrlHashShouldGenerateCorrectHash()
        {
            int entityId = 1337;
            string expectedHash = this.HashEntity(entityId, HttpContextSalt);

            string result = new UrlIdentifierProvider(this.httpContext).EncodeEntityId(entityId);

            Assert.AreEqual(expectedHash, result);
        }

        [TestMethod]
        public void GetEntityIdFromHashShouldGetTheIdCorrectly()
        {
            int entityId = 1337;
            string urlHash = this.HashEntity(entityId, HttpContextSalt);

            int decodedId = new UrlIdentifierProvider(this.httpContext).DecodeEntityId(urlHash);

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