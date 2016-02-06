namespace LinuxPackages.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Dependency
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PackageId { get; set; }

        [Required]
        public int DependsOnId { get; set; }
    }
}
