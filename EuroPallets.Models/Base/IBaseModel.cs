

namespace EuroPallets.Models.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBaseModel
    {
       bool isDeleted { get; set; }
        DateTime? DeletedOn { get; set; }
    }
}
