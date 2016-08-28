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

        // GET: Test
        public ActionResult Index()
        {
            EuroPalletsDbContext context = new EuroPalletsDbContext();
            var test = context.Users.ToList();

            var currentUsers = this.userServices.TakeUserByUserName("");

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}