namespace EuroPallets.Models
{
    using EuroPallets.Models.Base;
    using Helper;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EuroPalletFurniture : BaseModel<int>
    {
        public EuroPalletFurniture()
        {
            this.EuroPalletImages = new HashSet<EuroPalletImage>();
            //this.GlobalCategorys = new HashSet<GlobalCategory>();
            this.ShopingCarts = new HashSet<ShopingCart>();
        }

        public string Name { get; set; }

        public string Name_Description { get; set; }

        //public byte[] ShemePicture { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Rating { get; set; }

        public virtual ICollection<EuroPalletImage> EuroPalletImages { get; set; }
        //public virtual ICollection<GlobalCategory> GlobalCategorys { get; set; }
        public virtual ICollection<ShopingCart> ShopingCarts { get; set; }

        public int? SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public int? SpecificationId { get; set; }

        public virtual Specification Specification { get; set; }

        public decimal DeliveryPrice { get; set; }

        public int Quantity { get; set; }

        public bool IsAvailable { get; set; }

        [NotMapped]
        public Pager Pager { get; set; }

        public string Meta_Tag_Title { get; set; }
        public string Meta_Tat_Description { get; set; }
        public string Meta_Tag_Keywords { get; set; }
    }
}
