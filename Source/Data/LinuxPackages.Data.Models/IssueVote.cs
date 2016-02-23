namespace LinuxPackages.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class IssueVote
    {
        public int Id { get; set; }

        [Required]
        public string VoterId { get; set; }

        public virtual User Voter { get; set; }

        public int IssueId { get; set; }

        public virtual Issue Issue { get; set; }

        public IssueVoteType Type { get; set; }
    }
}
