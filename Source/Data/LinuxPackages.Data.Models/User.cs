namespace LinuxPackages.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Common.Constants;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Package> packages;
        private ICollection<Issue> issues;
        private ICollection<IssueReply> issueReplies;

        public User()
        {
            this.packages = new HashSet<Package>();
            this.issues = new HashSet<Issue>();
            this.issueReplies = new HashSet<IssueReply>();
        }

        [Required]
        [MinLength(UserConstants.MinFirstNameLength)]
        [MaxLength(UserConstants.MaxFirstNameLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(UserConstants.MinLastNameLength)]
        [MaxLength(UserConstants.MaxLastNameLength)]
        public string LastName { get; set; }

        public virtual ICollection<Package> Packages
        {
            get
            {
                return this.packages;
            }

            set
            {
                this.packages = value;
            }
        }

        public virtual ICollection<Issue> Issues
        {
            get
            {
                return this.issues;
            }

            set
            {
                this.issues = value;
            }
        }

        public virtual ICollection<IssueReply> IssueReplies
        {
            get
            {
                return this.issueReplies;
            }

            set
            {
                this.issueReplies = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            ClaimsIdentity userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
