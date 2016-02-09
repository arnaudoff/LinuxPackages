namespace LinuxPackages.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;

    public class Repository
    {
        private ICollection<Package> packages;

        public Repository()
        {
            this.packages = new HashSet<Package>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(PackageConstants.MinRepositoryNameLength)]
        [MaxLength(PackageConstants.MaxRepositoryNameLength)]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

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