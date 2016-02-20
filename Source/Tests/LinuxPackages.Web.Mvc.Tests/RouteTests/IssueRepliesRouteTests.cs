namespace LinuxPackages.Web.Mvc.Tests.RouteTests
{
    using System.Net.Http;
    using System.Web.Routing;
    using LinuxPackages.Web.Mvc.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MvcRouteTester;

    [TestClass]
    public class IssueRepliesRouteTests
    {
        private static readonly RouteCollection RouteCollection = new RouteCollection();

        [ClassInitialize]
        public static void RegisterRouteCollection(TestContext context)
        {
            RouteConfig.RegisterRoutes(RouteCollection);
        }

        [TestMethod]
        public void ReplyByIssueIdActionShouldMapCorrectly()
        {
            const string ReplyByIssueIdUrl = "/IssueReplies/ByIssueId/28h9s259a";
            RouteCollection
                .ShouldMap(ReplyByIssueIdUrl)
                .To<IssueRepliesController>(c => c.ByIssueId("28h9s259a"));
        }

        [TestMethod]
        public void AddReplyActionGetShouldMapCorrectly()
        {
            const string AddReplyByIssueIdUrl = "/IssueReplies/Add/28h9s259a";
            RouteCollection
                .ShouldMap(AddReplyByIssueIdUrl)
                .To<IssueRepliesController>(c => c.Add("28h9s259a"));
        }

        [TestMethod]
        public void AddReplyActionPostShouldMapCorrectly()
        {
            const string AddReplyByIssueIdUrl = "/IssueReplies/Add/28h9s259a";
            RouteCollection
                .ShouldMap(AddReplyByIssueIdUrl)
                .To<IssueRepliesController>(HttpMethod.Post, c => c.Add(null, "28h9s259a"));
        }
    }
}