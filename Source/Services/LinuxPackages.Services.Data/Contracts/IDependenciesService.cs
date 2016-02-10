namespace LinuxPackages.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using LinuxPackages.Data.Models;

    public interface IDependenciesService
    {
        IQueryable<Package> GetAllById(int packageId);
    }
}
