using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LinuxPackages.Web.Mvc.Startup))]

namespace LinuxPackages.Web.Mvc
{
    using Common;
    using LinuxPackages.Web.Mvc.App_Start;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfig.RegisterMappings(Assemblies.Mvc);
            this.ConfigureAuth(app);
        }
    }
}
