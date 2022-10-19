using GHC.ProjectTracker.Common;
using GHC.ProjectTracker.IService;
using GHC.ProjectTracker.Model;
using GHC.ProjectTracker.Web.Common;
using GHC.ProjectTracker.Web.Controllers;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GHC.ProjectTracker.Web.Infrastructure.Mappers;
using System.Web.Http;
using System.DirectoryServices.AccountManagement;
using GHC.ProjectTracker.Web.Filters;
using GHC.ProjectTracker.Common.Authorization;
using GHC.ProjectTracker.Web.Infrastructure.Config;

namespace GHC.ProjectTracker.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Use custom MetadataProvider so display names will be spaced over CamelCase by default
            ModelMetadataProviders.Current = new CamelCaseModelMetadataProvider();

            // Set up EntLib 6 Logging
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
            Logger.SetLogWriter(logWriterFactory.Create());

            //Initialise mapper
            ITPTrackerMapper.Initialise();
           
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }
        
        protected void Session_Start()
        {
            IUnityContainer container = GHC.ProjectTracker.Web.App_Start.UnityConfig.GetConfiguredContainer();
            IITPTrackerService iTProjectTrackerService = container.Resolve<IITPTrackerService>();
            ISessionManager sessionManager = container.Resolve<ISessionManager>();
            IApplicationRoleProvider applicationRoleProvider = container.Resolve<IApplicationRoleProvider>();
            IConfigSettingsHelper configSettingsHelper = container.Resolve<IConfigSettingsHelper>();

            //User user = new User { UserName = "ZEPTO-JUNAID\\Junaid" };
            User user = new User { UserName = HttpContext.Current.User.Identity.Name };

#if !ZEPTO
            using (var pctx = new PrincipalContext(ContextType.Domain))
            {
                using (UserPrincipal up = UserPrincipal.FindByIdentity(pctx, HttpContext.Current.User.Identity.Name))
                {
                    user.FirstName = up.GivenName;
                    user.LastName = up.Surname;
                    user.EmailAddress = up.EmailAddress;
                }
            }

            applicationRoleProvider.Initialize(configSettingsHelper.GetApplicationADConfigFilePath());
      
            if (true)
            {
#endif
            user = iTProjectTrackerService.GetUpdateCreateUser(user);
            sessionManager.LoggedInUser = user;
#if !ZEPTO
            }
            else
            {
                HttpContext.Current.Response.Write("You are not authorised to access this application.");
                HttpContext.Current.Response.End();
            }
#endif
        }

        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
    }
}
