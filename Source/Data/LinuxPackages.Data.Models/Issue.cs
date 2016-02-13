namespace LinuxPackages.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;

    public class Issue
    {
        private ICollection<IssueReply> replies;

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
        [MinLength(IssueConstants.MinContentLength)]
        [MaxLength(IssueConstants.MaxContentLength)]
        public string Content { get; set; }

        [Required]
        public IssueSeverityType Severity { get; set; }

        public IssueStateType State { get; set; }

        [Required]
        public DateTime OpenedOn { get; set; }

        public int PositiveVotes { get; set; }

        public int NegativeVotes { get; set; }

        public int PackageId { get; set; }

        public virtual Package Package { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

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
