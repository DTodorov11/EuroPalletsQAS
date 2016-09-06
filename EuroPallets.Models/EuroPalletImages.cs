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
        private ICollection<EuroPalletFurnitureEuroPalletImages> euroPalletFurnitureEuroPalletImages;

        public EuroPalletImage()
        {
            this.euroPalletFurnitureEuroPalletImages = new HashSet<EuroPalletFurnitureEuroPalletImages>();

        }
        public int? EuroPalletFurnitureID { get; set; }

        public virtual EuroPalletFurniture EuroPalletFurniture { get; set; }

        public byte[] Image { get; set; }
        public ICollection<EuroPalletFurnitureEuroPalletImages> EuroPalletFurnitureEuroPalletImages
        {
            get { return this.euroPalletFurnitureEuroPalletImages; }
            set { this.euroPalletFurnitureEuroPalletImages = value; }
        }
    }
}
