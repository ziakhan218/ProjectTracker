using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using GHC.ProjectTracker.Data.EntityConfigurations;
using GHC.ProjectTracker.Model;
using GHC.ProjectTracker.Model.ITProjectTracker;
using GHC.ProjectTracker.Data.EntityConfiguration.ITProjectTracker;
using GHC.ITPTracker.Model.ITProjectTracker;
using GHC.DataPortal.Model.ITProjectTracker;
using GHC.DataPortal.Data.EntityConfiguration.ITProjectTracker;

namespace GHC.ProjectTracker.Data
{
    public class ITPTrackerDbContext :  DbContext, IITPTrackerDbContext
    {
        public ITPTrackerDbContext()
            : base("name=ITProjectTracker")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<SystemLookup> SystemLookups { get; set; }
        public IDbSet<Menu> Menu { get; set; }
        public IDbSet<RoleMenu> RoleMenu { get; set; }
        public IDbSet<Country> Countries { get; set; }
        public IDbSet<Developers> Developers { get; set; }
        public IDbSet<ITProjectTracker> ITProjectTrackers { get; set; }
        public IDbSet<ProjectTrackerOpco> ProjectTrackerOpco { get; set; }
        public IDbSet<ProjectTrackerReqDoc> ProjectTrackerReqDoc { get; set; }
        public IDbSet<ProjectTrackerReview> ProjectTrackerReview { get; set; }

        public IDbSet<WorkItem> WorkItems{ get; set; }
        public IDbSet<WorkItemLink> WorkItemLinks { get; set; }
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new UserEntityConfiguration());
            modelBuilder.Configurations.Add(new CountryEntityConfiguration());
            modelBuilder.Configurations.Add(new SystemLookupEntityConfiguration());
            modelBuilder.Configurations.Add(new MenuEntityConfiguration());
            modelBuilder.Configurations.Add(new RoleMenuEntityConfiguration());

            modelBuilder.Configurations.Add(new ProjectTrackerEntityConfiguration());
            modelBuilder.Configurations.Add(new ProjectTrackerOpcoEntityConfiguration());
            modelBuilder.Configurations.Add(new ProjectTrackerReqDocEntityConfiguration());
            modelBuilder.Configurations.Add(new ProjectTrackerReviewEntityConfiguration());
            modelBuilder.Configurations.Add(new WorkItemEntityConfiguration());
            modelBuilder.Configurations.Add(new WorkItemLinkEntityConfiguration());
            modelBuilder.Configurations.Add(new DevelopersEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
