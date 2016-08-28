
namespace EuroPallets.Models
{
    using EuroPallets.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Specification : BaseModel<int>
    {
        public int EuroPalletFurnitureId { get; set; }

        public EuroPalletFurniture EuroPalletFurniture { get; set; }

        public string Type { get; set; }
        public string Material { get; set; }
        public string Color { get; set; }
        public string Structure { get; set; }
    }
}
