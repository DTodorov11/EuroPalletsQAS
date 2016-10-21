using EuroPallets.Data.Repositories;
using EuroPallets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.Data
{
    public interface IEvroPalletsData
    {
        IRepository<User> Users { get; }
        IRepository<EuroPalletFurniture> EuroPalletFurnitures { get; }
        IRepository<ShopingCart> ShopingCarts { get; }
        IRepository<ShopingCartEuroPalllets> ShopingCartEuroPalllets { get; }
        IRepository<GlobalCategory> GlobalCategories { get; }
        IRepository<Category> Category { get; }

        IRepository<PayPalPayment> PayPalPayments { get; }
        IRepository<Specification> Specification { get; }
        IRepository<EuroPalletImage> EuroPalletImages { get; }
        
        int SaveChanges();   
    }
}
