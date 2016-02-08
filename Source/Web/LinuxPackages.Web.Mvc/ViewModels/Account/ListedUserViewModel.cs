namespace LinuxPackages.Web.Mvc.ViewModels.Account
{
    using System;

    using AutoMapper;
    using LinuxPackages.Web.Mvc.Infrastructure.Mappings;
    using Data.Models;

    public class ListedUserViewModel : IMapFrom<User>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }
    }
}