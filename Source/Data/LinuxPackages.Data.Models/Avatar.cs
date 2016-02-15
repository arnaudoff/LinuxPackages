
namespace LinuxPackages.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Avatar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FileExtension { get; set; }

        [Required]
        public int Size { get; set; }
    }
}