[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Notes.WebApi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Notes.WebApi.App_Start.NinjectWebCommon), "Stop")]

namespace Notes.WebApi.App_Start
{
    using System;
    using System.Data.Entity;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataProtection;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Notes.Database.Contexts;
    using Notes.Database.Models;
    using Notes.Database.Repositories;
    using Notes.WebApi.Utils.Security;

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
            kernel.Bind<NotesContext>().ToSelf().InRequestScope();
            kernel.Load<AuthorizationModule>();
            kernel.Load<RepositoryModule>();
        }

        private class AuthorizationModule : NinjectModule
        {
            public override void Load()
            {
                this.Kernel.Bind<DbContext>().To<NotesContext>().InRequestScope();
                this.Kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>().InRequestScope();
                this.Kernel.Bind<IDataProtectionProvider>().To<MachineKeyDataProtectionProvider>().InRequestScope();
                this.Kernel.Bind<IDataProtector>().ToMethod((k) => k.Kernel.Get<IDataProtectionProvider>().Create("ASP.NET Identity")).InRequestScope();
                this.Kernel.Bind<UserManager<ApplicationUser>>().To<ApplicationUserManager>().InRequestScope();
                this.Kernel.Bind<IAuthenticationManager>().ToMethod((k) => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            }
        }

        private class RepositoryModule : NinjectModule
        {
            public override void Load()
            {
                this.Kernel.Bind<IRepository<Note>>().To<NoteRepository>();
            }
        }

    }
}