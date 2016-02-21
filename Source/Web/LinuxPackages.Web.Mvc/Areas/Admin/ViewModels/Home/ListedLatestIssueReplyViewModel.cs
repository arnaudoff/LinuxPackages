namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels
{
    using System;
    using AutoMapper;
    using Infrastructure.Helpers;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Web.Mvc.Infrastructure.Mappings;

    public class ListedLatestIssueReplyViewModel : IMapFrom<IssueReply>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string ShortenedContent
        {
            get
            {
                return this.Content.Length > 60 ? this.Content.Substring(0, 60) + "..." : this.Content;
            }
        }

        public DateTime CreatedOn { get; set; }

        public int IssueId { get; set; }

        public string IssueUrl
        {
            get
            {
                return new UrlIdentifierProvider().EncodeEntityId(this.IssueId);
            }
        }

        public string AuthorUsername { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<IssueReply, ListedLatestIssueReplyViewModel>()
                .ForMember(r => r.AuthorUsername, opts => opts.MapFrom(r => r.Author.UserName));
        }
    }
}