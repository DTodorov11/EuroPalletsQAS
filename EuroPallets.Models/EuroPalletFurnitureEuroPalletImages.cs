using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.Models
{
    public class EuroPalletFurnitureEuroPalletImages
    {
        [Key, Column(Order = 0)]
        public int EuroPalletFurnitureID { get; set; }

        public virtual EuroPalletFurniture EuroPalletFurniture { get; set; }

        public virtual ICollection<EuroPalletImage> EuroPalletImage { get; set; }
    }
}
