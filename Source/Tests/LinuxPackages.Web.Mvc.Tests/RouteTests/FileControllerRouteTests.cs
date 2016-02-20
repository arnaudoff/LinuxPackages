namespace LinuxPackages.Web.Mvc.Tests.RouteTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System.Web.Routing;

    using MvcRouteTester;
    using Controllers;

    [TestClass]
    public class FileControllerRouteTests
    {
        [TestMethod]
        public void ScreenshotsShouldMapCorrectly()
        {
            const string ScreenshotUrl = "/Files/Screenshots/2943urjf/248hdusaa/100x200";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection
                .ShouldMap(ScreenshotUrl)
                .To<FilesController>(c => c.Screenshots("2943urjf", "248hdusaa", "100x200"));
        }

        [TestMethod]
        public void AvatarsShouldMapCorrectly()
        {
            const string AvatarUrl = "/Files/Avatars/y332sd3467asda8sdh6865asuda/28h9s259a/100x100";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection
                .ShouldMap(AvatarUrl)
                .To<FilesController>(c => c.Avatars("y332sd3467asda8sdh6865asuda", "28h9s259a", "100x100"));
        }

        [TestMethod]
        public void PackagesShouldMapCorrectly()
        {
            const string AvatarUrl = "/Files/Packages/28h9s259a";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection
                .ShouldMap(AvatarUrl)
                .To<FilesController>(c => c.Packages("28h9s259a"));
        }
    }
}