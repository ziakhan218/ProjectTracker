using GHC.ProjectTracker.Model.ITProjectTracker;
using GHC.ProjectTracker.Data.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.Data.EntityConfiguration.ITProjectTracker
{
    public class WorkItemLinkEntityConfiguration : EntityConfiguration<WorkItemLink>
    {
        public WorkItemLinkEntityConfiguration()
        {
            ToTable("WorkItemLink");
            HasKey(x => x.Id);
        }
    }
}
