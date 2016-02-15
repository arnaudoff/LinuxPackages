namespace LinuxPackages.Services.Data.Savers
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;

    using Common.Constants;
    using Common.Utilities;
    using Contracts;
    using LinuxPackages.Services.Data.Contracts.Savers;

    public class HardDriveAvatarSaver : IAvatarSaver
    {
        private readonly string rootPath;
        private readonly IUsersService users;

        public HardDriveAvatarSaver(string rootPath, IUsersService users)
        {
            this.rootPath = rootPath;
            this.users = users;
        }

        public byte[] Read(string userId, int avatarId, int? width = null, int? height = null)
        {
            string filePath = this.GetAvatarPath(userId, avatarId, width, height);
            return File.ReadAllBytes(filePath);
        }

        public void Save(string userId, string avatarFileName, byte[] contents)
        {
            // Save original avatar
            string directoryToSavePath = Path.Combine(this.rootPath, userId);
            if (!Directory.Exists(directoryToSavePath))
            {
                Directory.CreateDirectory(directoryToSavePath);
            }

            string finalPath = Path.Combine(directoryToSavePath, avatarFileName);
            File.WriteAllBytes(finalPath, contents);

            // Save the thumbnail
            var thumbnailsFolderPath = Path.Combine(
                            directoryToSavePath,
                            string.Format("{0}x{1}", UserConstants.AvatarThumbnailWidth, UserConstants.AvatarThumbnailHeight));

            if (!Directory.Exists(thumbnailsFolderPath))
            {
                Directory.CreateDirectory(thumbnailsFolderPath);
            }

            using (var memoryStream = new MemoryStream(contents))
            {
                using (var image = Image.FromStream(memoryStream))
                {
                    using (var newImage = ImageUtils.ScaleImage(image, UserConstants.AvatarThumbnailWidth, UserConstants.AvatarThumbnailHeight))
                    {
                        newImage.Save(Path.Combine(thumbnailsFolderPath, avatarFileName), ImageFormat.Png);
                    }
                }
            }
        }

        private string GetAvatarPath(string userId, int avatarId, int? width, int? height)
        {
            if (width == null || height == null)
            {
                return Path.Combine(this.rootPath, userId, this.users.GetAvatarFileNameById(avatarId));
            }
            else
            {
                return Path.Combine(this.rootPath, userId, string.Format("{0}x{1}", width, height), this.users.GetAvatarFileNameById(avatarId));
            }
        }
    }
}
