
namespace LinuxPackages.Services.Data.Contracts
{
    using System.Linq;

    using LinuxPackages.Data.Models;

    public interface IUsersService
    {
        IQueryable<User> GetTopMaintainers(int n);
    }
}