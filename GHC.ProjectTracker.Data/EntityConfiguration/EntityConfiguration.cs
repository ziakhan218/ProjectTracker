using System;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace GHC.ProjectTracker.Data.EntityConfigurations
{
    public abstract class EntityConfiguration<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : class
    {
    }
}
