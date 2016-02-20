namespace LinuxPackages.Web.Mvc.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Common.Utilities;
    using Data.Models;
    using Infrastructure.ActionFilters;
    using Infrastructure.Extensions;
    using Infrastructure.Mappings;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Services.Data.Contracts;
    using ViewModels.Packages;

    public class PackagesController : BaseController
    {
        private readonly IPackagesService packages;
        private readonly IArchitecturesService architectures;
        private readonly ILicensesService licenses;
        private readonly IRepositoriesService repositories;
        private readonly IDistributionsService distros;
        private readonly IScreenshotsService screenshots;
        private readonly IDependenciesService dependencies;
        private ApplicationUserManager userManager;

        public PackagesController(
            IPackagesService packages,
            IArchitecturesService architectures,
            ILicensesService licenses,
            IRepositoriesService repositories,
            IDistributionsService distros,
            IScreenshotsService screenshots,
            IDependenciesService dependencies)
        {
            this.packages = packages;
            this.architectures = architectures;
            this.licenses = licenses;
            this.repositories = repositories;
            this.distros = distros;
            this.screenshots = screenshots;
            this.dependencies = dependencies;
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
            return this.View();
        }

        [HashCheck("id")]
        public ActionResult Details(string id)
        {
            int requestedPackageId = this.UrlIdentifierProvider.DecodeEntityId(id);

            var packageModel = this.packages
                .GetById(requestedPackageId)
                .To<PackageDetailsViewModel>()
                .FirstOrDefault();

            packageModel.Dependencies = this.dependencies.GetAllById(requestedPackageId)
                .To<ListedPackageViewModel>()
                .ToList();

            return this.View(packageModel);
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

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddPackageViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Repositories = this.GetRepositories();
                model.Architectures = this.GetArchitectures();
                model.Licenses = this.GetLicenses();
                model.Maintainers = this.GetMaintainers();
                model.Dependencies = this.GetDependencies();
                model.Distributions = this.GetDistributions();

                return this.View(model);
            }

            string currentUserId = this.User.Identity.GetUserId();
            if (!model.MaintainerIds.Contains(currentUserId))
            {
                model.MaintainerIds.Add(currentUserId);
            }

            Package newPackage = this.packages.Create(
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

            if (model.Screenshots != null)
            {
                foreach (var modelScreenshot in model.Screenshots)
                {
                    this.screenshots.Create(
                        modelScreenshot.FileName,
                        StreamHelper.ReadFully(modelScreenshot.InputStream, modelScreenshot.ContentLength),
                        newPackage.Id);
                }
            }

            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult GetPackages([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.packages
                .GetAll()
                .To<ListedPackageViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Rate(RatePackageViewModel model)
        {
            if (!this.UrlIdentifierProvider.IsHashValid(model.PackageId))
            {
                return new HttpNotFoundResult("The requested package was not found.");
            }

            int requestedPackageId = this.UrlIdentifierProvider.DecodeEntityId(model.PackageId);

            Rating rating = this.packages.AddRating(model.Value, requestedPackageId, this.User.Identity.GetUserId());

            if (rating != null)
            {
                return this.Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new { Success = false }, JsonRequestBehavior.AllowGet);
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
            IEnumerable<SelectListItem> repos = HttpRuntime
                .Cache
                .GetOrStore<IEnumerable<SelectListItem>>(
                    "repositories",
                    () => this.repositories
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
            IEnumerable<SelectListItem> archs = HttpRuntime
                .Cache
                .GetOrStore<IEnumerable<SelectListItem>>(
                    "architectures",
                    () => this.architectures
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
            IEnumerable<SelectListItem> licenses = HttpRuntime
                .Cache
                .GetOrStore<IEnumerable<SelectListItem>>(
                    "licenses",
                    () => this.licenses
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
            IEnumerable<SelectListItem> dependencies = HttpRuntime
                .Cache
                .GetOrStore<IEnumerable<SelectListItem>>(
                    "dependencies",
                    () => this.packages
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
            IEnumerable<SelectListItem> maintainers = HttpRuntime
                .Cache
                .GetOrStore<IEnumerable<SelectListItem>>(
                    "maintainers",
                    () => this.UserManager
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
            IEnumerable<SelectListItem> distros = HttpRuntime
                .Cache
                .GetOrStore<IEnumerable<SelectListItem>>(
                    "distros",
                    () => this.distros
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