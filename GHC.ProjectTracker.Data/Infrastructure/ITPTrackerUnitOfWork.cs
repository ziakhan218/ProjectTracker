using GHC.ProjectTracker.Data.Context;
using System;
using System.Linq;

namespace GHC.ProjectTracker.Data.Infrastructure
{
    /// <summary>
    /// IUnitOfWork wraps a IDbContext
    /// The 'unit of work' refers to a database transaction
    /// </summary>
    public sealed class ITPTrackerUnitOfWork : UnitOfWork, IDisposable, IITPTrackerUnitOfWork
    {
        public ITPTrackerUnitOfWork(IITPTrackerDbContext context) : base(context)
        {
        }
    }
}
