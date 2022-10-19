using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GHC.ProjectTracker.Model;
using GHC.ProjectTracker.Model.ITProjectTracker;

namespace GHC.ProjectTracker.IService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProjectTrackerService" in both code and config file together.
    [ServiceContract]
    public interface IProjectTrackerService
    {
        [OperationContract]
        IEnumerable<ITProjectTracker> GetAllProjects();

        [OperationContract]
        IEnumerable<SystemLookup> GetDropdownOpt(List<string> categories);

        [OperationContract]
        ITProjectTracker GetProjectById(string projectId);

        [OperationContract]
        int[] GetAllOpCo(int projectId);

        [OperationContract]
        int[] GetAllReview(int projectId);

        [OperationContract]
        int[] RequiredDocumentation(int projectId);
        [OperationContract]
        int[] DeveloperIds(int projectId);
        
        [OperationContract]
        bool SaveProject(ITProjectTracker project, int[] reviews, int[] opCo, int[] reqDoc);

        [OperationContract]
        bool DeleteProjects(int[] projects);

        [OperationContract]
        bool ProjectName(string name);
        [OperationContract]
        List<WorkItemandChildLinks> GetAllBacklogItems(string projectName);

        [OperationContract]
        bool SaveWorkItems(string ProjectName);

        [OperationContract]
        bool SaveProjectResource(int[] Developers, ITProjectTracker Project);
    }
}
