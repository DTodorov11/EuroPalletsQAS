using EuroPallets.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.Models
{
    public class EuroPalletImage : BaseModel<int>
    {

        public EuroPalletImage()
        {

        }
        public int? EuroPalletFurnitureID { get; set; }

        public virtual EuroPalletFurniture EuroPalletFurniture { get; set; }

        public byte[] Image { get; set; }
      
    }
}
