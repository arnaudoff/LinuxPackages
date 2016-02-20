namespace LinuxPackages.Web.Mvc.Tests.RouteTests
{
    using System.Net.Http;
    using System.Web.Routing;
    using LinuxPackages.Web.Mvc.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MvcRouteTester;

    [TestClass]
    public class IssuesRouteTests
    {
        private static readonly RouteCollection RouteCollection = new RouteCollection();

        [ClassInitialize]
        public static void RegisterRouteCollection(TestContext context)
        {
            RouteConfig.RegisterRoutes(RouteCollection);
        }

        [TestMethod]
        public void AllIssuesActionShouldMapCorrectly()
        {
            const string AllIssuesUrl = "/Issues/All";
            RouteCollection
                .ShouldMap(AllIssuesUrl)
                .To<IssuesController>(c => c.All());
        }

        [TestMethod]
        public void AddIssueGetActionShouldMapCorrectly()
        {
            const string AddIssueUrl = "/Issues/Add/28h9s259a";
            RouteCollection
                .ShouldMap(AddIssueUrl)
                .To<IssuesController>(c => c.Add("28h9s259a"));
        }

        [TestMethod]
        public void AddIssuePostActionShouldMapCorrectly()
        {
            const string AddIssueUrl = "/Issues/Add/28h9s259a";
            RouteCollection
                .ShouldMap(AddIssueUrl)
                .To<IssuesController>(HttpMethod.Post, c => c.Add(null, "28h9s259a"));
        }

        [TestMethod]
        public void IssueDetailsActionShouldMapCorrectly()
        {
            const string IssueDetailsUrl = "/Issues/Details/28h9s259a";
            RouteCollection
                .ShouldMap(IssueDetailsUrl)
                .To<IssuesController>(c => c.Details("28h9s259a"));
        }
    }
}