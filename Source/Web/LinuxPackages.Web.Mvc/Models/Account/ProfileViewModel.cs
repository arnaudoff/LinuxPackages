namespace LinuxPackages.Web.Mvc.Models.Account
{
    public class ProfileViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int PackagesUploadedCount { get; set; }

        public int IssuesCreatedCount { get; set; }
    }
}