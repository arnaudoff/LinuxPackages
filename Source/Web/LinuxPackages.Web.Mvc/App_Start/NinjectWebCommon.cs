[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LinuxPackages.Web.Mvc.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(LinuxPackages.Web.Mvc.App_Start.NinjectWebCommon), "Stop")]

namespace LinuxPackages.Web.Mvc.App_Start
{
    using System;
    using System.Data.Entity;
    using System.Web;
    using System.Web.Hosting;

    using Common;
    using Common.Constants;
    using Common.Contracts;
    using Common.Utilities;
    using Data;
    using Data.Models;
    using Data.Repositories;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;
    using Services.Data;
    using Services.Data.Contracts;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        public static void Stop()
        {
            Bootstrapper.ShutDown();
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
            kernel.Bind<DbContext>().To<LinuxPackagesDbContext>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));
            kernel.Bind<IRandomGenerator>().To<RandomGenerator>();

            string packagesStorePath = HostingEnvironment.MapPath(PackageConstants.PackagesPath);

            kernel.Bind<IPackageSaver>()
                .To<HardDrivePackageSaver>()
                .WithConstructorArgument("rootPath", packagesStorePath);

            kernel.Bind<IScreenshotSaver>()
                .To<HardDriveScreenshotSaver>()
                .WithConstructorArgument("rootPath", packagesStorePath)
                .WithConstructorArgument("screenshotsFolderName", PackageConstants.ScreenshotsFolderName);

            kernel.Bind(b => b
                .From(Assemblies.Services)
                .SelectAllClasses()
                .EndingWith("Service")
                .BindDefaultInterface());
        }        
    }
}
