namespace LinuxPackages.Services.Data
{
    using System.IO;
    using System.Linq;

    using Contracts;
    using Contracts.Savers;

    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;

    public class UsersService : IUsersService
    {
        private readonly IRepository<User> users;
        private readonly IRepository<Avatar> avatars;
        private readonly IAvatarSaver avatarSaver;

        public UsersService(IRepository<User> users, IRepository<Avatar> avatars, IAvatarSaver avatarSaver)
        {
            this.users = users;
            this.avatars = avatars;
            this.avatarSaver = avatarSaver;
        }

        public IQueryable<User> GetAll()
        {
            return this.users.All();
        }

        public IQueryable<User> GetById(string userId)
        {
            return this.users.All().Where(u => u.Id == userId);
        }

        public Avatar CreateAvatar(string fileName, byte[] contents, User user)
        {
            var newAvatar = new Avatar()
            {
                FileName = Path.GetFileNameWithoutExtension(fileName),
                FileExtension = Path.GetExtension(fileName),
                Size = contents.Length,
                UserId = user.Id
            };

            this.avatars.Add(newAvatar);
            user.AvatarId = newAvatar.Id;
            this.users.SaveChanges();
            this.avatars.SaveChanges();
            this.avatarSaver.Save(user.Id, fileName, contents);

            return newAvatar;
        }

        public IQueryable<User> GetTopMaintainers(int n)
        {
            return this.users
                .All()
                .OrderByDescending(u => u.Packages.Count)
                .Take(n);
        }

        public IQueryable<Avatar> GetAvatarById(int avatarId)
        {
            return this.avatars.All().Where(a => a.Id == avatarId);
        }

        public string GetAvatarFileNameById(int avatarId)
        {
            return this.avatars
                .All()
                .Where(a => a.Id == avatarId)
                .Select(a => string.Concat(a.FileName, a.FileExtension))
                .FirstOrDefault();
        }
    }
}