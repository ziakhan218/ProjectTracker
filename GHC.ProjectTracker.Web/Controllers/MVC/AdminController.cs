using GHC.ProjectTracker.Common.Authorization;
using GHC.ProjectTracker.IService;
using GHC.ProjectTracker.Web.Common;
using GHC.ProjectTracker.Web.Controllers.MVC;
using GHC.ProjectTracker.Web.Filters;
using GHC.ProjectTracker.Web.Filters.MVC;
using GHC.ProjectTracker.Web.Infrastructure.Config;
using GHC.ProjectTracker.Web.Infrastructure.Mappers;
using System.Web.Mvc;


namespace GHC.ProjectTracker.Web.Controllers
{
    
    public class AdminController : BaseController
    {
        private readonly IITPTrackerService iTProjectTrackerService;
        private readonly ISessionManager sessionManager;
        private IITPTrackerMapper mapper;

        public AdminController(IITPTrackerService iTProjectTrackerService, ISessionManager sessionManager, IITPTrackerMapper mapper,
                                        IConfigSettingsHelper configSettingsHelper, IApplicationRoleProvider applicationRoleProvider)
                            : base(configSettingsHelper, applicationRoleProvider, sessionManager)
        {
            this.iTProjectTrackerService = iTProjectTrackerService;
            this.sessionManager = sessionManager;
            this.mapper = mapper;
        }

        //[AuthorizeCustom(Roles.ITProjectTrackerAdmin)]
        public ActionResult Index()
        {
            return View();
        }
    }
}