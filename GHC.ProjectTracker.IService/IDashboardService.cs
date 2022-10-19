using GHC.DataPortal.Model.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GHC.DataPortal.IService
{
    [ServiceContract]
    public interface IDashboardService
    {
        [OperationContract]
        ProjectPrioritiesChartData GetProjectPriorities(int userId, bool isTeamView);

        [OperationContract]
        ProjectStatusChartData GetProjectStatus(int userId, bool isTeamView);
    }
}
