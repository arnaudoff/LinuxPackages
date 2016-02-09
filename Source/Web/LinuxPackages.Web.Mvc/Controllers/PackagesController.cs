﻿namespace LinuxPackages.Web.Mvc.Controllers
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
    using Infrastructure.Extensions;
    using System;
    using Common.Constants;
    using Infrastructure.Helpers;

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

        public ActionResult Details(string id)
        {
            var packageHash = id.Substring(Math.Max(0, id.Length - GlobalConstants.UrlHashLength));
            var packageId = id.Substring(0, Math.Max(0, id.Length - GlobalConstants.UrlHashLength));

            string packageIdHashed = QueryStringUrlHelper.GenerateUrlHash(
                packageId,
                (string)this.ControllerContext.HttpContext.Application[GlobalConstants.UrlSaltKeyName]);

            if (packageIdHashed != packageHash)
            {
                return new HttpNotFoundResult("The requested package was not found.");
            }

            var packageModel = this.packages
                .GetById(int.Parse(packageId))
                .ProjectTo<PackageDetailsViewModel>()
                .FirstOrDefault();

            return View(packageModel);
        }

        [Authorize]
        public ActionResult Add()
        {
            var model = new AddPackageViewModel
            {
                Repositories = this.GetRepositories(),
                Architectures = this.GetArchitectures(),
                Licenses = this.GetLicenses(),
                Maintainers = this.GetMaintainers(),
                Dependencies = this.GetDependencies(),
                Distributions = this.GetDistributions()
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddPackageViewModel model)
        {
            model.Repositories = this.GetRepositories();
            model.Architectures = this.GetArchitectures();
            model.Licenses = this.GetLicenses();
            model.Maintainers = this.GetMaintainers();
            model.Dependencies = this.GetDependencies();
            model.Distributions = this.GetDistributions();

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

        private IEnumerable<SelectListItem> GetRepositories()
        {
            var repos = HttpRuntime
                .Cache
                .GetOrStore<IEnumerable<SelectListItem>>("repositories", () => this.repositories
                    .GetAll()
                    .Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    })
                    .ToList());

            return repos;
        }

        private IEnumerable<SelectListItem> GetArchitectures()
        {
            var archs = HttpRuntime
                .Cache
                .GetOrStore<IEnumerable<SelectListItem>>("architectures", () => this.architectures
                    .GetAll()
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Name
                    })
                    .ToList());

            return archs;
        }

        private IEnumerable<SelectListItem> GetLicenses()
        {
            var licenses = HttpRuntime
                .Cache
                .GetOrStore<IEnumerable<SelectListItem>>("licenses", () => this.licenses
                    .GetAll()
                    .Select(l => new SelectListItem
                    {
                        Value = l.Id.ToString(),
                        Text = l.Name
                    })
                    .ToList());

            return licenses;
        }

        private IEnumerable<SelectListItem> GetDependencies()
        {
            var dependencies = HttpRuntime
                .Cache
                .GetOrStore<IEnumerable<SelectListItem>>("dependencies", () => this.packages
                    .GetAll()
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = p.Name
                    })
                    .ToList());

            return dependencies;
        }

        private IEnumerable<SelectListItem> GetMaintainers()
        {
            var maintainers = HttpRuntime
                .Cache
                .GetOrStore<IEnumerable<SelectListItem>>("maintainers", () => this.UserManager
                    .Users
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id,
                        Text = u.UserName
                    })
                    .ToList());

            return maintainers;
        }

        private IEnumerable<SelectListItem> GetDistributions()
        {
            var distros = HttpRuntime
                .Cache
                .GetOrStore<IEnumerable<SelectListItem>>("distros", () => this.distros
                    .GetAll()
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.Name + " " + d.Version
                    })
                    .ToList());

            return distros;
        }
    }
}