namespace LinuxPackages.Services.Data
{
    using System;
    using System.Linq;

    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;
    using LinuxPackages.Services.Data.Contracts;

    public class IssuesService : IIssuesService
    {
        private readonly IRepository<Issue> issues;
        private readonly IRepository<IssueReply> replies;

        public IssuesService(IRepository<Issue> issues, IRepository<IssueReply> replies)
        {
            this.issues = issues;
            this.replies = replies;
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
                PositiveVotes = 0,
                NegativeVotes = 0,
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
    }
}