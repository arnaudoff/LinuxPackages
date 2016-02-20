namespace LinuxPackages.Web.Mvc.Tests.RouteTests
{
    using System.Net.Http;
    using System.Web.Routing;
    using LinuxPackages.Web.Mvc.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MvcRouteTester;

    [TestClass]
    public class CommentsRouteTests
    {
        private static readonly RouteCollection RouteCollection = new RouteCollection();

        [ClassInitialize]
        public static void RegisterRouteCollection(TestContext context)
        {
            RouteConfig.RegisterRoutes(RouteCollection);
        }

        [TestMethod]
        public void AllCommentsActionShouldMapCorrectly()
        {
            const string AllCommentsUrl = "/Comments/All?packageId=28h9s259a";
            RouteCollection
                .ShouldMap(AllCommentsUrl)
                .To<CommentsController>(c => c.All(null, "28h9s259a"));
        }

        [TestMethod]
        public void AddCommentActionShouldMapCorrectly()
        {
            const string AddCommentUrl = "/Comments/Add?packageId=28h9s259a";
            RouteCollection
                .ShouldMap(AddCommentUrl)
                .To<CommentsController>(HttpMethod.Post, c => c.Add(null, null, "28h9s259a"));
        }
    }
}