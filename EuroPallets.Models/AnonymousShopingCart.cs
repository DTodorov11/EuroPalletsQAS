using EuroPallets.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.Models
{
    public class AnonymousShopingCart : BaseModel<int>
    {
        public AnonymousShopingCart()
        {
            this.EuroPalletFurnitures = new HashSet<EuroPalletFurniture>();
        }

        public ICollection<EuroPalletFurniture> EuroPalletFurnitures { get; set; }
    }
}
