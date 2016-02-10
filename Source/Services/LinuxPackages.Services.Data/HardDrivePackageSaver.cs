namespace LinuxPackages.Services.Data
{
    using System;
    using System.IO;

    using Common.Constants;
    using LinuxPackages.Services.Data.Contracts;

    public class HardDrivePackageSaver : IPackageSaver
    {
        private readonly string rootPath;

        public HardDrivePackageSaver(string rootPath)
        {
            this.rootPath = rootPath;
        }

        public void Save(int packageId, string packageName, string filename, byte[] contents)
        {
            var directoryToSave = packageId % PackageConstants.PackagesPerDirectory;

            var directoryToSavePath = Path.Combine(this.rootPath, directoryToSave.ToString());
            if (!Directory.Exists(directoryToSavePath))
            {
                Directory.CreateDirectory(directoryToSavePath);
            }

            var packageDirectoryPath = Path.Combine(directoryToSavePath, packageName);
            if (!Directory.Exists(packageDirectoryPath))
            {
                Directory.CreateDirectory(packageDirectoryPath);
            }

            string finalPath = Path.Combine(packageDirectoryPath, filename);
            File.WriteAllBytes(finalPath, contents);
        }

        public byte[] Read(int packageId, string packageName, string fileName)
        {
            var filePath = Path.Combine(
                this.rootPath,
                (packageId % PackageConstants.PackagesPerDirectory).ToString(),
                packageName,
                fileName);

            return File.ReadAllBytes(filePath);
        }
    }
}