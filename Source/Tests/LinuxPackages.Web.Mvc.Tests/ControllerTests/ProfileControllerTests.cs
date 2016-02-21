namespace LinuxPackages.Web.Mvc.Tests.ControllerTests
{
    using System.Web.Mvc;
    using LinuxPackages.Web.Mvc.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ViewModels.Profile;

    [TestClass]
    public class ProfileControllerTests
    {
        [TestMethod]
        public void ProfileIndexShouldHaveTheCorrectModelPassed()
        {
            var controller = new ProfileController();
            var result = controller.Index(null) as ViewResult;
            var profileModel = (ProfileViewModel)result.ViewData.Model;
        }
    }
}