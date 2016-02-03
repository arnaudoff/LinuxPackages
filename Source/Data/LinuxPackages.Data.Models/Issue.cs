namespace LinuxPackages.Data.Models
{
    using Common.Constants;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Issue
    {
        public ICollection<IssueReply> replies;

        public Issue()
        {
            this.replies = new HashSet<IssueReply>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(IssueConstants.MinTitleLength)]
        [MaxLength(IssueConstants.MaxTitleLength)]
        public string Title { get; set; }

        [Required]
        public IssueSeverityLevel SeverityLevel { get; set; }

        [Required]
        [MinLength(IssueConstants.MinContentLength)]
        [MaxLength(IssueConstants.MaxContentLength)]
        public string Content { get; set; }

        [Required]
        public DateTime OpenedOn { get; set; }

        public int PositiveVotes { get; set; }

        public int NegativeVotes { get; set; }

        public int PackageId { get; set; }

        public virtual Package Package { get; set; }

        public virtual ICollection<IssueReply> Replies
        {
            get
            {
                return this.replies;
            }

            set
            {
                this.replies = value;
            }
        }
    }
}
