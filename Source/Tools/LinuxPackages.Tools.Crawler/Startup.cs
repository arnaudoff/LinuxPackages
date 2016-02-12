namespace LinuxPackages.Tools.Crawler
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            var packagesCrawler = new PackagesCrawler();
            packagesCrawler.Crawl();
        }
    }
}
