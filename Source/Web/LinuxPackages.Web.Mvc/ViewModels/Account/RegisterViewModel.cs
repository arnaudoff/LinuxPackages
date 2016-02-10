namespace LinuxPackages.Web.Mvc.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class RegisterViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(UserConstants.MaxFirstNameLength, ErrorMessage = "The first name must be at least {2} characters long.", MinimumLength = UserConstants.MinFirstNameLength)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(UserConstants.MaxLastNameLength, ErrorMessage = "The last name must be at least {2} characters long.", MinimumLength = UserConstants.MinLastNameLength)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The password must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}