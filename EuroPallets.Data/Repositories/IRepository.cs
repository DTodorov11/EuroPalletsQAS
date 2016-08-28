using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.Data.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> All();

        T Find(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
        void BulkInsert(IEnumerable<T> entity);
        void BulkDelete(IEnumerable<T> entity);
        void ChangeStates(T entity, EntityState state);

        T Delete(object id);
        DbContext CurrentDbContext();
        void BulkSaveChanges();
        void SaveChanges();
    }
}
