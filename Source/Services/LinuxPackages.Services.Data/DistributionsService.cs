namespace LinuxPackages.Services.Data
{
    using System;
    using System.Linq;
    using Contracts;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;

    public class DistributionsService : IDistributionsService
    {
        private readonly IRepository<Distribution> distros;

        public DistributionsService(IRepository<Distribution> distros)
        {
            this.distros = distros;
        }

        public IQueryable<Distribution> GetAll()
        {
            return this.distros.All();
        }

        public IQueryable<Distribution> GetById(int id)
        {
            return this.distros.All().Where(d => d.Id == id);
        }

        public Distribution Create(string name, string maintainer, string url)
        {
            var newDistro = new Distribution
            {
                Name = name,
                Maintainer = maintainer,
                Url = url
            };

            this.distros.Add(newDistro);
            this.distros.SaveChanges();

            return newDistro;
        }
    }
}