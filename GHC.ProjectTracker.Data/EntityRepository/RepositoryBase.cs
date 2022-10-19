using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using GHC.ProjectTracker.Data.Infrastructure;
using System.Data.Entity;
using GHC.ProjectTracker.Data.Context;

namespace GHC.ProjectTracker.Data.EntityRepository
{
    public abstract class RepositoryBase
    {
        private readonly IUnitOfWork unitOfWork;

        protected RepositoryBase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected IDbContext DataContext
        {
            get { return unitOfWork.Context; }
        }

        public virtual int ExecuteSql(string query, params object[] parameters)
        {
            return ((DbContext)unitOfWork.Context).Database.ExecuteSqlCommand(query, parameters);
        }

        public virtual IEnumerable<T> SqlQuery<T>(string query, params object[] parameters)
        {
            return ((DbContext)unitOfWork.Context).Database.SqlQuery<T>(query, parameters);
        }

        public virtual void SetModified(object entity)
        { 
            if(entity != null)
                ((DbContext)unitOfWork.Context).Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void SetModified(object entity, params string[] properties)
        {
            var entry = ((DbContext)unitOfWork.Context).Entry(entity);
            entry.State = System.Data.Entity.EntityState.Unchanged;
            foreach (var property in properties)
            {
                entry.Property(property).IsModified = true;
            }
            ((DbContext)unitOfWork.Context).Configuration.ValidateOnSaveEnabled = false;

        }

        public virtual void SetUnChanged(object entity)
        { 
            if(entity != null)
                ((DbContext)unitOfWork.Context).Entry(entity).State = System.Data.Entity.EntityState.Unchanged;
        }

        public virtual void SetDetached(object entity)
        { 
            if(entity != null)
                ((DbContext)unitOfWork.Context).Entry(entity).State = System.Data.Entity.EntityState.Detached;
        }

        public virtual void SetAdded(object entity)
        { 
            if(entity != null)
                ((DbContext)unitOfWork.Context).Entry(entity).State = System.Data.Entity.EntityState.Added;
        }

        public virtual void SetDeleted(object entity)
        { 
            if(entity != null)
                ((DbContext)unitOfWork.Context).Entry(entity).State = System.Data.Entity.EntityState.Deleted;
        }
    }
}
