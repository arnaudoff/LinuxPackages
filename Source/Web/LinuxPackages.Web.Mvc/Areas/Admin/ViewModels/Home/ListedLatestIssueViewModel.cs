namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels
{
    using System;
    using AutoMapper;
    using Infrastructure.Helpers;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Web.Mvc.Infrastructure.Mappings;
    using Mvc.ViewModels.Packages;

    public class ListedLatestIssueViewModel : IMapFrom<Issue>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Url
        {
            get
            {
                return new UrlIdentifierProvider().EncodeEntityId(this.Id);
            }
        }

        public string Title { get; set; }

        public string ShortenedTitle
        {
            get
            {
                return this.Title.Length > 60 ? this.Title.Substring(0, 60) + "..." : this.Title;
            }
        }

        public IssueSeverityType Severity { get; set; }

        public DateTime OpenedOn { get; set; }

        public int PackageId { get; set; }

        public string PackageUrl
        {
            get
            {
                return new UrlIdentifierProvider().EncodeEntityId(this.PackageId);
            }
        }

        public string PackageName { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Issue, ListedLatestIssueViewModel>()
                .ForMember(i => i.PackageName, opts => opts.MapFrom(i => i.Package.Name));
        }
    }
}