using EuroPallets.Data.Repositories;
using EuroPallets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.Data
{
    public interface IEvroPalletsData
    {
        IRepository<User> Users { get; }
        int SaveChanges();   
    }
}
