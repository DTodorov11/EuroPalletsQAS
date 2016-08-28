using EuroPallets.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.Data.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private DbContext context;
        private IDbSet<T> set;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        public IDbSet<T> Set
        {
            get { return this.set; }
        }

        public System.Linq.IQueryable<T> All()
        {
            return this.set;
        }

        public T Find(object id)
        {
            return this.set.Find(id);
        }

        public void Add(T entity)
        {
            this.ChangeState(entity, EntityState.Added);
        }

        public void ChangeStates(T entity, EntityState state)
        {
            this.context.Entry(entity).State = state;
        }

        public void BulkInsert(IEnumerable<T> entity)
        {
            this.context.BulkInsert(entity);
        }

        public void BulkDelete(IEnumerable<T> entity)
        {
            this.context.BulkDelete(entity);
        }

        public void BulkSaveChanges()
        {
            this.context.BulkSaveChanges();
        }
        public void Update(T entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        public void Delete(T entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
        }

        public DbContext CurrentDbContext()
        {
            return this.context;
        }

        public T Delete(object id)
        {
            var entity = this.Find(id);
            this.Delete(entity);
            return entity;
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private void ChangeState(T entity, EntityState state)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            entry.State = state;
        }
    }
}
