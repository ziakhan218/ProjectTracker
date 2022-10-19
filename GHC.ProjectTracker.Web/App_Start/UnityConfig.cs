using GHC.Common.Logger;
using GHC.ProjectTracker.Common.Authorization;
using GHC.ProjectTracker.IService;
using GHC.ProjectTracker.Web.Common;
using GHC.ProjectTracker.Web.Controllers;
using GHC.ProjectTracker.Web.Infrastructure.Config;
using GHC.ProjectTracker.Web.Infrastructure.Mappers;
using Microsoft.Practices.Unity;
using System;
using System.Configuration;
using System.ServiceModel;

namespace GHC.ProjectTracker.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            container.RegisterType<IITPTrackerService>(
                new ContainerControlledLifetimeManager(),
                new InjectionFactory((c) => new ChannelFactory<IITPTrackerService>("BasicHttpBinding_IITPTrackerService").CreateChannel()));

            container.RegisterType<IProjectTrackerService>(
                new ContainerControlledLifetimeManager(),
                new InjectionFactory((c) => new ChannelFactory<IProjectTrackerService>("BasicHttpBinding_IProjectTrackerService").CreateChannel()));


            container.RegisterType<ISessionManager>(new ContainerControlledLifetimeManager(), new InjectionFactory(c => new SessionManager()))
                .RegisterType<IITPTrackerMapper>(new ContainerControlledLifetimeManager(), new InjectionFactory(c => new ITPTrackerMapper()))
                
                .RegisterType<IApplicationRoleProvider>(new ContainerControlledLifetimeManager(), new InjectionFactory(c => new ApplicationRoleProvider()))
                .RegisterType<IConfigSettingsHelper>(new ContainerControlledLifetimeManager(), new InjectionFactory(c => new ConfigSettingsHelper()));
                
                
            
        }

        private static void RegisterLogger(IUnityContainer container)
        {
            Logger.EnableUnityInterceptor(container, ConfigurationManager.AppSettings["ExceptionPolicyToUse"]);
            Logger.SetInteceptorFor<HomeController>();
        }
    }
}
