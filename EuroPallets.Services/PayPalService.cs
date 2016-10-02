
namespace EuroPallets.Services
{
    using EuroPallets.Data.Repositories;
    using EuroPallets.Models;
    using EuroPallets.Services.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PayPalService : IPayPalService
    {
        private readonly IRepository<PayPalPayment> paypalRepo;

        public PayPalService(IRepository<PayPalPayment> paypalRepo)
        {
            this.paypalRepo = paypalRepo;
        }

        public IQueryable<PayPalPayment> GetAllPayPalPayment()
        {
            return this.paypalRepo.All().Where(x => !x.isDeleted);

        }

        public IQueryable<PayPalPayment> GetAllPayPalPaymentForUser(string id)
        {
            var membership = this.paypalRepo.All().Where(x => x.UserPaymentId == id);
            return membership;
        }

        public void Save(PayPalPayment model)
        {
            try
            {
                this.paypalRepo.Add(model);
                this.paypalRepo.SaveChanges();
                Logger("Yes Save");
            }
            catch (System.Exception)
            {

                Logger("NO Save");
            }
        }

        public static void Logger(string mes)
        {
#if (!DEBUG)
            System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Content\\PayPalIPN.txt", true);
#else
            System.IO.StreamWriter file = new System.IO.StreamWriter("G:\\PayPalServiceLogger.txt", true);
#endif
            file.WriteLine("===");
            file.WriteLine(mes);
            file.WriteLine("---");
            file.WriteLine(System.DateTime.UtcNow.ToString());
            file.WriteLine("===");
            file.Close();
        }
    }
}

