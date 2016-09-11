using EuroPallets.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.Models
{
    public class ShopingCart : BaseModel<int>
    {
        public ShopingCart()
        {
            this.ShopingCartEuroPalllets = new HashSet<ShopingCartEuroPalllets>();
        }

        public virtual ICollection<ShopingCartEuroPalllets> ShopingCartEuroPalllets { get; set; }

        //public virtual ICollection<EuroPalletFurniture> EuroPalletFurnitures { get; set; }

        //public string UserID { get; set; }
        //public virtual User User { get; set; }
    }
}
