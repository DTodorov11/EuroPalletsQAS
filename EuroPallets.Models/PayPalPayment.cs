
namespace EuroPallets.Models
{
    using EuroPallets.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class PayPalPayment : BaseModel<int>
    {
        public string mc_gross { get; set; }

        public string payer_id { get; set; }

        public string payment_date { get; set; }

        public string payment_status { get; set; }

        public string mc_fee { get; set; }

        public string transaction_id { get; set; }

        public string business { get; set; }

        public string verify_sign { get; set; }

        public string payment_type { get; set; }

        public string payment_fee { get; set; }

        public string receiver_id { get; set; }

        public string txn_type { get; set; }

        public string ipn_track_id { get; set; }

        public string payment_gross { get; set; }

        public string payer_email { get; set; }

        public string receiver_email { get; set; }

        public string item_name { get; set; }

        public string mc_currency { get; set; }

        public string subscr_id { get; set; }

        public string amount3 { get; set; }

        public string recurring { get; set; }

        public string reattempt { get; set; }

        public string subscr_date { get; set; }

        public string period3 { get; set; }

        public string mc_amount3 { get; set; }

        public string UserPaymentId { get; set; }

        public DateTime SubscriptionCancleDate { get; set; }

        public string ReceivedResponse { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}