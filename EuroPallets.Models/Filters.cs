using EuroPallets.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.Models
{
    public class Filters : BaseModel<int>
    {
        public Filters()
        {
            this.FilterChaildName = new HashSet<FilterChaildName>();
        }
        public string Name { get; set; }

        public virtual ICollection<FilterChaildName> FilterChaildName { get; set; }
    }
}
