

namespace EuroPallets.Models.Base
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BaseModel<TKey> : IBaseModel, IAuditInfo
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }
 

        public DateTime? DeletedOn { get; set; }
     

        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool isDeleted { get; set; }
    }
}
