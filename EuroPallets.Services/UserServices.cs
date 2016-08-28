using EuroPallets.Data;
using EuroPallets.Models;
using EuroPallets.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.Services
{
    public class UserServices : IUserServices
    {
        private IEvroPalletsData data;
        public UserServices(IEvroPalletsData data)
        {
            this.data = data;
        }

        public User TakeUserByUserName(string userName)
        {
            return this.data.Users.All().FirstOrDefault(x => x.UserName == userName);
        }
    }
}
