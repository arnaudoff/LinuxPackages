namespace LinuxPackages.Web.Mvc.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Extensions;
    using Infrastructure.Mappings;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Mvc.Controllers;
    using Services.Data.Contracts;
    using ViewModels.Packages;

    public class PackagesController : BaseController
    {
        private readonly IPackagesService packages;
        private readonly IDistributionsService distros;
        private readonly IRepositoriesService repositories;
        private readonly IArchitecturesService architectures;
        private readonly ILicensesService licenses;

        public PackagesController(
            IPackagesService packages,
            IDistributionsService distros,
            IRepositoriesService repositories,
            IArchitecturesService architectures,
            ILicensesService licenses)
        {
            this.packages = packages;
            this.distros = distros;
            this.repositories = repositories;
            this.architectures = architectures;
            this.licenses = licenses;
        }

        public ActionResult Manage()
        {
            if (this.packages.GetAll().Count() == 0)
            {
                return this.PartialView("_NoEntriesToManagePartial");
            }

            this.ViewData["distros"] = this.GetDistributions();
            this.ViewData["repositories"] = this.GetRepositories();
            this.ViewData["architectures"] = this.GetArchitectures();
            this.ViewData["licenses"] = this.GetLicenses();

            return this.View();
        }

        public ActionResult All([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.packages
                .GetAll()
                .To<ListedPackageViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, UpdatePackageInputModel package)
        {
            if (this.ModelState.IsValid)
            {
                this.packages.Update(package.Id, package.Name, package.DistributionId, package.RepositoryId, package.ArchitectureId, package.LicenseId);
            }

            var packageToDisplay = this.packages
                           .GetAll()
                           .To<ListedPackageViewModel>()
                           .FirstOrDefault(p => p.Id == package.Id);

            return this.Json(new[] { packageToDisplay }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, ListedPackageViewModel package)
        {
            this.packages.DeleteById(package.Id);
            return this.Json(new[] { package }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Export(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);
            return this.File(fileContents, contentType, fileName);
        }

        public ActionResult Statistics()
        {
            var distributionModel = this.packages
                .GetLastMonthUploadDayDistribution()
                .OrderBy(r => r.Key)
                .Select(x => new PackagesUploadDayStatsViewModel { Day = x.Key, Value = x.Value });

            return this.View(distributionModel);
        }

        // TODO: Extract these to a common method
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
    }
}