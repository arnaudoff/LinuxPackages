namespace LinuxPackages.Web.Mvc.Tests.RouteTests
{
    using System.Web.Routing;
    using Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MvcRouteTester;

    [TestClass]
    public class FilesRouteTests
    {
        private static readonly RouteCollection RouteCollection = new RouteCollection();

        [ClassInitialize]
        public static void RegisterRouteCollection(TestContext context)
        {
            RouteConfig.RegisterRoutes(RouteCollection);
        }

        [TestMethod]
        public void ScreenshotsActionShouldMapCorrectly()
        {
            const string ScreenshotUrl = "/Files/Screenshots/2943urjf/248hdusaa/100x200";
            RouteCollection
                .ShouldMap(ScreenshotUrl)
                .To<FilesController>(c => c.Screenshots("2943urjf", "248hdusaa", "100x200"));
        }

        [TestMethod]
        public void AvatarsActionShouldMapCorrectly()
        {
            const string AvatarUrl = "/Files/Avatars/y332sd3467asda8sdh6865asuda/28h9s259a/100x100";
            RouteCollection
                .ShouldMap(AvatarUrl)
                .To<FilesController>(c => c.Avatars("y332sd3467asda8sdh6865asuda", "28h9s259a", "100x100"));
        }

        [TestMethod]
        public void PackagesActionShouldMapCorrectly()
        {
            const string AvatarUrl = "/Files/Packages/28h9s259a";
            RouteCollection
                .ShouldMap(AvatarUrl)
                .To<FilesController>(c => c.Packages("28h9s259a"));
        }
    }
}