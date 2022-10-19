using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GHC.ProjectTracker.Model;

namespace GHC.ProjectTracker.Data.EntityConfigurations
{
    public class SystemLookupEntityConfiguration : EntityConfiguration<SystemLookup>
    {
        public SystemLookupEntityConfiguration()
        {
            ToTable("SystemLookup");
            HasKey(b => b.Id);
            Property(t => t.Name).IsRequired();
            Property(t => t.Category).IsRequired();
            Property(t => t.IsActive).IsRequired();
        }
    }
}
