namespace LinuxPackages.Web.Mvc.Areas.Admin.ViewModels.Home
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Helpers;
    using LinuxPackages.Web.Mvc.Infrastructure.Mappings;

    public class ListedLatestCommentViewModel : IMapFrom<PackageComment>, IHaveCustomMappings
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

        public int PackageId { get; set; }

        public string PackageUrl
        {
            get
            {
                return new UrlIdentifierProvider().EncodeEntityId(this.PackageId);
            }
        }

        public string PackageName { get; set; }

        public string AuthorUsername { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<PackageComment, ListedLatestCommentViewModel>()
                .ForMember(c => c.PackageName, opts => opts.MapFrom(c => c.Package.Name))
                .ForMember(c => c.AuthorUsername, opts => opts.MapFrom(c => c.Author.UserName));
        }
    }
}