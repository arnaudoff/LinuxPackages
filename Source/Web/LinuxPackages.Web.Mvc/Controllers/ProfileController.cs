namespace LinuxPackages.Web.Mvc.Controllers
{
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using ViewModels.Profile;

    public enum EditProfileResultType
    {
        UpdateProfileSuccess,
        ChangePasswordSuccess,
    }

    [Authorize]
    public partial class ProfileController : Controller
    {
        private const string XsrfKey = "XsrfId";

        private ApplicationSignInManager signInManager;
        private ApplicationUserManager userManager;

        public ProfileController()
        {
        }

        public ProfileController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this.signInManager ?? this.HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                this.signInManager = value;
            }
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

        public async Task<ActionResult> Index(EditProfileResultType? message)
        {
            this.ViewBag.StatusMessage = string.Empty;

            if (message == EditProfileResultType.ChangePasswordSuccess)
            {
                this.ViewBag.StatusMessage = "Your password has been changed.";
            }
            else if (message == EditProfileResultType.UpdateProfileSuccess)
            {
                this.ViewBag.StatusMessage = "Your profile has been updated.";
            }

            var user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
            var userProfile = Mapper.Map<ProfileViewModel>(user);

            return this.View(userProfile);
        }

        public ActionResult ChangePassword()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            IdentityResult result = await this.UserManager.ChangePasswordAsync(this.User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                User user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
                if (user != null)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                return this.RedirectToAction("Index", new { Message = EditProfileResultType.ChangePasswordSuccess });
            }

            this.AddErrors(result);
            return this.View(model);
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error);
            }
        }
    }
}