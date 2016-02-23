namespace LinuxPackages.Web.Mvc.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
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
                Categories = this.GetCategories(),
                Repositories = this.GetRepositories(),
                Architectures = this.GetArchitectures(),
                Licenses = this.GetLicenses(),
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
                model.Categories = this.GetCategories();
                model.Repositories = this.GetRepositories();
                model.Architectures = this.GetArchitectures();
                model.Licenses = this.GetLicenses();
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
                model.CategoryId,
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

        public ActionResult Filter(string text)
        {
            if (text == null || text.Length < 2)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "The text length should be equal to or more than 2 characters.");
            }

            var result = this.packages
                .GetAll()
                .Where(p => p.Name.Contains(text))
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })
                .ToList();

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

        private IEnumerable<SelectListItem> GetCategories()
        {
            IEnumerable<SelectListItem> categories = HttpRuntime
                .Cache
                .GetOrStore<IEnumerable<SelectListItem>>(
                    "categories",
                    () => this.packages
                        .GetAllCategories()
                        .Select(c => new SelectListItem
                        {
                            Value = c.Id.ToString(),
                            Text = c.Name
                        })
                        .ToList());

            return categories;
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