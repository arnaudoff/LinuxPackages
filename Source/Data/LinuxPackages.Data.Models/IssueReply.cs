namespace LinuxPackages.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;

    public class IssueReply
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(IssueConstants.MinIssueReplyContentLength)]
        [MaxLength(IssueConstants.MaxIssueReplyContentLength)]
        public string Content { get; set; }

        public int IssueId { get; set; }

        public virtual Issue Issue { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}