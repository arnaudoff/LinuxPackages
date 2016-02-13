namespace LinuxPackages.Services.Data.Contracts
{
    using System.Linq;
    using LinuxPackages.Data.Models;

    public interface IIssuesService
    {
        IQueryable<Issue> GetAll();

        IQueryable<Issue> GetById(int id);

        Issue Create(string title, string content, IssueSeverityType severity, int packageId, string authorId);
    }
}