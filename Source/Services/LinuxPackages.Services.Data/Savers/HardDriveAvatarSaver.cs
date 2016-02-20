namespace LinuxPackages.Services.Data.Savers
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;

    using Common.Constants;
    using Common.Utilities;
    using LinuxPackages.Services.Data.Contracts.Savers;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;

    public class HardDriveAvatarSaver : IAvatarSaver
    {
        private readonly string rootPath;
        private readonly IRepository<Avatar> avatars;

        public HardDriveAvatarSaver(string rootPath, IRepository<Avatar> avatars)
        {
            this.rootPath = rootPath;
            this.avatars = avatars;
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
            string avatarFilename = this.avatars
                .All()
                .Where(a => a.Id == avatarId)
                .Select(a => string.Concat(a.FileName, a.FileExtension))
                .FirstOrDefault();

            if (width == null || height == null)
            {
                return Path.Combine(this.rootPath, userId, avatarFilename);
            }
            else
            {
                return Path.Combine(this.rootPath, userId, string.Format("{0}x{1}", width, height), avatarFilename);
            }
        }
    }
}
