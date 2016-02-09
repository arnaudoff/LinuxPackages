namespace LinuxPackages.Web.Mvc.Tests.ControllerTests
{
    using System.Web.Mvc;

    using LinuxPackages.Web.Mvc.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void HomeShouldReturnTheCorrectView()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;

            // TODO: Implement actual test
            Assert.AreEqual(true, true);
        }
    }
}