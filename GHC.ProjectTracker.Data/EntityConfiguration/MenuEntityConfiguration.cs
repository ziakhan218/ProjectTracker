using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHC.ProjectTracker.Model;

namespace GHC.ProjectTracker.Data.EntityConfigurations
{
    public class MenuEntityConfiguration : EntityConfiguration<Menu>
    {
        public MenuEntityConfiguration()
        {
            ToTable("Menu");
            HasKey(x => x.Id);
            Property(x => x.ModuleName).IsRequired();
            Property(x => x.ModulePath).IsRequired();
            Property(x => x.ParentId);
            Property(x => x.SequenceNumber).IsRequired();
        }
    }
}
