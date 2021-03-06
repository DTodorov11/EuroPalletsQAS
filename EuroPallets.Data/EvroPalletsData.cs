﻿using EuroPallets.Data.Repositories;
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

        public IRepository<Category> Category
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        public IRepository<EuroPalletImage> EuroPalletImages
        {
            get
            {
                return this.GetRepository<EuroPalletImage>();
            }
        }

        public IRepository<Specification> Specification
        {
            get
            {
                return this.GetRepository<Specification>();
            }
        }

        public IRepository<Filters> Filter
        {
            get
            {
                return this.GetRepository<Filters>();
            }
        }

        public IRepository<ShopingCartEuroPalllets> ShopingCartEuroPalllets
        {
            get
            {
                return this.GetRepository<ShopingCartEuroPalllets>();
            }
        }

        public IRepository<GlobalCategory> GlobalCategories
        {
            get
            {
                return this.GetRepository<GlobalCategory>();
            }
        }

        public IRepository<EuroPalletFurniture> EuroPalletFurnitures
        {
            get
            {
                return this.GetRepository<EuroPalletFurniture>();
            }
        }

        public IRepository<ShopingCart> ShopingCarts
        {
            get
            {
                return this.GetRepository<ShopingCart>();
            }
        }

        public IRepository<PayPalPayment> PayPalPayments
        {
            get
            {
                return this.GetRepository<PayPalPayment>();
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
