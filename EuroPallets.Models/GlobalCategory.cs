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
            this.Category = new HashSet<Category>();
        }
        public string Name { get; set; }

        public virtual ICollection<Category> Category { get; set; }
    }
}
