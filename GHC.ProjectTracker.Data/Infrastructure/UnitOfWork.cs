using GHC.ProjectTracker.Data.Context;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace GHC.ProjectTracker.Data.Infrastructure
{
    /// <summary>
    /// IUnitOfWork wraps a IDbContext
    /// The 'unit of work' refers to a database transaction
    /// </summary>
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        protected IDbContext dbContext;

        public UnitOfWork(IDbContext context)
        {
            this.dbContext = context;
        }

        public void Dispose()
        {
        }

        public bool SaveChanges()
        {
            try
            {
                dbContext.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
        }

        public IDbContext Context
        {
            get { return dbContext; }
        }
    }
}
