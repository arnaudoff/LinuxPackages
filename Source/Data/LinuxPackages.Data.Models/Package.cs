namespace LinuxPackages.Data.Models
{
    using Common.Constants;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Package
    {
        private ICollection<User> maintainers;
        private ICollection<PackageRating> ratings;
        private ICollection<Screenshot> screenshots;
        private ICollection<PackageComment> comments;

        public Package()
        {
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