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
    using ViewModels.Licenses;

    public class LicensesController : BaseController
    {
        private readonly ILicensesService licenses;

        public LicensesController(ILicensesService licenses)
        {
            this.licenses = licenses;
        }

        public ActionResult Manage()
        {
            return this.View();
        }

        public ActionResult All([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.licenses
                .GetAll()
                .To<ListedLicenseViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, LicenseInputModel license)
        {
            var newId = 0;

            if (this.ModelState.IsValid)
            {
                var newLicense = this.licenses.Create(license.Name, license.Description, license.Url);
                newId = newLicense.Id;
            }

            var licenseToDisplay = this.licenses
                .GetAll()
                .To<ListedLicenseViewModel>()
                .FirstOrDefault(x => x.Id == newId);

            return this.Json(new[] { licenseToDisplay }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, LicenseInputModel license)
        {
            if (this.ModelState.IsValid)
            {
                this.licenses.Update(license.Id, license.Name, license.Description, license.Url);
            }

            var licenseToDisplay = this.licenses
                            .GetAll()
                            .To<ListedLicenseViewModel>()
                            .FirstOrDefault(x => x.Id == license.Id);

            return this.Json(new[] { licenseToDisplay }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, ListedLicenseViewModel license)
        {
            this.licenses.DeleteById(license.Id);
            return this.Json(new[] { license }.ToDataSourceResult(request, this.ModelState));
        }
    }
}