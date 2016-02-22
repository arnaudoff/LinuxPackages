namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels.Issues
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;
    using Common.Constants;
    using Data.Models;
    using Infrastructure.Helpers;
    using Infrastructure.Mappings;

    public class AdminListedIssueViewModel : IMapFrom<Issue>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Url
        {
            get
            {
                return new UrlIdentifierProvider().EncodeEntityId(this.Id);
            }
        }

        [Required]
        [AllowHtml]
        [StringLength(IssueConstants.MaxTitleLength, MinimumLength = IssueConstants.MinTitleLength)]
        [UIHint("SingleLineText")]
        public string Title { get; set; }

        public string ShortenedTitle
        {
            get
            {
                if (this.Title != null)
                {
                    return this.Title.Length > 60 ? this.Title.Substring(0, 60) + "..." : this.Title;
                }

                return null;
            }
        }

        [Required]
        [AllowHtml]
        [StringLength(IssueConstants.MaxContentLength, MinimumLength = IssueConstants.MinContentLength)]
        [UIHint("KendoEditor")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Severity")]
        [UIHint("DropDownList")]
        public int SeverityId { get; set; }

        public IssueSeverityType Severity { get; set; }

        [Required]
        [Display(Name = "State")]
        [UIHint("DropDownList")]
        public int StateId { get; set; }

        public IssueStateType State { get; set; }

        public DateTime OpenedOn { get; set; }

        public string AuthorUsername { get; set; }

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
            configuration.CreateMap<Issue, AdminListedIssueViewModel>()
                .ForMember(i => i.PackageName, opts => opts.MapFrom(i => i.Package.Name))
                .ForMember(i => i.AuthorUsername, opts => opts.MapFrom(i => i.Author.UserName))
                .ForMember(i => i.SeverityId, opts => opts.MapFrom(i => (int)i.Severity))
                .ForMember(i => i.StateId, opts => opts.MapFrom(i => (int)i.State));
        }
    }
}