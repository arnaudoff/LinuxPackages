﻿namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using LinuxPackages.Web.Mvc.Infrastructure.Mappings;
    using Account;
    using Infrastructure.Helpers;
    using Common.Constants;

    public class PackageDetailsViewModel : IMapFrom<Package>, IHaveCustomMappings
    {
        private string id;

        public string Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value.ToString() + QueryStringUrlHelper.GenerateUrlHash(value.ToString(), (string)HttpContext.Current.Application[GlobalConstants.UrlSaltKeyName]);
            }
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public DistributionViewModel Distribution { get; set; }

        public RepositoryViewModel Repository { get; set; }

        public ArchitectureViewModel Architecture { get; set; }

        public LicenseViewModel License { get; set; }

        public string FileName { get; set; }

        public int Size { get; set; }

        public DateTime UploadedOn { get; set; }

        public string AverageRating { get; set; }

        public IEnumerable<ListedUserViewModel> Maintainers { get; set; }

        public IEnumerable<ListedScreenshotViewModel> Screenshots { get; set; }

        public IEnumerable<PackageCommentViewModel> Comments { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Package, PackageDetailsViewModel>()
                .ForMember(p => p.AverageRating, opts => opts.MapFrom(p => p.Ratings.Average(r => r.Value) == null ? "Not rated" : p.Ratings.Average(r => r.Value).ToString()));
        }
    }
}