using System;
using System.Data.Entity;
using System.Linq;
using GHC.ProjectTracker.Model;
using GHC.ProjectTracker.Data.Context;
using GHC.ProjectTracker.Model.ITProjectTracker;
using GHC.ProjectTracker.Model.ITProjectTracker;
using GHC.ITPTracker.Model.ITProjectTracker;

namespace GHC.ProjectTracker.Data
{
    public interface IITPTrackerDbContext : IDbContext
    {
        IDbSet<User> Users { get; set; }
        IDbSet<SystemLookup> SystemLookups { get; set; }
        IDbSet<Menu> Menu { get; set; }
        IDbSet<RoleMenu> RoleMenu { get; set; }
        IDbSet<Country> Countries { get; set; }
        IDbSet<ITProjectTracker> ITProjectTrackers { get; set; }
        IDbSet<ProjectTrackerOpco> ProjectTrackerOpco { get; set; }
        IDbSet<ProjectTrackerReqDoc> ProjectTrackerReqDoc { get; set; }
        IDbSet<ProjectTrackerReview> ProjectTrackerReview { get; set; }
        IDbSet<WorkItem> WorkItems { get; set; }
        IDbSet<WorkItemLink> WorkItemLinks { get; set; }
    }
}
