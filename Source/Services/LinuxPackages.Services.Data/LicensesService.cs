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

        public void Update(int licenseId, string name, string description, string url)
        {
            var license = this.licenses.GetById(licenseId);
            license.Name = name;
            license.Description = description;
            license.Url = url;

            this.licenses.SaveChanges();
        }

        public void DeleteById(int licenseId)
        {
            this.licenses.Delete(licenseId);
            this.licenses.SaveChanges();
        }
    }
}