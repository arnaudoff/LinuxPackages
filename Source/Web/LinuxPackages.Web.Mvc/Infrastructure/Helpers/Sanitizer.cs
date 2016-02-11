namespace LinuxPackages.Web.Mvc.Infrastructure.Helpers
{
    using Contracts;
    using Ganss.XSS;

    public class Sanitizer : ISanitizer
    {
        public string Sanitize(string html)
        {
            var sanitizer = new HtmlSanitizer();
            return sanitizer.Sanitize(html);
        }
    }
}