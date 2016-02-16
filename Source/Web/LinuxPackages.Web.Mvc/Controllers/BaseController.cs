namespace LinuxPackages.Web.Mvc.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using App_Start;
    using AutoMapper;

    using Data.Models;
    using Infrastructure.Helpers.Contracts;

    using Microsoft.AspNet.Identity;
    using Ninject;

    using Services.Data;
    using Services.Data.Contracts;

    public abstract class BaseController : Controller
    {
        [Inject]
        public IUsersService Users { protected get; set; }

        [Inject]
        public IUrlIdentifierProvider UrlIdentifierProvider { get; set; }

        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }

        protected User UserProfile { get; set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.UserProfile = this.Users.GetById(requestContext.HttpContext.User.Identity.GetUserId()).FirstOrDefault();
            var result = base.BeginExecute(requestContext, callback, state);
            return result;
        }
    }
}