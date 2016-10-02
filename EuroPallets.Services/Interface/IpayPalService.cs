using EuroPallets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.Services.Interface
{
    public interface IPayPalService
    {
        void Save(PayPalPayment model);

        IQueryable<PayPalPayment> GetAllPayPalPayment();

        IQueryable<PayPalPayment> GetAllPayPalPaymentForUser(string id);
    }
}
