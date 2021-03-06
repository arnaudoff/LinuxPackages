﻿namespace LinuxPackages.Services.Data.Contracts
{
    using System.Linq;
    using LinuxPackages.Data.Models;

    public interface IArchitecturesService
    {
        IQueryable<Architecture> GetAll();

        IQueryable<Architecture> GetById(int id);

        Architecture Create(string name);

        void Update(int archId, string name);

        void DeleteById(int archId);
    }
}