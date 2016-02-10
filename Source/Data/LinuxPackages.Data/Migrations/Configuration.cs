namespace LinuxPackages.Data.Migrations
{
    using System.Data.Entity.Migrations;

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