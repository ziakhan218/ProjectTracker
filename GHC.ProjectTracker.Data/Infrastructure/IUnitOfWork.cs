using GHC.ProjectTracker.Data.Context;
using System;
using System.Linq;

namespace GHC.ProjectTracker.Data.Infrastructure
{
    
    public interface IUnitOfWork
    {

        bool SaveChanges();

        
        IDbContext Context { get; }
    }
}
