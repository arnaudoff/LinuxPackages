namespace LinuxPackages.Web.Mvc.Tests.RouteTests
{
    using System.Net.Http;
    using System.Web.Routing;
    using LinuxPackages.Web.Mvc.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MvcRouteTester;

    [TestClass]
    public class ProfileRouteTests
    {
        private static readonly RouteCollection RouteCollection = new RouteCollection();

        [ClassInitialize]
        public static void RegisterRouteCollection(TestContext context)
        {
            RouteConfig.RegisterRoutes(RouteCollection);
        }

        [TestMethod]
        public void ProfileIndexActionShouldMapCorrectly()
        {
            const string AllIssuesUrl = "/Profile";
            RouteCollection
                .ShouldMap(AllIssuesUrl)
                .To<ProfileController>(c => c.Index(null));
        }

        [TestMethod]
        public void ProfileChangePasswordGetActionShouldMapCorrectly()
        {
            const string ChangePasswordUrl = "/Profile/ChangePassword";
            RouteCollection
                .ShouldMap(ChangePasswordUrl)
                .To<ProfileController>(c => c.ChangePassword());
        }

        [TestMethod]
        public void ProfileChangePasswordPostActionShouldMapCorrectly()
        {
            const string ChangePasswordUrl = "/Profile/ChangePassword";
            RouteCollection
                .ShouldMap(ChangePasswordUrl)
                .To<ProfileController>(HttpMethod.Post, c => c.ChangePassword(null));
        }

        [TestMethod]
        public void ProfileChangeAvatarGetActionShouldMapCorrectly()
        {
            const string ChangeAvatarUrl = "/Profile/ChangeAvatar";
            RouteCollection
                .ShouldMap(ChangeAvatarUrl)
                .To<ProfileController>(c => c.ChangeAvatar());
        }

        [TestMethod]
        public void ProfileChangeAvatarPostActionShouldMapCorrectly()
        {
            const string ChangeAvatarUrl = "/Profile/ChangeAvatar";
            RouteCollection
                .ShouldMap(ChangeAvatarUrl)
                .To<ProfileController>(HttpMethod.Post, c => c.ChangeAvatar(null));
        }
    }
}