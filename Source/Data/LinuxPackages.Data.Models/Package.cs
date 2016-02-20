namespace LinuxPackages.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Constants;

    public class Package
    {
        private ICollection<User> maintainers;
        private ICollection<Rating> ratings;
        private ICollection<Screenshot> screenshots;
        private ICollection<PackageComment> comments;
        private ICollection<Issue> issues;

        public Package()
        {
            this.maintainers = new HashSet<User>();
            this.ratings = new HashSet<Rating>();
            this.screenshots = new HashSet<Screenshot>();
            this.comments = new HashSet<PackageComment>();
            this.issues = new HashSet<Issue>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(PackageConstants.MinNameLength)]
        [MaxLength(PackageConstants.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(PackageConstants.MinDescriptionLength)]
        [MaxLength(PackageConstants.MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public int DistributionId { get; set; }

        public virtual Distribution Distribution { get; set; }

        [Required]
        public int RepositoryId { get; set; }

        public virtual Repository Repository { get; set; }

        [Required]
        public int ArchitectureId { get; set; }

        public virtual Architecture Architecture { get; set; }

        [Required]
        public int LicenseId { get; set; }

        public virtual License License { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public int Size { get; set; }

        [Required]
        public DateTime UploadedOn { get; set; }

        public int Downloads { get; set; }

        public virtual ICollection<Rating> Ratings
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

        public virtual ICollection<Screenshot> Screenshots
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

        public virtual ICollection<Issue> Issues
        {
            get
            {
                return this.issues;
            }

            set
            {
                this.issues = value;
            }
        }
    }
}