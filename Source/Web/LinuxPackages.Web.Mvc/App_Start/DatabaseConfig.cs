﻿namespace LinuxPackages.Web.Mvc
{
    using System.Data.Entity;

    using Data;
    using Data.Migrations;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LinuxPackagesDbContext, Configuration>());
        }
    }
}