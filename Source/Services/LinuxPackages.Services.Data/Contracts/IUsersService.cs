
namespace LinuxPackages.Services.Data.Contracts
{
    using System.Linq;

    using LinuxPackages.Data.Models;

    public interface IUsersService
    {
        IQueryable<User> GetAll();

        IQueryable<User> GetById(string userId);

        IQueryable<User> GetTopMaintainers(int n);

        Avatar CreateAvatar(string fileName, byte[] contents);

        string GetAvatarFileNameById(int avatarId);
    }
}