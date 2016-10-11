using EuroPallets.Data;
using EuroPallets.Services;
using EuroPallets.ViewModels.CashOnDelivery;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroPallets.Controllers
{
    public class CashOnDeliveryController : BaseController
    {
        private readonly UserServices userService;
        public CashOnDeliveryController(UserServices userService, IEvroPalletsData data)
            : base(data)
        {
            this.userService = userService;
        }
        // GET: CashOnDelivery
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult PaymentModalUpdate()
        {
            var currentUser = this.userService.GetById(this.User.Identity.GetUserId());
            var model = new CashOnDeliveryControllerViewModel();
            model.Id = this.User.Identity.GetUserId();
            model.Name = currentUser.UserName;
            model.PhoneNumber = currentUser.PhoneNumber;
            return this.PartialView("_PaymentModalPartial", model);
        }
    }
}