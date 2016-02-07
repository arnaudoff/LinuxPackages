namespace LinuxPackages.Services.Data
{
    using System;
    using System.Linq;
    using Contracts;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;

    public class LicensesService : ILicensesService
    {
        private readonly IRepository<License> licenses;

        public LicensesService(IRepository<License> licenses)
        {
            this.licenses = licenses;
        }

        public IQueryable<License> GetAll()
        {
            return this.licenses.All();
        }

        public IQueryable<License> GetById(int id)
        {
            return this.licenses.All().Where(l => l.Id == id);
        }

        public License Create(string name, string description, string url)
        {
            var newLicense = new License
            {
                Name = name,
                Description = description,
                Url = url
            };

            this.licenses.Add(newLicense);
            this.licenses.SaveChanges();

            return newLicense;
        }
    }
}