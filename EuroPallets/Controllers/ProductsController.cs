using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroPallets.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowProductDetails(int? id)
        {

            return View();
        }

        public ActionResult ShowUserCart(string userId)
        {

            return View();
        }
    }
}