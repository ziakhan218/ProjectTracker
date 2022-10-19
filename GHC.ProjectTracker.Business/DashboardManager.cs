using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHC.DataPortal.Model.Dashboard;
using GHC.DataPortal.Data;

namespace GHC.ProjectTracker.Business
{
    public class DashboardManager : IDashboardManager
    {
        private readonly IDashboardRepository repository;
        public DashboardManager(IDashboardRepository dashboardRepository)
        {
            this.repository = dashboardRepository;
        }
        public ProjectPrioritiesChartData GetProjectPriorities(int userId, bool isTeamView)
        {
            return repository.GetProjectPriorities(userId, isTeamView);
        }

        public ProjectStatusChartData GetProjectStatus(int userId, bool isTeamView)
        {
            return repository.GetProjectStatus(userId, isTeamView);
        }
    }
}
