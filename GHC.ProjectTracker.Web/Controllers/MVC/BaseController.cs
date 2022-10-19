using GHC.Common.Logger;
using GHC.ProjectTracker.Common.Authorization;
using GHC.ProjectTracker.Web.Common;
using GHC.ProjectTracker.Web.Filters;
using GHC.ProjectTracker.Web.Infrastructure.Config;
using System.Web.Mvc;

namespace GHC.ProjectTracker.Web.Controllers.MVC
{
    public class BaseController : Controller
    {
        protected IConfigSettingsHelper configSettingsHelper;
        protected IApplicationRoleProvider applicationRoleProvider;
        private ISessionManager sessionManager;

        public BaseController(IConfigSettingsHelper configSettingsHelper, IApplicationRoleProvider applicationRoleProvider, ISessionManager sessionManager)
        {
            this.configSettingsHelper = configSettingsHelper;
            this.applicationRoleProvider = applicationRoleProvider;
            this.sessionManager = sessionManager;

            applicationRoleProvider.Initialize(configSettingsHelper.GetApplicationADConfigFilePath());
        }

        protected override void HandleUnknownAction(string actionName)
        {
            HttpContext.Server.TransferRequest("/UploadConfig", true);
        }

#if !ZEPTO

#else
        
#endif

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.UserWelcomeNote = sessionManager.GetUserWelcomeNote();
#if !ZEPTO
#else
            ViewBag.IsITPTrackerAdministrator = true;            
            ViewBag.IsITPTrackerUser = true;

          
#endif
            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Logger.Error(string.Format("{0}. \r\n {1}", filterContext.Exception.Message, filterContext.Exception.StackTrace));
            base.OnException(filterContext);
        }
    }
}
