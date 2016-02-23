namespace LinuxPackages.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using LinuxPackages.Data.Models;

    public interface IIssuesService
    {
        IQueryable<Issue> GetAll();

        IQueryable<Issue> GetById(int id);

        IQueryable<Issue> GetLatest(int n);

        Issue Create(string title, string content, IssueSeverityType severity, int packageId, string authorId);

        void Update(int issueId, string title, string content, IssueSeverityType severity, IssueStateType state);

        void DeleteById(int id);

        IQueryable<IssueReply> GetRepliesByIssueId(int issueId);

        IQueryable<IssueReply> GetLatestReplies(int n);

        IssueReply CreateReply(string content, int issueId, string authorId);

        KeyValuePair<int, int> GetVotesById(int issueId);

        KeyValuePair<int, int> Vote(int issueId, int voteType, string userId);

        void Close(int issueId);
    }
}