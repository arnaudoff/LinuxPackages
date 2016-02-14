﻿namespace LinuxPackages.Services.Data
{
    using System;
    using System.Linq;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;
    using LinuxPackages.Services.Data.Contracts;

    public class UsersService : IUsersService
    {
        private readonly IRepository<User> users;

        public UsersService(IRepository<User> users)
        {
            this.users = users;
        }

        public IQueryable<User> GetAll()
        {
            return this.users.All();
        }

        public IQueryable<User> GetById(string userId)
        {
            return this.users.All().Where(u => u.Id == userId);
        }

        public IQueryable<User> GetTopMaintainers(int n)
        {
            return this.users
                .All()
                .OrderByDescending(u => u.Packages.Count)
                .Take(n);
        }
    }
}