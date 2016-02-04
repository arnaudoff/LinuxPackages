namespace LinuxPackages.Services.Data
{
    using System;
    using LinuxPackages.Services.Data.Contracts;
    using System.IO;
    using Common.Constants;

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

            string finalPath = Path.Combine(this.rootPath, directoryToSave.ToString(), Path.GetFileNameWithoutExtension(filename), filename);

            File.WriteAllBytes(finalPath, contents);
        }
    }
}