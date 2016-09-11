using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.Models
{
    public class ShopingCartEuroPalllets
    {
        [Key, Column(Order = 0)]
        public int ShopingCartID { get; set; }
        public virtual ShopingCart ShopingCart { get; set; }

        [Key, Column(Order = 1)]
        public int EuroPalletFurnitureID { get; set; }

        public virtual EuroPalletFurniture EuroPalletFurniture { get; set; }

        public int  ItemQuantity { get; set; }
    }
}
