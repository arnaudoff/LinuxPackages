namespace LinuxPackages.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Collections.Generic;

    public sealed class Configuration : DbMigrationsConfiguration<LinuxPackagesDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LinuxPackagesDbContext context)
        {
            new DataSeeder(context).Seed();
        }
    }
}