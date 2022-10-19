using GHC.DataPortal.Data.Infrastructure;
using GHC.DataPortal.Model.Dashboard;
using GHC.ProjectTracker.Data;
using GHC.ProjectTracker.Data.EntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.DataPortal.Data
{
    public class DashboardRepository : RepositoryBase, IDashboardRepository
    {
        private IDashboardUnitOfWork unitOfWork;

        public DashboardRepository(IDashboardUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ProjectPrioritiesChartData GetProjectPriorities(int userId, bool isTeamView)
        {

            var dto = (from sl in ((IITPTrackerDbContext)DataContext).SystemLookups
                       join pt in ((IITPTrackerDbContext)DataContext).ITProjectTrackers
                       on sl.Id equals pt.Status into lpt from t in lpt.DefaultIfEmpty()
                       where sl.Category == "ProjectTracker.RiskRegisterFlag"
                       select new
                       {
                           ProjectName = t.ProjectName,
                           Risk = sl.Name,
                           RiskId = t == null ? 0 : t.RiskRegisterFlag
                       }).ToList();

            ProjectPrioritiesChartData data = new ProjectPrioritiesChartData
            {
                RawData = dto.Select(x =>
                new ProjectPriorities
                {
                    //Status = x.Risk,
                    //StatusId = x.RiskId ?? 0,
                    ProjectTeam = x.ProjectName
                }).ToList(),
                ChartCounts = dto.GroupBy(gp => gp.Risk)
                   .Select(cc => new KeyValuePair<string, int>(cc.Key, cc.Count())
                 ).ToList()
            };
            //return data;
            return null;
        }

        public ProjectStatusChartData GetProjectStatus(int userId, bool isTeamView)
        {
            var dto = (from sl in ((IITPTrackerDbContext)DataContext).SystemLookups
                       join pt in ((IITPTrackerDbContext)DataContext).ITProjectTrackers
                       on sl.Id equals pt.Status into lpt from t in lpt.DefaultIfEmpty()
                       where sl.Category == "ProjectTracker.Status"
                       select new
                       {
                           ProjectName = t.ProjectName,
                           Status = sl.Name,
                           StatusId = t == null ? 0 : t.Status
                       }).ToList();

            ProjectStatusChartData data = new ProjectStatusChartData
            {
                RawData = dto.Select(x =>
                new ProjectStatus
                {
                    Status = x.Status,
                    StatusId = x.StatusId ?? 0,
                    ProjectTeam = x.ProjectName
                }).ToList(),
                ChartCounts = dto.GroupBy(gp => gp.Status)
                   .Select(cc => new KeyValuePair<string, int>(cc.Key, cc.Count())
                 ).ToList()
            };
            return data;
        }

        public List<DashboardProjectModalDetail> GetDashboardProjectModalDetails(List<int> developmentIds, int statusId)
        {

            var dto = (from d in ((IITPTrackerDbContext)DataContext).ITProjectTrackers
                                        .Where(x => x.Status == statusId)
                       
                       select new
                       {
                           Development = d,
                           OppInfo = oi,
                           FSector = fsector,
                           CStatus = status,
                           Programme = p,
                           DevelopmentValue = dv,
                           AssetManager = am,
                           CostApproval = cna,
                           Financial = f,
                           DevManager = dm,
                           ProjectManager = pm,
                           ProjectTeam = pt.Name
                       }).ToList();


            List<DashboardProjectModalDetail> item = dto.Select(x => new DashboardProjectModalDetail
            {
                DevelopmentId = x.Development.Id,
                DevelopmentName = x.Development.Name,
                ProjectTeam = x.ProjectTeam,
                DevelopmentManager = x.DevManager?.FullName,
                ProjectManager = x.ProjectManager?.FullName,
                AssetManager = x.AssetManager?.Name,
                ProposedSector = x.FSector?.Name,
                ProposedGIA = x.OppInfo?.GIAsqft != null ? Convert.ToString(x.OppInfo?.GIAsqft) : "",
                ForecastERV = x.DevelopmentValue?.ActualRental != null ? Convert.ToString(x.DevelopmentValue.ActualRental) : "",
                ForecastNDV = x.DevelopmentValue?.ActualNDVGrown != null ? Convert.ToString(x.DevelopmentValue.ActualNDVGrown) : "",
                CurrentStatus = x.CStatus?.Name,
                PlanningDetermination = x.Programme?.DeterminationDate != null ? x.Programme?.DeterminationDate.Value.ToString("dd MMM yyyy") : "",
                StartOnSite = x.Programme?.MainContractStartDate != null ? x.Programme?.MainContractStartDate.Value.ToString("dd MMM yyyy") : "",
                PracticalCompletion = x.Programme?.ActualPCDate != null ? x.Programme?.ActualPCDate.Value.ToString("dd MMM yyyy") : "",
                GrossYieldOnCost = x.Financial?.ActualGrossYieldCost != null ? Convert.ToString(x.Financial.ActualGrossYieldCost) + "%" : "",
                NetMarginalYield = x.Financial?.ActualMarginalYield != null ? Convert.ToString(x.Financial.ActualMarginalYield) + "%" : "",
                ValueAdd = x.Financial?.ActualValueAdd != null ? Convert.ToString(x.Financial.ActualValueAdd) : "",
                NPVHurdle = x.Financial?.ActualNPV != null ? Convert.ToString(x.Financial.ActualNPV) : ""
            }).ToList();

            return item;
        }
    }
}
