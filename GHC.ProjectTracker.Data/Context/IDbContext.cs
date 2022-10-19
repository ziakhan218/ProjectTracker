using GHC.ProjectTracker.Model.ITProjectTracker;
using GHC.ProjectTracker.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using GHC.ITPTracker.Model.ITProjectTracker;
using GHC.DataPortal.Model.ITProjectTracker;

namespace GHC.ProjectTracker.Data.Context
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;


        IDbSet<User> Users { get; set; }
        IDbSet<SystemLookup> SystemLookups { get; set; }

        IDbSet<WorkItem> WorkItems{ get; set; }
        IDbSet<Developers> Developers { get; set; }

        IDbSet<WorkItemLink> WorkItemLinks { get; set; }
        int SaveChanges();
    }
}
