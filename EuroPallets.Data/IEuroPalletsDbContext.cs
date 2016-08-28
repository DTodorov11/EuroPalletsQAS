using EuroPallets.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.Data
{
    public interface IEuroPalletsDbContext
    {
        int SaveChanges();
    }
}
