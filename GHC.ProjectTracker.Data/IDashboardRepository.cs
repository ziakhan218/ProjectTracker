using GHC.DataPortal.Model.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.DataPortal.Data
{
    public interface IDashboardRepository
    {
        ProjectPrioritiesChartData GetProjectPriorities(int userId, bool isTeamView);

        ProjectStatusChartData GetProjectStatus(int userId, bool isTeamView);
    }
}
