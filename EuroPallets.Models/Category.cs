

namespace EuroPallets.Models
{
    using EuroPallets.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Category : BaseModel<int>
    {
        public Category()
        {
            this.EuroPalletFurnitures = new HashSet<EuroPalletFurniture>();
        }
        public string Name { get; set; }

        public ICollection<EuroPalletFurniture> EuroPalletFurnitures { get; set; }
    }
}
