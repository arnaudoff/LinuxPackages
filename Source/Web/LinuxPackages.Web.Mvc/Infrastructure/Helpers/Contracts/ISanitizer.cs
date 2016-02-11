namespace LinuxPackages.Web.Mvc.Infrastructure.Helpers.Contracts
{
    public interface ISanitizer
    {
        string Sanitize(string html);
    }
}
