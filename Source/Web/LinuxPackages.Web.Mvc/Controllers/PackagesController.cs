namespace LinuxPackages.Web.Mvc.Controllers
{
    using System.Web.Mvc;

    using Services.Data.Contracts;
    using ViewModels.Packages;
    using System.Linq;
    using Microsoft.AspNet.Identity;

    public class PackagesController : Controller
    {
        private readonly IPackagesService packages;
        private readonly IArchitecturesService architectures;
        private readonly ILicensesService licenses;
        private readonly IRepositoriesService repositories;
        private readonly IDistributionsService distros;
        private readonly IUsersService users;

        public PackagesController(
            IPackagesService packages,
            IArchitecturesService architectures,
            ILicensesService licenses,
            IRepositoriesService repositories,
            IDistributionsService distros,
            IUsersService users)
        {
            this.packages = packages;
            this.architectures = architectures;
            this.licenses = licenses;
            this.repositories = repositories;
            this.distros = distros;
            this.users = users;
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
            var repos = this.repositories
                .GetAll()
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                });

            var archs = this.architectures
                .GetAll()
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                });

            var licenses = this.licenses
                .GetAll()
                .Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.Name
                });

            var dependencies = this.packages
                .GetAll()
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                });

            var maintainers = this.users
                .GetAll()
                .Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.UserName
                });

            var distros = this.distros
                .GetAll()
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                });

            string currentUserId = this.User.Identity.GetUserId();

            var model = new AddPackageViewModel
            {
                Repositories = repos,
                Architectures = archs,
                Licenses = licenses,
                Maintainers = maintainers,
                Dependencies = dependencies,
                CurrentUserAsMaintainer = maintainers.Where(u => u.Value == currentUserId).FirstOrDefault(),
                Distributions = distros
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddPackageViewModel model)
        {
            return this.RedirectToAction("Index", "Home");
        }
    }
}