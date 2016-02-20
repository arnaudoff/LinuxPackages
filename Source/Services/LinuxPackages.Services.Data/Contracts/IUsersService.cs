
namespace LinuxPackages.Services.Data.Contracts
{
    using System.Linq;

    using LinuxPackages.Data.Models;

    public interface IUsersService
    {
        IQueryable<User> GetAll();

        IQueryable<User> GetById(string userId);

        IQueryable<User> GetTopMaintainers(int n);

        IQueryable<Avatar> GetAvatarById(int avatarId);
       
        Avatar CreateAvatar(string fileName, byte[] contents, User user);

        string GetAvatarFileNameById(int avatarId);
    }
}