using System.Web.Configuration;
using testWurflLocalIIS.Capabilities;
using testWurflLocalIIS.ClientProfile;
using testWurflLocalIIS.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(testWurflLocalIIS.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(testWurflLocalIIS.App_Start.NinjectWebCommon), "Stop")]

namespace testWurflLocalIIS.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                HttpCapabilitiesBase.BrowserCapabilitiesProvider = kernel.Get<MobileCapabilitiesProvider>();

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var func = new Func<string, string>((path) => HttpContext.Current.Server.MapPath(path));
            kernel.Bind<IProfileCookieEncoder>().To<ProfileCookieEncoder>();
            kernel.Bind<IProfileManifestRepository>().To<XmlProfileManifestRepository>().
                WithConstructorArgument("virtualPath", "~/ClientProfile/").
                WithConstructorArgument("virtualPathResolver", func);

        }
    }
}
