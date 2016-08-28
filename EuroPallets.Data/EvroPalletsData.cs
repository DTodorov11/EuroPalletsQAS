using EuroPallets.Data.Repositories;
using EuroPallets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.Data
{
    public class EvroPalletsData : IEvroPalletsData
    {
        private IEuroPalletsDbContext context;
        private IDictionary<Type, object> repositories;

        public EvroPalletsData(IEuroPalletsDbContext context)
        {
            this.context = context;
            repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!this.repositories.ContainsKey(type))
            {
                var typeOfRepository = typeof(GenericRepository<T>);
                //                if (type.IsAssignableFrom(typeof(Game)))
                //                {
                //                    typeOfRepository = typeof(GamesRepository);
                //                }

                var repository = Activator.CreateInstance(typeOfRepository, this.context);
                this.repositories.Add(type, repository);
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}
