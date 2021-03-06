﻿namespace LinuxPackages.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;

    public class PackageComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(PackageConstants.MinCommentContentLength)]
        [MaxLength(PackageConstants.MaxCommentContentLength)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public int PackageId { get; set; }

        public virtual Package Package { get; set;  }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }
    }
}