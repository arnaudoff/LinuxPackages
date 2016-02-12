namespace LinuxPackages.Tools.Crawler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using LinuxPackages.Tools.Crawler.Contracts;
    using Models;

    internal class CsvFilePackagesExporter : IPackagesExporter
    {
        private const string PackagesFileName = "packages.csv";

        public void Export(IEnumerable<Package> packages)
        {
            var csv = new StringBuilder();

            foreach (var package in packages)
            {
                var currentLine = $"{package.Name},{package.Description},{package.CategoryName}";
                csv.AppendLine(currentLine);
            }

            File.WriteAllText(PackagesFileName, csv.ToString());
        }
    }
}