namespace LinuxPackages.Web.Mvc.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Mappings;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Mvc.Controllers;
    using Services.Data.Contracts;
    using ViewModels.Distributions;

    public class DistributionsController : BaseController
    {
        private readonly IDistributionsService distros;

        public DistributionsController(IDistributionsService distros)
        {
            this.distros = distros;
        }

        public ActionResult Manage()
        {
            return this.View();
        }

        public ActionResult All([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.distros
                .GetAll()
                .To<ListedDistributionViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, DistributionInputModel distro)
        {
            var newId = 0;

            if (this.ModelState.IsValid)
            {
                var newDistro = this.distros.Create(distro.Name, distro.Version, distro.Maintainer, distro.Url);
                newId = newDistro.Id;
            }

            var distroToDisplay = this.distros
                .GetAll()
                .To<ListedDistributionViewModel>()
                .FirstOrDefault(x => x.Id == newId);

            return this.Json(new[] { distroToDisplay }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, DistributionInputModel distro)
        {
            if (this.ModelState.IsValid)
            {
                this.distros.Update(distro.Id, distro.Name, distro.Version, distro.Maintainer, distro.Url);
            }

            var distroToDisplay = this.distros
                            .GetAll()
                            .To<ListedDistributionViewModel>()
                            .FirstOrDefault(x => x.Id == distro.Id);

            return this.Json(new[] { distroToDisplay }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, DistributionInputModel distro)
        {
            this.distros.DeleteById(distro.Id);
            return this.Json(new[] { distro }.ToDataSourceResult(request, this.ModelState));
        }
    }
}