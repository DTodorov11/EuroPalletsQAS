using EuroPallets.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Infrastructure.Mapping;

namespace EuroPallets.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            EuroPalletsDbContext context = new EuroPalletsDbContext();
            //var viewModel = context.EuroPalletFurnituries.Where(x => !x.isDeleted).AsQueryable().To<>;


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}