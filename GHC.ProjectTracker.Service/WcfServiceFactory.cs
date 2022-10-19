using GHC.ProjectTracker.Business;
using GHC.ProjectTracker.Data;
using GHC.ProjectTracker.Data.Infrastructure;
using GHC.ProjectTracker.IService;
using GHC.Common.Logger;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Configuration;
using Unity.Wcf;
using GHC.DataPortal.IService;
using GHC.DataPortal.Service;
using GHC.DataPortal.Data;

namespace GHC.ProjectTracker.Service
{
	public class WcfServiceFactory : UnityServiceHostFactory
    {
        static bool interceptorsAlreadySet = false;

        protected override void ConfigureContainer(IUnityContainer container)
        {
            // register all your components with the container here
            container.RegisterType<IITPTrackerService, ITPTrackerService>()
                     .RegisterType<IITPTrackerManager, ITPTrackerManager>()
                     .RegisterType<IUnitOfWork, UnitOfWork>()
                     .RegisterType<IITPTrackerUnitOfWork, ITPTrackerUnitOfWork>()
                     .RegisterType<IITPTrackerDbContext, ITPTrackerDbContext>()
                     .RegisterType<IITPTrackerRepository, ITPTrackerRepository>()
                     .RegisterType<IProjectTrackerRepository, ProjectTrackerRepository>()
                     .RegisterType<IProjectTrackerService, ProjectTrackerService>()
                     .RegisterType<IProjectTrackerManager, ProjectTrackerManager>()
                     .RegisterType<IDashboardService, DashboardService>()
                     .RegisterType<IDashboardManager, DashboardManager>()
                     .RegisterType<IDashboardRepository, DashboardRepository>(); 

            //TODO Any cleaner way to do this - as this should not fire for every service
            if (!interceptorsAlreadySet)
            { 
                Logger.EnableUnityInterceptor(container, ConfigurationManager.AppSettings["ExceptionPolicyToUse"]);
                Logger.SetInteceptorFor<IITPTrackerService>();
            }

            interceptorsAlreadySet = true;
        }
    }    
}