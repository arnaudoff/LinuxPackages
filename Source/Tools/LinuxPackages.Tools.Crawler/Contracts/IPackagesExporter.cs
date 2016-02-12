namespace LinuxPackages.Tools.Crawler.Contracts
{
    using Models;
    using System.Collections.Generic;

    public interface IPackagesExporter
    {
        void Export(IEnumerable<Package> packages);
    }
}