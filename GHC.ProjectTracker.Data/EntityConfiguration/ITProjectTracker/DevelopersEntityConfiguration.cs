using GHC.DataPortal.Model.ITProjectTracker;
using GHC.ProjectTracker.Data.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.DataPortal.Data.EntityConfiguration.ITProjectTracker
{
    public class DevelopersEntityConfiguration : EntityConfiguration<Developers>
    {
        public DevelopersEntityConfiguration()
        {
            ToTable("Developers");
            HasKey(x => x.Id);
        }
    }
}
