namespace EuroPallets.Models
{
    using EuroPallets.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EuroPalletFurniture : BaseModel<int>
    {
        public EuroPalletFurniture()
        {
            this.EuroPalletImages = new HashSet<EuroPalletImages>();
            this.GlobalCategorys = new HashSet<GlobalCategory>();
            this.ShopingCarts = new HashSet<ShopingCart>();
        }

        public string Name { get; set; }

        //public byte[] ShemePicture { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Rating { get; set; }

        public ICollection<EuroPalletImages> EuroPalletImages { get; set; }
        public ICollection<GlobalCategory> GlobalCategorys { get; set; }
        public ICollection<ShopingCart> ShopingCarts { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int SpecificationId { get; set; }

        public Specification Specification { get; set; }

        public decimal DeliveryPrice { get; set; }

        public int Quantity { get; set; }

        public bool IsAvailable { get; set; }
    }
}
