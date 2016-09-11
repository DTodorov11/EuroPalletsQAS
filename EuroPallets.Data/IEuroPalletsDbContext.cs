using EuroPallets.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.Data
{
    public interface IEuroPalletsDbContext
    {
        IDbSet<Category> Categories { get; set; }
        IDbSet<EuroPalletFurniture> EuroPalletFurnituries { get; set; }
        IDbSet<EuroPalletImage> EuroPalletImages { get; set; }
        IDbSet<GlobalCategory> GlobalCategories { get; set; }
        IDbSet<ShopingCart> ShopingCart { get; set; }
        IDbSet<Specification> Specifications { get; set; }
        IDbSet<ShopingCartEuroPalllets> ShopingCartEuroPalllets { get; set; }
        IDbSet<AnonymousShopingCart> AnonymousShopingCarts { get; set; }
        int SaveChanges();
    }
}
