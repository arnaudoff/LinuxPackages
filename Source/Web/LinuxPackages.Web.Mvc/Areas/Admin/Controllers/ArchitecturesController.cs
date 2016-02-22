namespace LinuxPackages.Web.Mvc.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Mappings;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Mvc.Controllers;
    using Services.Data.Contracts;
    using ViewModels.Architectures;

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

        public ActionResult All([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.architectures
                .GetAll()
                .To<ListedArchitectureViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ArchitectureInputModel arch)
        {
            var newId = 0;

            if (this.ModelState.IsValid)
            {
                var newArchitecutre = this.architectures.Create(arch.Name);
                newId = newArchitecutre.Id;
            }

            var archToDisplay = this.architectures
                .GetAll()
                .To<ListedArchitectureViewModel>()
                .FirstOrDefault(x => x.Id == newId);

            return this.Json(new[] { archToDisplay }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ArchitectureInputModel arch)
        {
            if (this.ModelState.IsValid)
            {
                this.architectures.Update(arch.Id, arch.Name);
            }

            var archToDisplay = this.architectures
                            .GetAll()
                            .To<ListedArchitectureViewModel>()
                            .FirstOrDefault(x => x.Id == arch.Id);

            return this.Json(new[] { archToDisplay }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, ArchitectureInputModel arch)
        {
            this.architectures.DeleteById(arch.Id);
            return this.Json(new[] { arch }.ToDataSourceResult(request, this.ModelState));
        }
    }
}