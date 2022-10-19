using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHC.ProjectTracker.Model;
using GHC.ProjectTracker.Model.ITProjectTracker;

namespace GHC.ProjectTracker.Business
{
    public interface IProjectTrackerManager
    {
        IEnumerable<ITProjectTracker> GetAllProjects();
        IEnumerable<SystemLookup> GetDropdownOpt(List<string> categories);
        ITProjectTracker GetProjectById(string projectId);
        int[] GetAllOpCo(int projectId);
        int[] GetAllReview(int projectId);
        int[] RequiredDocumentation(int projectId);
        int[] DeveloperIds(int projectId);        
        bool SaveProject(ITProjectTracker project, int[] reviews, int[] opCo, int[] reqDoc);
        bool DeleteProjects(int[] projects);
        bool ProjectName(string name);
        List<WorkItemandChildLinks> GetAllBacklogItems(string projectName);
        bool SaveWorkItems(string ProjectName); 
        bool SaveProjectResource(int[] Developers,ITProjectTracker Project);
    }
}
