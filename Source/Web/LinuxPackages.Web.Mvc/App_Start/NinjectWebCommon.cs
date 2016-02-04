[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LinuxPackages.Web.Mvc.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(LinuxPackages.Web.Mvc.App_Start.NinjectWebCommon), "Stop")]

namespace LinuxPackages.Web.Mvc.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Extensions.Conventions;

    using Data;
    using Data.Repositories;
    using Common;
    using Services.Data.Contracts;
    using Services.Data;
    using System.Web.Hosting;
    using Common.Constants;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ILinuxPackagesDbContext>().To<LinuxPackagesDbContext>();
            kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));

            string packagesStorePath = HostingEnvironment.MapPath(PackageConstants.PackagesPath);

            kernel.Bind<IPackageSaver>()
                .To<HardDrivePackageSaver>()
                .WithConstructorArgument(packagesStorePath);

            kernel.Bind<IScreenshotSaver>()
                .To<HardDriveScreenshotSaver>()
                .WithConstructorArgument(packagesStorePath)
                .WithConstructorArgument(PackageConstants.ScreenshotsFolderName);

            kernel.Bind(b => b
                .From(Assemblies.Services)
                .SelectAllClasses()
                .BindDefaultInterface());
        }        
    }
}
