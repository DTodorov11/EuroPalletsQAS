using EuroPallets.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.Models
{
    public class SubCategory : BaseModel<int>
    {
        public string Name { get; set; }
    }
}
