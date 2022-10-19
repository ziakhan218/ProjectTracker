using GHC.ITPTracker.Model.ITProjectTracker;
using GHC.ProjectTracker.Data.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.Data.EntityConfiguration.ITProjectTracker
{
    public class WorkItemEntityConfiguration : EntityConfiguration<WorkItem>
    {
        public WorkItemEntityConfiguration()
        {
            ToTable("WorkItem");
            HasKey(x => x.Id);
        }
    }
}
