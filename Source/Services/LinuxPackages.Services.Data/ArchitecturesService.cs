namespace LinuxPackages.Services.Data
{
    using System;
    using System.Linq;
    using Contracts;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;

    public class ArchitecturesService : IArchitecturesService
    {
        private readonly IRepository<Architecture> architectures;

        public ArchitecturesService(IRepository<Architecture> architectures)
        {
            this.architectures = architectures;
        }

        public IQueryable<Architecture> GetAll()
        {
            return this.architectures.All();
        }

        public IQueryable<Architecture> GetById(int id)
        {
            return this.architectures.All().Where(a => a.Id == id);
        }

        public Architecture Create(string name)
        {
            var newArchitecture = new Architecture
            {
                Name = name
            };

            this.architectures.Add(newArchitecture);
            this.architectures.SaveChanges();

            return newArchitecture;
        }

        public void Update(int archId, string name)
        {
            var arch = this.architectures.GetById(archId);
            arch.Name = name;

            this.architectures.SaveChanges();
        }

        public void DeleteById(int archId)
        {
            this.architectures.Delete(archId);
            this.architectures.SaveChanges();
        }
    }
}