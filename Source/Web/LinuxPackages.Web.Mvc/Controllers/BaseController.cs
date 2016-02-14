namespace LinuxPackages.Web.Mvc.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using App_Start;
    using AutoMapper;
    using Services.Data;
    using Ninject;

    public abstract class BaseController : Controller
    {
        [Inject]
        public IUsersService Users { private get; set; }

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