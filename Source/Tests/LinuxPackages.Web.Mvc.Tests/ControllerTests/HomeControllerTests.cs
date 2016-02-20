namespace LinuxPackages.Web.Mvc.Tests.ControllerTests
{
    using System.Linq;
    using App_Start;
    using Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Data.Contracts;
    using Setups;
    using TestStack.FluentMVCTesting;
    using ViewModels.Home;

    [TestClass]
    public class HomeControllerTests
    {
        private AutoMapperConfig automapperConfig;
        private IPackagesService packages = Services.GetPackagesService();
        private IIssuesService issues = Services.GetIssuesService();
        private IUsersService users = Services.GetUsersService();
        private HomeController controller;

        [TestInitialize]
        public void InitializeController()
        {
            this.automapperConfig = new AutoMapperConfig();
            this.automapperConfig.Execute(typeof(HomeController).Assembly);

            this.packages = Services.GetPackagesService();
            this.issues = Services.GetIssuesService();
            this.users = Services.GetUsersService();

            this.controller = new HomeController(this.packages, this.issues, this.users);
        }

        [TestCleanup]
        public void CleanUpController()
        {
            this.automapperConfig = null;

            this.packages = null;
            this.issues = null;
            this.users = null;

            this.controller.Dispose();
            this.controller = null;
        }

        [TestMethod]
        public void IndexShouldDisplayTheCorrectAmountOfPackages()
        {
            this.controller.WithCallTo(c => c.Index())
                .ShouldRenderView("Index")
                .WithModel<IndexViewModel>(
                    viewModel =>
                    {
                        Assert.AreEqual(this.packages.GetAll().Count(), viewModel.PackagesCount);
                    })
                .AndNoModelErrors();
        }

        [TestMethod]
        public void IndexShouldDisplayTheCorrectAmountOfIssues()
        {
            this.controller.WithCallTo(c => c.Index())
                .ShouldRenderView("Index")
                .WithModel<IndexViewModel>(
                    viewModel =>
                    {
                        Assert.AreEqual(this.issues.GetAll().Count(), viewModel.IssuesCount);
                    })
                .AndNoModelErrors();
        }

        [TestMethod]
        public void IndexShouldDisplayTheCorrectAmountOfMaintainers()
        {
            this.controller.WithCallTo(c => c.Index())
                .ShouldRenderView("Index")
                .WithModel<IndexViewModel>(
                    viewModel =>
                    {
                        Assert.AreEqual(this.users.GetAll().Count(), viewModel.MaintainersCount);
                    })
                .AndNoModelErrors();
        }
    }
}