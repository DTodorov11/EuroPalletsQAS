
namespace EuroPallets.ViewModels.PayPal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class PayPalActionViewModel
    {
        public int Id { get; set; }

        public string shipping { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Currency { get; set; }

        public int Quantity { get; set; }

        public string Sku { get; set; }

        //[System.Web.Mvc.Remote("CheckExistingCoupom", "PayPal", HttpMethod = "POST", ErrorMessage = "  Incorrect coupon code")]
        public string Coupon { get; set; }

        public string UserId { get; set; }
    }
}