using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHC.ProjectTracker.Model.ITProjectTracker;

namespace GHC.ProjectTracker.Data.EntityConfigurations
{
    class ProjectTrackerReqDocEntityConfiguration:EntityConfiguration<ProjectTrackerReqDoc>
    {
        public ProjectTrackerReqDocEntityConfiguration()
        {
            ToTable("ProjectTrackerReqDoc");
            HasKey(x => x.Id);
        }
    }
}
