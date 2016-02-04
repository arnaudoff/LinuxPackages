﻿namespace LinuxPackages.Data.Models
{
    using Common.Constants;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Package
    {
        private ICollection<Package> dependencies;
        private ICollection<User> maintainers;
        private ICollection<PackageRating> ratings;
        private ICollection<Screenshot> screenshots;
        private ICollection<PackageComment> comments;

        public Package()
        {
            this.dependencies = new HashSet<Package>();
            this.maintainers = new HashSet<User>();
            this.ratings = new HashSet<PackageRating>();
            this.screenshots = new HashSet<Screenshot>();
            this.comments = new HashSet<PackageComment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(PackageConstants.MinNameLength)]
        [MaxLength(PackageConstants.MaxNameLength)]
        [Index]
        public string Name { get; set; }

        public int ArchitectureId { get; set; }

        public virtual Architecture Architecture { get; set; }

        [Index]
        public string Description { get; set; }

        public int LicenseId { get; set; }

        public virtual License License { get; set; }

        public int RepositoryId { get; set; }

        public virtual Repository Repository { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public uint Size { get; set; }

        [Required]
        public DateTime UploadedOn { get; set; }

        public virtual ICollection<Package> Dependencies
        {
            get
            {
                return this.dependencies;
            }

            set
            {
                this.dependencies = value;
            }
        }

        public virtual ICollection<PackageRating> Ratings
        {
            get
            {
                return this.ratings;
            }

            set
            {
                this.ratings = value;
            }
        }

        [Required]
        public virtual ICollection<User> Maintainers
        {
            get
            {
                return this.maintainers;
            }

            set
            {
                this.maintainers = value;
            }
        }

        public virtual ICollection<Screenshot> Screenshot
        {
            get
            {
                return this.screenshots;
            }

            set
            {
                this.screenshots = value;
            }
        }

        public virtual ICollection<PackageComment> Comments
        {
            get
            {
                return this.comments;
            }

            set
            {
                this.comments = value;
            }
        }
    }
}