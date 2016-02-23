namespace LinuxPackages.Services.Data
{
    using System;
    using System.Linq;

    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;
    using LinuxPackages.Services.Data.Contracts;
    using System.Collections.Generic;

    public class IssuesService : IIssuesService
    {
        private readonly IRepository<Issue> issues;
        private readonly IRepository<IssueReply> replies;
        private readonly IRepository<IssueVote> votes;

        public IssuesService(IRepository<Issue> issues, IRepository<IssueReply> replies, IRepository<IssueVote> votes)
        {
            this.issues = issues;
            this.replies = replies;
            this.votes = votes;
        }

        public IQueryable<Issue> GetAll()
        {
            return this.issues.All();
        }

        public IQueryable<Issue> GetById(int id)
        {
            return this.issues
                .All()
                .Where(i => i.Id == id);
        }

        public IQueryable<Issue> GetLatest(int n)
        {
            return this.issues
                .All()
                .OrderByDescending(i => i.OpenedOn)
                .ThenBy(i => i.Id)
                .Take(n);
        }

        public Issue Create(string title, string content, IssueSeverityType severity, int packageId, string authorId)
        {
            var newIssue = new Issue()
            {
                Title = title,
                Content = content,
                Severity = severity,
                State = IssueStateType.Opened,
                OpenedOn = DateTime.UtcNow,
                AuthorId = authorId,
                PackageId = packageId
            };

            this.issues.Add(newIssue);
            this.issues.SaveChanges();

            return newIssue;
        }

        public void Update(int issueId, string title, string content, IssueSeverityType severity, IssueStateType state)
        {
            var issue = this.issues.GetById(issueId);
            issue.Title = title;
            issue.Content = content;
            issue.Severity = severity;
            issue.State = state;

            this.issues.SaveChanges();
        }

        public void DeleteById(int issueId)
        {
            this.issues.Delete(issueId);
            this.issues.SaveChanges();
        }

        public IssueReply CreateReply(string content, int issueId, string authorId)
        {
            var newReply = new IssueReply()
            {
                Content = content,
                IssueId = issueId,
                CreatedOn = DateTime.Now,
                AuthorId = authorId
            };

            this.replies.Add(newReply);
            this.replies.SaveChanges();

            return newReply;
        }

        public IQueryable<IssueReply> GetRepliesByIssueId(int issueId)
        {
            return this.replies.All().Where(r => r.IssueId == issueId);
        }

        public IQueryable<IssueReply> GetLatestReplies(int n)
        {
            return this.replies
                .All()
                .OrderByDescending(r => r.CreatedOn)
                .ThenBy(r => r.Id)
                .Take(n);
        }

        public KeyValuePair<int, int> GetVotesById(int issueId)
        {
            var votesCount = this.votes
                .All()
                .Where(v => v.IssueId == issueId)
                .GroupBy(v => v.Type)
                .Select(g => new
                {
                    PositiveCount = g.Count(v => v.Type == IssueVoteType.Positive),
                    NegativeCount = g.Count(v => v.Type == IssueVoteType.Negative)
                })
                .FirstOrDefault();

            if (votesCount == null)
            {
                return new KeyValuePair<int, int>(0, 0);
            }
            else
            {
                return new KeyValuePair<int, int>(votesCount.PositiveCount, votesCount.NegativeCount);
            }
        }

        public KeyValuePair<int, int> Vote(int issueId, int voteType, string userId)
        {
            if (voteType > 1)
            {
                voteType = 1;
            }

            if (voteType < -1)
            {
                voteType = -1;
            }

            var newVoteType = (IssueVoteType)voteType;
            var vote = this.votes
                .All()
                .FirstOrDefault(v => v.VoterId == userId && v.IssueId == issueId);

            if (vote == null)
            {
                vote = new IssueVote
                {
                    VoterId = userId,
                    IssueId = issueId,
                    Type = newVoteType
                };

                this.votes.Add(vote);
            }
            else
            {
                if (vote.Type == IssueVoteType.Neutral)
                {
                    vote.Type = newVoteType;
                }
                else
                {
                    if ((vote.Type == IssueVoteType.Positive && newVoteType == IssueVoteType.Negative) ||
                        (vote.Type == IssueVoteType.Negative && newVoteType == IssueVoteType.Positive))
                    {
                        vote.Type = IssueVoteType.Neutral;
                    }
                }
            }

            this.votes.SaveChanges();

            return this.GetVotesById(issueId);
        }

        public void Close(int issueId)
        {
            var issue = this.issues.GetById(issueId);
            issue.State = IssueStateType.Closed;
            this.issues.SaveChanges();
        }
    }
}