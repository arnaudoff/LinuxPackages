namespace LinuxPackages.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using LinuxPackages.Data.Models;

    public interface IPackagesService
    {
        IQueryable<Package> GetAll();

        IQueryable<Package> GetById(int id);

        Package Create(
            string name,
            string description,
            int distributionId,
            int repositoryId,
            int architectureId,
            int licenseId,
            string fileName,
            byte[] contents,
            IList<int> dependencyIds,
            IList<string> maintainerIds);

        PackageComment AddComment(string content, int packageId, string authorId);

        PackageRating AddRating(int value, int packageId);
    }
}