using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHC.ProjectTracker.Model.ITProjectTracker;

namespace GHC.ProjectTracker.Data.EntityConfigurations
{
    class ProjectTrackerOpcoEntityConfiguration:EntityConfiguration<ProjectTrackerOpco>
    {
        public ProjectTrackerOpcoEntityConfiguration()
        {
            ToTable("ProjectTrackerOpco");
            HasKey(x => x.Id);
        }
    }
}
