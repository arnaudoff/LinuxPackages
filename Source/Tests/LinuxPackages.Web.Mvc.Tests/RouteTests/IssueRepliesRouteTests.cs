namespace LinuxPackages.Web.Mvc.Tests.RouteTests
{
    using LinuxPackages.Web.Mvc.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MvcRouteTester;
    using System.Net.Http;
    using System.Web.Routing;

    [TestClass]
    public class IssueRepliesRouteTests
    {
        [TestMethod]
        public void ReplyByIssueIdActionShouldMapCorrectly()
        {
            const string ReplyByIssueIdUrl = "/IssueReplies/ByIssueId/28h9s259a";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection
                .ShouldMap(ReplyByIssueIdUrl)
                .To<IssueRepliesController>(c => c.ByIssueId("28h9s259a"));
        }

        [TestMethod]
        public void AddReplyActionGetShouldMapCorrectly()
        {
            const string AddReplyByIssueIdUrl = "/IssueReplies/Add/28h9s259a";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection
                .ShouldMap(AddReplyByIssueIdUrl)
                .To<IssueRepliesController>(c => c.Add("28h9s259a"));
        }

        [TestMethod]
        public void AddReplyActionPostShouldMapCorrectly()
        {
            const string AddReplyByIssueIdUrl = "/IssueReplies/Add/28h9s259a";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection
                .ShouldMap(AddReplyByIssueIdUrl)
                .To<IssueRepliesController>(HttpMethod.Post, c => c.Add(null, "28h9s259a"));
        }
    }
}