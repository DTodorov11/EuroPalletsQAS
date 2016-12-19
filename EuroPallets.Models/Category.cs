

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
            this.SubCategory = new HashSet<SubCategory>();
            this.Filter = new HashSet<Filters>();

        }
        public string Name { get; set; }

        public ICollection<SubCategory> SubCategory { get; set; }
        public ICollection<Filters> Filter { get; set; }
    }
}
