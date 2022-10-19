using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHC.ProjectTracker.Model;
using GHC.ProjectTracker.Model.ITProjectTracker;
using GHC.ITPTracker.Model.ITProjectTracker;

namespace GHC.ProjectTracker.Data
{
    public interface IProjectTrackerRepository
    {

        IEnumerable<ITProjectTracker> GetAllProjects();
        IEnumerable<SystemLookup> GetDropdownOpt(List<string> categories);
        ITProjectTracker GetProjectById(string projectId);
        int[] GetAllOpCo(int projectId);
        int[] GetAllReview(int projectId);
        int[] RequiredDocumentation(int projectId);
        int[] DeveloperIds(int projectId);
        bool ProjectName(string name);
        bool SaveProject(ITProjectTracker project, int[] reviews, int[] opCo, int[] reqDoc);
        bool DeleteProjects(int[] projects);
        bool SaveWorkItems(IList<WorkItem> workItems, IList<WorkItemLink> workItemlinks);
        bool SaveProjectResource(int[] Developers, ITProjectTracker Project);


    }
}
