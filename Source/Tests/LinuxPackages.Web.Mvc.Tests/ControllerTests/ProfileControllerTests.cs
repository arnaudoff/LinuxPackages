namespace LinuxPackages.Web.Mvc.Tests.ControllerTests
{
    using System.Web.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LinuxPackages.Web.Mvc.Controllers;
    using ViewModels.Profile;

    [TestClass]
    public class ProfileControllerTests
    {
        [TestMethod]
        public async void ProfileIndexShouldHaveTheCorrectModelPassed()
        {
            // TODO: Mock http://stackoverflow.com/questions/758066/how-to-mock-controller-user-using-moq
            var controller = new ProfileController();
            var result = await controller.Index(null) as ViewResult;
            var profileModel = (ProfileViewModel)result.ViewData.Model;
        }
    }
}