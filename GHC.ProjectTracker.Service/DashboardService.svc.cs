using GHC.DataPortal.IService;
using GHC.DataPortal.Model.Dashboard;
using GHC.ProjectTracker.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GHC.DataPortal.Service
{
    
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardManager dashboardManager;

        // Constructor for Dependency Injection - only works in conjunction with a custom ServiceHostFactory (WcfServiceFactory.cs)
        public DashboardService(IDashboardManager propertyManager)
        {
            this.dashboardManager = propertyManager;
        }
        public ProjectPrioritiesChartData GetProjectPriorities(int userId, bool isTeamView)
        {
            return dashboardManager.GetProjectPriorities(userId, isTeamView);
        }
        public ProjectStatusChartData GetProjectStatus(int userId, bool isTeamView)
        {
            return dashboardManager.GetProjectStatus(userId, isTeamView);
        }
    }
}
