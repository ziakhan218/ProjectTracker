using GHC.ProjectTracker.Common.Authorization;
using GHC.ProjectTracker.Web.Common;
using GHC.ProjectTracker.Web.Filters;
using GHC.ProjectTracker.Web.Filters.MVC;
using GHC.ProjectTracker.Web.Infrastructure.Config;
using System.Web.Mvc;


namespace GHC.ProjectTracker.Web.Controllers.MVC
{
    //[AuthorizeCustom(Roles.ITProjectTrackerUser)]
    public class ITProjectTrackerController : BaseController
    {
        // GET: ITProjectTracker
        public ITProjectTrackerController(IConfigSettingsHelper configSettingsHelper, IApplicationRoleProvider applicationRoleProvider, ISessionManager sessionManager)
                                                                            : base(configSettingsHelper, applicationRoleProvider, sessionManager)
        {
        }

        //[AuthorizeCustom(Roles.ITProjectTrackerAdmin)]
        public ActionResult Index()
        {
            return View();
        }
    }
}