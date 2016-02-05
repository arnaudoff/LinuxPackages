[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LinuxPackages.Web.Mvc.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(LinuxPackages.Web.Mvc.App_Start.NinjectWebCommon), "Stop")]

namespace LinuxPackages.Web.Mvc.App_Start
{
    using System;
    using System.Web;
    using System.Web.Hosting;

    using Common;
    using Common.Constants;
    using Common.Contracts;
    using Common.Utilities;
    using Data;
    using Data.Repositories;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;

    using Services.Data;
    using Services.Data.Contracts;

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
            kernel.Bind<IRandomGenerator>().To<RandomGenerator>();

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
                .EndingWith("Service")
                .BindDefaultInterface());
        }        
    }
}
