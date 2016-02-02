using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LinuxPackages.Web.Mvc.Startup))]

namespace LinuxPackages.Web.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           this.ConfigureAuth(app);
        }
    }
}
