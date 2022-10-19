using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GHC.ProjectTracker.IService;
using GHC.ProjectTracker.Web.Common;
using GHC.ProjectTracker.Web.Filters;
using GHC.ProjectTracker.Web.Filters.API;
using GHC.ProjectTracker.Web.Models.ProjectTracker;
using GHC.ITPTracker.Model.ITProjectTracker;
using GHC.ProjectTracker.Model.ITProjectTracker;

namespace GHC.ProjectTracker.Web.Controllers.API
{
    [RoutePrefix("api")]
    //[Authorize]
    public class ITProjectTrackerApiController : ApiController
    {
        private readonly IProjectTrackerService projectTrackerService;
        private ISessionManager sessionManger;
        public ITProjectTrackerApiController(IProjectTrackerService projectTrackerService, ISessionManager sessionManager)
        {
            this.projectTrackerService = projectTrackerService;
            this.sessionManger = sessionManager;
        }

        public ITProjectTrackerApiController()
        {

        }

        [HttpGet]
        [Route("getAllProjects")]
        //[AuthorizeCustom(Roles.ITProjectTrackerAdmin)]
        public HttpResponseMessage GetAllProjects()
        {
            var data = projectTrackerService.GetAllProjects();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("getDropdownOpt")]
        //[AuthorizeCustom(Roles.ITProjectTrackerAdmin)]
        public HttpResponseMessage GetDropDownOpt(HttpRequestMessage request, [FromUri]string[] categoryList)
        {
            var data = projectTrackerService.GetDropdownOpt(categoryList.ToList());
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("getAllIds")]
        //[AuthorizeCustom(Roles.ITProjectTrackerAdmin)]
        public HttpResponseMessage GetAllOpCo(int Id)
        {
            var result = new
            {
                OpCoId = projectTrackerService.GetAllOpCo(Id),
                Review = projectTrackerService.GetAllReview(Id),
                DocumentaionId = projectTrackerService.RequiredDocumentation(Id),

            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpGet]
        [Route("getDeveloperIds")]
        public HttpResponseMessage GetDeveloperIds(int Id)
        {
            var res = projectTrackerService.DeveloperIds(Id);
            return Request.CreateResponse(HttpStatusCode.OK, res);
                
        }

        [HttpGet]
        [Route("getProjectByName")]
        //[AuthorizeCustom(Roles.ITProjectTrackerAdmin)]
        public HttpResponseMessage GetProjectByName(string name)
        {
            var result = projectTrackerService.GetProjectById(name);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("saveProject")]
        //[AuthorizeCustom(Roles.ITProjectTrackerAdmin)]
        public HttpResponseMessage SaveProject(ProjectInputParams inputParams)
        {
            inputParams.Project.ProjectAddedDate = DateTime.Now;
            inputParams.Project.DateModified = DateTime.Now;
            inputParams.Project.ModifiedBy = sessionManger.LoggedInUser.Id;
            var data = projectTrackerService.SaveProject(inputParams.Project, inputParams.Review, inputParams.OpCo, inputParams.ReqDoc);
            return Request.CreateResponse(HttpStatusCode.OK, inputParams);
        }
       
        [HttpPost]
        [Route("deleteProjects")]
        //[AuthorizeCustom(Roles.ITProjectTrackerAdmin)]
        public HttpResponseMessage DeleteProjects(int[] ids)
        {
            var data = projectTrackerService.DeleteProjects(ids);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("projectName")]
        //[AuthorizeCustom(Roles.ITProjectTrackerAdmin)]
        public HttpResponseMessage projectName(string projectName)
        {
            var data = projectTrackerService.ProjectName(projectName);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }


        [HttpGet]
        [Route("getAllBacklogItems")]
        public HttpResponseMessage GetAllBacklogItems(string projectName)
        {
            var data = projectTrackerService.GetAllBacklogItems(projectName);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpPost]
        [Route("saveWorkItem")]
        //[AuthorizeCustom(Roles.ITProjectTrackerAdmin)]
        public HttpResponseMessage SaveWorkItem(WorkItem ProjectName)
        {
            projectTrackerService.SaveWorkItems(ProjectName.ProjectName);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }


        [HttpPost]
        [Route("saveProjectResource")]
        public HttpResponseMessage SaveProjectResource(ProjectInputParams inputParams)
        {
            projectTrackerService.SaveProjectResource(inputParams.Developers, inputParams.Project);
            return null;
        }

    }
}
