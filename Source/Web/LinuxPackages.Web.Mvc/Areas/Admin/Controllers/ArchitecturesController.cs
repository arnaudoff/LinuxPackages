namespace LinuxPackages.Web.Mvc.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Mappings;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;
    using Mvc.Controllers;
    using Services.Data.Contracts;

    public class ArchitecturesController : BaseController
    {
        private readonly IArchitecturesService architectures;

        public ArchitecturesController(IArchitecturesService architectures)
        {
            this.architectures = architectures;
        }

        public ActionResult Manage()
        {
            return this.View();
        }

        public ActionResult GetArchitectures([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.architectures
                .GetAll()
                .To<AdminListedArchitectureViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateArchitecture([DataSourceRequest]DataSourceRequest request, ArchitectureInputModel arch)
        {
            var newId = 0;

            if (this.ModelState.IsValid)
            {
                var newLicense = this.architectures.Create(arch.Name);
                newId = newLicense.Id;
            }

            var archToDisplay = this.architectures
                .GetAll()
                .To<AdminListedArchitectureViewModel>()
                .FirstOrDefault(x => x.Id == newId);

            return this.Json(new[] { archToDisplay }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateArchitecture([DataSourceRequest]DataSourceRequest request, ArchitectureInputModel arch)
        {
            if (this.ModelState.IsValid)
            {
                this.architectures.Update(arch);
            }

            var archToDisplay = this.architectures
                            .GetAll()
                            .To<AdminListedArchitectureViewModel>()
                            .FirstOrDefault(x => x.Id == arch.Id);

            return this.Json(new[] { archToDisplay }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteArchitecture([DataSourceRequest]DataSourceRequest request, ArchitectureInputModel arch)
        {
            this.architectures.DeleteById(arch.Id);
            return this.Json(new[] { arch }.ToDataSourceResult(request, this.ModelState));
        }
    }
}