namespace LinuxPackages.Services.Data.Contracts
{
    using LinuxPackages.Data.Models;
    using System.Linq;

    public interface IUsersService
    {
        IQueryable<User> GetAll();
    }
}
