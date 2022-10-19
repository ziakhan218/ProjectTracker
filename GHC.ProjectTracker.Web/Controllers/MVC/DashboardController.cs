using GHC.ProjectTracker.Common.Authorization;
using GHC.ProjectTracker.Web.Common;
using GHC.ProjectTracker.Web.Controllers.MVC;
using GHC.ProjectTracker.Web.Infrastructure.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GHC.DataPortal.Web.Controllers.MVC
{
    public class DashboardController : BaseController
    {
        public DashboardController(IConfigSettingsHelper configSettingsHelper, IApplicationRoleProvider applicationRoleProvider, ISessionManager sessionManager)
                                                                            : base(configSettingsHelper, applicationRoleProvider, sessionManager)
        {
        }
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        
    }
}