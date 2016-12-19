using EuroPallets.Models;
using EuroPallets.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroPallets.ViewModels.ProductsViewModel
{
    public class ProductsViewModel
    {
        public IEnumerable<EuroPalletFurniture> EuroPalletFurniture { get; set; }

        public Pager Pager { get; set; }

        public IEnumerable<Filter> Filters { get; set; }
    }
}