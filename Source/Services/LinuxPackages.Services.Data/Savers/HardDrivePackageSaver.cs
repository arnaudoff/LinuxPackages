namespace LinuxPackages.Services.Data.Savers
{
    using System;
    using System.IO;

    using Common.Constants;
    using Contracts.Savers;

    public class HardDrivePackageSaver : IPackageSaver
    {
        private readonly string rootPath;

        public HardDrivePackageSaver(string rootPath)
        {
            this.rootPath = rootPath;
        }

        public void Save(int packageId, string filename, byte[] contents)
        {
            var directoryToSave = packageId % PackageConstants.PackagesPerDirectory;

            var directoryToSavePath = Path.Combine(this.rootPath, directoryToSave.ToString());
            if (!Directory.Exists(directoryToSavePath))
            {
                Directory.CreateDirectory(directoryToSavePath);
            }

            var packageDirectoryPath = Path.Combine(directoryToSavePath, packageId.ToString());
            if (!Directory.Exists(packageDirectoryPath))
            {
                Directory.CreateDirectory(packageDirectoryPath);
            }

            string finalPath = Path.Combine(packageDirectoryPath, filename);
            File.WriteAllBytes(finalPath, contents);
        }

        public byte[] Read(int packageId, string fileName)
        {
            var filePath = Path.Combine(
                this.rootPath,
                (packageId % PackageConstants.PackagesPerDirectory).ToString(),
                packageId.ToString(),
                fileName);

            return File.ReadAllBytes(filePath);
        }
    }
}