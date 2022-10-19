using GHC.DataPortal.IService;
using GHC.DataPortal.Model.Dashboard;
using GHC.DataPortal.Web.Infrastructure.Mappers;
using GHC.ProjectTracker.Web.Common;
using GHC.ProjectTracker.Web.Filters;
using GHC.ProjectTracker.Web.Filters.API;
using GHC.ProjectTracker.Web.Infrastructure.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GHC.DataPortal.Web.Controllers.API
{
    [RoutePrefix("api")]
    public class DashboardApiController : ApiController
    {
        private readonly IDashboardService DashboardService;
        private IDashboardMapper Mapper;
        private ISessionManager SessionManager;
        private string AttachmentsPath;

        public DashboardApiController(IConfigSettingsHelper configSettingsHelper, IDashboardService dashboardService, IDashboardMapper mapper, ISessionManager sessionManager)
        {
            this.DashboardService = dashboardService;
            this.Mapper = mapper;
            this.SessionManager = sessionManager;
            //this.AttachmentsPath = configSettingsHelper.GetWLSAttachmentsPath();
        }


        [HttpGet]
        [Route("getprojectStatusData/{isTeamView}")]
        //[AuthorizeCustom(Roles.DevelopmentPortalAdmin, Roles.DevelopmentPortalUser)]
        public HttpResponseMessage GetProjectStatus(HttpRequestMessage request, bool isTeamView)
        {
            var userId = SessionManager.LoggedInUser.Id;
            var result = DashboardService.GetProjectStatus(userId, isTeamView);
            return request.CreateResponse<ProjectStatusChartData>(HttpStatusCode.OK, result);
        }
    }
}
