namespace LinuxPackages.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class LinuxPackagesDbContext : IdentityDbContext<User>, ILinuxPackagesDbContext
    {
        public LinuxPackagesDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static LinuxPackagesDbContext Create()
        {
            return new LinuxPackagesDbContext();
        }
    }
}