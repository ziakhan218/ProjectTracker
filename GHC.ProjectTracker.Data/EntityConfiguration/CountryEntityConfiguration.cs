using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHC.ProjectTracker.Model;

namespace GHC.ProjectTracker.Data.EntityConfigurations
{
    public class CountryEntityConfiguration : EntityConfiguration<Country>
    {
        public CountryEntityConfiguration()
        {
            ToTable("Country");
            HasKey(x => x.Id);
            Property(x => x.Name).IsRequired();
            Property(x => x.AreaUnit).IsRequired();
            Property(x => x.Currency).IsRequired();
            Property(x => x.Vat).IsRequired();
            Property(x => x.Region).IsRequired();
        }
    }
}
