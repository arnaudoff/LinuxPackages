namespace LinuxPackages.Data
{
    using System;
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class LinuxPackagesDbContext : IdentityDbContext<User>, ILinuxPackagesDbContext
    {
        public LinuxPackagesDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Package> Packages { get; set; }

        public virtual IDbSet<PackageRating> PackageRatings { get; set; }

        public virtual IDbSet<PackageComment> PackageComments { get; set; }

        public virtual IDbSet<Screenshot> Screenshots { get; set; }

        public virtual IDbSet<Issue> Issues { get; set; }

        public virtual IDbSet<IssueReply> IssueReplies { get; set; }

        public virtual IDbSet<Repository> Repositories { get; set; }

        public static LinuxPackagesDbContext Create()
        {
            return new LinuxPackagesDbContext();
        }
    }
}