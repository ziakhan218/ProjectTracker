using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GHC.ProjectTracker.Business;
using GHC.ProjectTracker.IService;
using GHC.ProjectTracker.Model;
using GHC.ProjectTracker.Model.ITProjectTracker;

namespace GHC.ProjectTracker.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProjectTrackerService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProjectTrackerService.svc or ProjectTrackerService.svc.cs at the Solution Explorer and start debugging.
    public class ProjectTrackerService : IProjectTrackerService
    {
        private readonly IProjectTrackerManager projectTrackerManager;
        public ProjectTrackerService(IProjectTrackerManager projectTrackerManager)
        {
            this.projectTrackerManager = projectTrackerManager;
        }
        public IEnumerable<ITProjectTracker> GetAllProjects()
        {
            return projectTrackerManager.GetAllProjects();
        }

        public IEnumerable<SystemLookup> GetDropdownOpt(List<string> categories)
        {
            return projectTrackerManager.GetDropdownOpt(categories);
        }

        public ITProjectTracker GetProjectById(string projectId)
        {
            return projectTrackerManager.GetProjectById(projectId);
        }
        public bool SaveProject(ITProjectTracker project, int[] reviews, int[] opCo, int[] reqDoc)
        {
            return projectTrackerManager.SaveProject(project, reviews, opCo, reqDoc);
        }

        public bool DeleteProjects(int[] projects)
        {
            return projectTrackerManager.DeleteProjects(projects);
        }

        public int[] GetAllOpCo(int projectId)
        {
            return projectTrackerManager.GetAllOpCo(projectId);
        }

        public int[] GetAllReview(int projectId)
        {
            return projectTrackerManager.GetAllReview(projectId);
        }

        public int[] RequiredDocumentation(int projectId)
        {
            return projectTrackerManager.RequiredDocumentation(projectId);
        }
        public int[] DeveloperIds(int projectId)
        {
            return projectTrackerManager.DeveloperIds(projectId);
        }
        
        public bool ProjectName(string name)
        {
            return projectTrackerManager.ProjectName(name);
        }

        public List<WorkItemandChildLinks> GetAllBacklogItems(string projectName)
        {
            return projectTrackerManager.GetAllBacklogItems(projectName);
        }

     

        public bool SaveWorkItems(string ProjectName)
        {
            return projectTrackerManager.SaveWorkItems(ProjectName);
        }

        public bool SaveProjectResource(int[] Developers, ITProjectTracker Project)
        {
            return projectTrackerManager.SaveProjectResource(Developers, Project);
        }
    }
}
