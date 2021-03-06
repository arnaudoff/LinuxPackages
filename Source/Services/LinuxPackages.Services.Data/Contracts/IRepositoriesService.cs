﻿namespace LinuxPackages.Services.Data.Contracts
{
    using System.Linq;
    using LinuxPackages.Data.Models;

    public interface IRepositoriesService
    {
        IQueryable<Repository> GetAll();

        IQueryable<Repository> GetById(int id);

        Repository Create(string name, string url);

        void Update(int repoId, string name, string url);

        void DeleteById(int repoId);
    }
}