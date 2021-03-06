﻿namespace LinuxPackages.Web.Mvc.ViewModels.Packages
{
    using Infrastructure.Mappings;
    using LinuxPackages.Data.Models;

    public class LicenseViewModel : IMapFrom<License>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }
    }
}