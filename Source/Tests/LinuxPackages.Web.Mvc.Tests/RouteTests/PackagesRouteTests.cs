namespace LinuxPackages.Web.Mvc.Tests.RouteTests
{
    using System.Net.Http;
    using System.Web.Routing;
    using LinuxPackages.Web.Mvc.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MvcRouteTester;

    [TestClass]
    public class PackagesRouteTests
    {
        private static readonly RouteCollection RouteCollection = new RouteCollection();

        [ClassInitialize]
        public static void RegisterRouteCollection(TestContext context)
        {
            RouteConfig.RegisterRoutes(RouteCollection);
        }

        [TestMethod]
        public void AllPackagesActionShouldMapCorrectly()
        {
            const string AllPackagesUrl = "/Packages/All";
            RouteCollection
                .ShouldMap(AllPackagesUrl)
                .To<PackagesController>(c => c.All());
        }

        [TestMethod]
        public void PackageDetailsActionShouldMapCorrectly()
        {
            const string PackageDetailsUrl = "/Packages/Details/28h9s259a";
            RouteCollection
                .ShouldMap(PackageDetailsUrl)
                .To<PackagesController>(c => c.Details("28h9s259a"));
        }

        [TestMethod]
        public void AddPackageGetActionShouldMapCorrectly()
        {
            const string AddPackageUrl = "/Packages/Add";
            RouteCollection
                .ShouldMap(AddPackageUrl)
                .To<PackagesController>(HttpMethod.Get, c => c.Add());
        }

        [TestMethod]
        public void AddPackagePostActionShouldMapCorrectly()
        {
            const string AddPackageUrl = "/Packages/Add";
            RouteCollection
                .ShouldMap(AddPackageUrl)
                .To<PackagesController>(HttpMethod.Post, c => c.Add(null));
        }

        [TestMethod]
        public void RatePackageActionShouldMapCorrectly()
        {
            const string RatePackageUrl = "/Packages/Rate";
            RouteCollection
                .ShouldMap(RatePackageUrl)
                .To<PackagesController>(HttpMethod.Post, c => c.Rate(null));
        }
    }
}
