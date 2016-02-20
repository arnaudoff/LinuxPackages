namespace LinuxPackages.Web.Mvc.Tests.RouteTests
{
    using LinuxPackages.Web.Mvc.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MvcRouteTester;
    using System.Net.Http;
    using System.Web.Routing;

    [TestClass]
    public class CommentsControllerTests
    {
        [TestMethod]
        public void AllCommentsActionShouldMapCorrectly()
        {
            const string AllCommentsUrl = "/Comments/All?packageId=28h9s259a";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection
                .ShouldMap(AllCommentsUrl)
                .To<CommentsController>(c => c.All(null, "28h9s259a"));
        }

        [TestMethod]
        public void AddCommentActionShouldMapCorrectly()
        {
            const string AddCommentUrl = "/Comments/Add?packageId=28h9s259a";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection
                .ShouldMap(AddCommentUrl)
                .To<CommentsController>(HttpMethod.Post, c => c.Add(null, null, "28h9s259a"));
        }
    }
}