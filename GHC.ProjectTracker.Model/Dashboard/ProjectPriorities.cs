using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.DataPortal.Model.Dashboard
{
    public class ProjectPriorities
    {
        public int DevelopmentId { get; set; }
        public string Name { get; set; }
        public string Priority { get; set; }
        public string ProjectTeam { get; set; }
    }
    public class ProjectPrioritiesChartData
    {
        public List<ProjectPriorities> RawData { get; set; }
        public List<KeyValuePair<string, int>> ChartCounts { get; set; }
    }
}
