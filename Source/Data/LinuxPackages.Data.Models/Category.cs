namespace LinuxPackages.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(PackageConstants.MinCategoryLength)]
        [MaxLength(PackageConstants.MaxCategoryLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(PackageConstants.MinCategoryDescriptionLength)]
        [MaxLength(PackageConstants.MaxDescriptionLength)]
        public string Description { get; set; }
    }
}