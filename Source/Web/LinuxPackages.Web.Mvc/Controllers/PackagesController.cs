namespace LinuxPackages.Web.Mvc.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Common.Utilities;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Services.Data.Contracts;
    using ViewModels.Packages;
    using Ninject;
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using AutoMapper.QueryableExtensions;

    public class PackagesController : Controller
    {
        private readonly IPackagesService packages;
        private readonly IArchitecturesService architectures;
        private readonly ILicensesService licenses;
        private readonly IRepositoriesService repositories;
        private readonly IDistributionsService distros;
        private readonly IScreenshotsService screenshots;
        private ApplicationUserManager userManager;

        public PackagesController(
            IPackagesService packages,
            IArchitecturesService architectures,
            ILicensesService licenses,
            IRepositoriesService repositories,
            IDistributionsService distros,
            IScreenshotsService screenshots)
        {
            this.packages = packages;
            this.architectures = architectures;
            this.licenses = licenses;
            this.repositories = repositories;
            this.distros = distros;
            this.screenshots = screenshots;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                this.userManager = value;
            }
        }

        public ActionResult All()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        [Authorize]
        public ActionResult Add()
        {
            // TODO: Cache
            var repos = this.repositories
                .GetAll()
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                }).AsEnumerable();

            var archs = this.architectures
                .GetAll()
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                }).AsEnumerable();

            var licenses = this.licenses
                .GetAll()
                .Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.Name
                }).AsEnumerable();

            var dependencies = this.packages
                .GetAll()
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }).AsEnumerable();

            var maintainers = this.UserManager.Users
                .Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.UserName
                }).AsEnumerable();

            var distros = this.distros
                .GetAll()
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).AsEnumerable();

            var model = new AddPackageViewModel
            {
                Repositories = repos,
                Architectures = archs,
                Licenses = licenses,
                Maintainers = maintainers,
                Dependencies = dependencies,
                Distributions = distros
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddPackageViewModel model)
        {
            // TODO: Fix the validations & rebind
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUserId = this.User.Identity.GetUserId();
            if (!model.MaintainerIds.Contains(currentUserId))
            {
                model.MaintainerIds.Add(currentUserId);
            }

            var newPackage = this.packages.Create(
                model.Name,
                model.Description,
                model.DistributionId,
                model.RepositoryId,
                model.ArchitectureId,
                model.LicenseId,
                model.Contents.FileName,
                StreamHelper.ReadFully(model.Contents.InputStream, model.Contents.ContentLength),
                model.DependencyIds,
                model.MaintainerIds);

            if (newPackage == null)
            {
                this.ModelState.AddModelError(string.Empty, "Could not create new package.");
                return View(model);
            }

            foreach (var modelScreenshot in model.Screenshots)
            {
                Screenshot screen = this.screenshots.Create(modelScreenshot.FileName, StreamHelper.ReadFully(modelScreenshot.InputStream, modelScreenshot.ContentLength), newPackage.Id, newPackage.Name);
                if (screen == null)
                {
                    this.ModelState.AddModelError(string.Empty, "Could not create screenshot.");
                    return View(model);
                }
            }

            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult GetPackages([DataSourceRequest]DataSourceRequest request)
        {
            var packages = this.packages.GetAll();
            var packagesViewModel = packages.ProjectTo<ListedPackageViewModel>();
            var results = packagesViewModel.ToDataSourceResult(request);
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.userManager != null)
            {
                this.userManager.Dispose();
                this.userManager = null;
            }

            base.Dispose(disposing);
        }
    }
}