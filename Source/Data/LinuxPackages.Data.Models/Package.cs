namespace LinuxPackages.Data.Models
{
    using Common.Constants;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Package
    {
        private ICollection<Package> dependencies;
        private ICollection<Package> requiredBy;
        private ICollection<User> maintainers;
        private ICollection<PackageRating> ratings;
        private ICollection<Screenshot> screenshots;
        private ICollection<PackageComment> comments;

        public Package()
        {
            this.dependencies = new HashSet<Package>();
            this.requiredBy = new HashSet<Package>();
            this.ratings = new HashSet<PackageRating>();
            this.maintainers = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(PackageConstants.MinNameLength)]
        [MaxLength(PackageConstants.MaxNameLength)]
        [Index]
        public string Name { get; set; }

        [Required]
        [MinLength(PackageConstants.MinArchitectureNameLength)]
        [MaxLength(PackageConstants.MaxArchitectureNameLength)]
        public string Architecture { get; set; }

        [Required]
        [MinLength(PackageConstants.MinArchitectureNameLength)]
        [MaxLength(PackageConstants.MaxArchitectureNameLength)]
        [Index]
        public string Description { get; set; }

        [Required]
        [MinLength(PackageConstants.MinLicenseNameLength)]
        [MaxLength(PackageConstants.MaxLicenseNameLength)]
        public string License { get; set; }

        [Required]
        public string FilePath { get; set; }

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

        public virtual ICollection<Package> RequiredBy
        {
            get
            {
                return this.requiredBy;
            }

            set
            {
                this.requiredBy = value;
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