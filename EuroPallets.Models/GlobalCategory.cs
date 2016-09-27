namespace EuroPallets.Models
{
    using EuroPallets.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GlobalCategory : BaseModel<int>
    {
        public GlobalCategory()
        {
            this.EuroPalletFurnitures = new HashSet<EuroPalletFurniture>();
            this.SubCategories = new HashSet<SubCategory>();
        }
        public string Name { get; set; }

        public virtual ICollection<EuroPalletFurniture> EuroPalletFurnitures { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
