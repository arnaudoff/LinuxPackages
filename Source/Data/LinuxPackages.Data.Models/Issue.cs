namespace LinuxPackages.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;

    public class Issue
    {
        private ICollection<IssueReply> replies;
        private ICollection<IssueVote> votes;

        public Issue()
        {
            this.replies = new HashSet<IssueReply>();
            this.votes = new HashSet<IssueVote>();
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

        public virtual ICollection<IssueVote> Votes
        {
            get
            {
                return this.votes;
            }

            set
            {
                this.votes = value;
            }
        }
    }
}
