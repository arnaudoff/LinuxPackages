namespace LinuxPackages.Web.Mvc.Tests.Setups
{
    using LinuxPackages.Services.Data;
    using LinuxPackages.Services.Data.Contracts;

    internal static class Services
    {
        internal static IPackagesService GetPackagesService()
        {
            return new PackagesService(
                Repositories.GetPackagesRepository(),
                Repositories.GetUsersRepository(),
                Repositories.GetDependenciesRepository(),
                Repositories.GetPackageCommentsRepository(),
                Repositories.GetPackageRatingsRepository(),
                null,
                null);
        }

        internal static IIssuesService GetIssuesService()
        {
            return new IssuesService(
                Repositories.GetIssuesRepository(),
                Repositories.GetIssueRepliesRepository(), null);
        }

        internal static IUsersService GetUsersService()
        {
            return new UsersService(Repositories.GetUsersRepository(), null, null);
        }
    }
}
