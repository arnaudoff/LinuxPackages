namespace LinuxPackages.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Screenshot
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FileExtension { get; set; }

        [Required]
        public uint Size { get; set; }

        public int PackageId { get; set; }

        public virtual Package Package { get; set; }
    }
}