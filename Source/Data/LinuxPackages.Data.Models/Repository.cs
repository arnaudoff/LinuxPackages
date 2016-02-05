﻿namespace LinuxPackages.Data.Models
{
    using Common.Constants;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Repository
    {
        private ICollection<Package> packages;

        public Repository()
        {
            this.packages = new HashSet<Package>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(PackageConstants.MinRepositoryNameLength)]
        [MaxLength(PackageConstants.MaxRepositoryNameLength)]
        public string Name { get; set; }

        public virtual ICollection<Package> Packages
        {
            get
            {
                return this.packages;
            }

            set
            {
                this.packages = value;
            }
        }
    }
}