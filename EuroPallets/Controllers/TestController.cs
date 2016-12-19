using EuroPallets.Data;
using EuroPallets.Models;
using EuroPallets.Services;
using EuroPallets.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

namespace EuroPallets.Controllers
{
    public class TestController : BaseController
    {
        
        private IUserServices userServices;

        public TestController(IEvroPalletsData data) : base(data)
        {
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
            var allTable = this.Data.Category.All()
                .Include(x => x.SubCategory)
                .Include(x => x.SubCategory.Select(y => y.EuroPalletFurniture))
                .FirstOrDefault(x => x.Name == "Маси").SubCategory.FirstOrDefault().EuroPalletFurniture.FirstOrDefault();


            return View();
        }
    }
}