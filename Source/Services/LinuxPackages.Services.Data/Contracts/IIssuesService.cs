﻿namespace LinuxPackages.Services.Data.Contracts
{
    using System.Linq;
    using LinuxPackages.Data.Models;

    public interface IIssuesService
    {
        IQueryable<Issue> GetAll();

        IQueryable<Issue> GetById(int id);

        IQueryable<Issue> GetRecent(int n);

        Issue Create(string title, string content, IssueSeverityType severity, int packageId, string authorId);

        IQueryable<IssueReply> GetRepliesByIssueId(int issueId);

        IssueReply CreateReply(string content, int issueId, string authorId);
    }
}