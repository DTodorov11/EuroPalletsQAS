using EuroPallets.Data;
using EuroPallets.Models;
using EuroPallets.Services;
using EuroPallets.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

namespace EuroPallets.Controllers
{
    public class TestController : Controller
    {
        private IUserServices userServices;

        public TestController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        //// GET: Test
        //[HttpGet]
        //public ActionResult Index(int? page)
        //{
        //    var dummyItems = Enumerable.Range(1, 150).Select(x => "Item " + x);
        //    var pager = new Pager(dummyItems.Count(), page);

        //    var viewModel = new IndexViewModels
        //    {
        //        Items = dummyItems.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
        //        Pager = pager
        //    };

        //    return View(viewModel);
        //}

        public ActionResult Test()
        {
            return View();
        }
    }
}