using GHC.DataPortal.Model.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.Business
{
    public interface IDashboardManager
    {
        ProjectPrioritiesChartData GetProjectPriorities(int userId, bool isTeamView);
        ProjectStatusChartData GetProjectStatus(int userId, bool isTeamView);
    }
}
