namespace LinuxPackages.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class Architecture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ArchitectureConstants.MinArchitectureNameLength)]
        [MaxLength(ArchitectureConstants.MaxArchitectureNameLength)]
        public string Name { get; set; }
    }
}