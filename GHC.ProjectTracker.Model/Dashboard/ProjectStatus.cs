using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.DataPortal.Model.Dashboard
{
    public class ProjectStatus
    {
        public int DevelopmentId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public string ProjectTeam { get; set; }
    }

    public class ProjectStatusChartData
    {
        public List<ProjectStatus> RawData { get; set; }
        public List<KeyValuePair<string, int>> ChartCounts { get; set; }
    }
}
