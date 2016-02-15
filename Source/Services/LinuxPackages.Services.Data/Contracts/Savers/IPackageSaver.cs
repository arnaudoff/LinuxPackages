﻿namespace LinuxPackages.Services.Data.Contracts.Savers
{
    public interface IPackageSaver
    {
        void Save(int packageId, string packageName, string fileName, byte[] contents);

        byte[] Read(int packageId, string name, string fileName);
    }
}