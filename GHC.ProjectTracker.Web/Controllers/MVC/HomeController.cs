using GHC.ProjectTracker.IService;
using GHC.ProjectTracker.Web.Infrastructure.Mappers;
using System.Web.Mvc;
using GHC.ProjectTracker.Web.Common;
using GHC.ProjectTracker.Web.Filters.MVC;
using GHC.ProjectTracker.Common.Authorization;
using GHC.ProjectTracker.Web.Filters;
using GHC.ProjectTracker.Web.Controllers.MVC;
using GHC.ProjectTracker.Web.Infrastructure.Config;
using System;
using System.Collections.Generic;
using GHC.ProjectTracker.Model.Enums;

namespace GHC.ProjectTracker.Web.Controllers
{
    //[HandleError, AuthorizeCustom(Roles.ITProjectTrackerAdmin)]
    public class HomeController : BaseController
    {
        private readonly IITPTrackerService iTProjectTrackerService;
        private readonly ISessionManager sessionManager;
        private IITPTrackerMapper mapper;

        public HomeController(IITPTrackerService iTProjectTrackerService, ISessionManager sessionManager, IITPTrackerMapper mapper
                                , IConfigSettingsHelper configSettingsHelper, IApplicationRoleProvider applicationRoleProvider)
                            : base(configSettingsHelper, applicationRoleProvider, sessionManager)
        {
            this.iTProjectTrackerService = iTProjectTrackerService;
            this.sessionManager = sessionManager;
            this.mapper = mapper;

        }
 
        //[AuthorizeCustom(Roles.ITProjectTrackerAdmin)]
        public ActionResult Index()
        {
            
                return Redirect("/ITProjectTracker/#/ProjectTrackerView");
            
        }

        public JsonResult GetMenu()
        {
            List<int> roleIds = new List<int>();

            foreach (var x in Enum.GetValues(typeof(UserRole)))
            {
#if !ZEPTO
                var roleParts = x.ToString().Split('_');
                var roleName = roleParts[roleParts.Length - 1];
                var application = roleParts[roleParts.Length - 2];
                if (User.IsInRole(applicationRoleProvider.GetRoleADGroup(new Role { Application = application, Name = roleName })))
                {
                    roleIds.Add((int)x);
                }
#else
                roleIds.Add((int)x);
#endif
            }

            var res = iTProjectTrackerService.GetMenu(roleIds.ToArray());

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}