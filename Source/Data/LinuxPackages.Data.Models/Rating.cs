namespace LinuxPackages.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;

    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(PackageConstants.MinRating, PackageConstants.MaxRating)]
        public int Value { get; set; }

        public int PackageId { get; set; }

        public virtual Package Package { get; set; }

        public string RatedById { get; set; }

        public virtual User RatedBy { get; set; }
    }
}