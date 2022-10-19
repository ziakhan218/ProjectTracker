using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHC.ProjectTracker.Data.EntityConfigurations;
using GHC.ProjectTracker.Model;

namespace GHC.ProjectTracker.Data.EntityConfigurations
{
    public class RoleMenuEntityConfiguration : EntityConfiguration<RoleMenu>
    {
        public RoleMenuEntityConfiguration()
        {
            ToTable("RoleMenu");
            HasKey(x => x.Id);
            HasRequired(a => a.Menu).WithMany().HasForeignKey(c => c.MenuId);
            HasRequired(a => a.Role).WithMany().HasForeignKey(c => c.RoleId);
        }
    }
}
