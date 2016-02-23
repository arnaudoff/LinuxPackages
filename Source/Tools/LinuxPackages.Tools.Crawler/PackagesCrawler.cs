namespace LinuxPackages.Tools.Crawler
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using AngleSharp;
    using AngleSharp.Dom.Html;
    using Contracts;
    using Models;

    public class PackagesCrawler : ICrawler
    {
        private const string Url = "https://packages.debian.org/stable/";
        private const int NewCategoryRequestDelay = 15000;

        private readonly IConfiguration configuration;
        private readonly IBrowsingContext browsingContext;
        private readonly IPackagesExporter exporter;

        public PackagesCrawler()
            : this(new CsvFilePackagesExporter())
        {
        }

        public PackagesCrawler(IPackagesExporter exporter)
        {
            this.configuration = Configuration.Default.WithDefaultLoader();
            this.browsingContext = BrowsingContext.New(this.configuration);
            this.exporter = exporter;
        }

        public void Crawl()
        {
            IEnumerable<Category> categories = this.ParseCategories();
            IEnumerable<Package> packages = this.ParsePackages(categories);
            this.exporter.Export(packages);
        }

        private IList<Package> ParsePackages(IEnumerable<Category> categories)
        {
            var packages = new List<Package>();

            foreach (var category in categories)
            {
                var currentPackageDocument = this.browsingContext.OpenAsync(category.Url).Result;
                var packageElements = currentPackageDocument.QuerySelectorAll("#content dl dt a");
                var packageDescription = currentPackageDocument.QuerySelectorAll("#content dl dd");

                for (int i = 0; i < packageElements.Length; i++)
                {
                    var currentPackage = new Package()
                    {
                        Name = packageElements[i].TextContent,
                        Description = packageDescription[i].TextContent.Replace("\"", "'"),
                        CategoryName = category.Name
                    };

                    Console.WriteLine($"Crawled package {currentPackage.Name}. ({currentPackage.CategoryName})");
                    packages.Add(currentPackage);
                }

                Console.WriteLine($"Crawled category {category.Name}. ({NewCategoryRequestDelay} ms timeout..)");
                Thread.Sleep(NewCategoryRequestDelay);
            }

            return packages;
        }

        private IList<Category> ParseCategories()
        {
            var categoriesDocument = this.browsingContext.OpenAsync(Url).Result;

            var leftPackagesCategories = categoriesDocument.QuerySelectorAll("#lefthalfcol dl dt");
            var leftPackagesCategoriesDescriptions = categoriesDocument.QuerySelectorAll("#lefthalfcol dl dd");

            var rightPackagesCategories = categoriesDocument.QuerySelectorAll("#righthalfcol dl dt");
            var rightPackagesCategoriesDescriptions = categoriesDocument.QuerySelectorAll("#righthalfcol dl dd");

            var categories = new List<Category>();

            for (int i = 0; i < leftPackagesCategories.Length; i++)
            {
                var categoryAnchor = (IHtmlAnchorElement)leftPackagesCategories[i].FirstChild;
                var currentCategory = new Category()
                {
                    Name = categoryAnchor.Text,
                    Description = leftPackagesCategoriesDescriptions[i].TextContent,
                    Url = categoryAnchor.Href
                };

                categories.Add(currentCategory);
            }

            for (int i = 0; i < rightPackagesCategories.Length; i++)
            {
                var categoryAnchor = (IHtmlAnchorElement)rightPackagesCategories[i].FirstChild;
                var currentCategory = new Category()
                {
                    Name = categoryAnchor.Text,
                    Description = rightPackagesCategoriesDescriptions[i].TextContent,
                    Url = categoryAnchor.Href
                };

                categories.Add(currentCategory);
            }

            return categories;
        }
    }
}