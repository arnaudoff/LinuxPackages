namespace LinuxPackages.Services.Data
{
    using System;
    using System.Linq;
    using Contracts;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;

    public class RepositoriesService : IRepositoriesService
    {
        private readonly IRepository<Repository> repositories;

        public RepositoriesService(IRepository<Repository> repositories)
        {
            this.repositories = repositories;
        }

        public IQueryable<Repository> GetAll()
        {
            return this.repositories.All();
        }

        public IQueryable<Repository> GetById(int id)
        {
            return this.repositories.All().Where(r => r.Id == id);
        }

        public Repository Create(string name, string url)
        {
            var newRepository = new Repository
            {
                Name = name,
                Url = url
            };

            this.repositories.Add(newRepository);
            this.repositories.SaveChanges();

            return newRepository;
        }
    }
}