

namespace EuroPallets.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data;
    using Models;
    using Microsoft.AspNet.Identity;
    using System.Text;
    using System.Net;
    using System.IO;
    using ViewModels.PayPal;
    using Services.Interface;
    using System.Web.Script.Serialization;
    using Newtonsoft.Json;

    public class PayPalController : BaseController
    {
        private readonly IPayPalService payPalService;
        private readonly IUserServices userService;
        protected PayPalController(IUserServices userService, IPayPalService payPalService, IEvroPalletsData data) : base(data)
        {
            this.payPalService = payPalService;
            this.userService = userService;
        }

        // GET: PayPal
        public void Index(PayPalActionViewModel model)
        {
            string business = "stoyan.kafaliev@mailinator.com";
            string item_name = model.Name;
            string amount = model.Price;
            var item_id = model.Id;
            var shipping = model.shipping;

            var sb = new StringBuilder();
            sb.Append("cmd=_xclick");
            sb.Append("&business=" + HttpUtility.UrlEncode(business));
            sb.Append("&lc=BG");
            sb.Append("&item_name=" + item_name);
            sb.Append("&item_number=" + item_id);
            sb.Append("&a3=" + amount);
            sb.Append("&currency_code=" + model.Currency);
            sb.Append("&button_subtype=services");
            sb.Append("&no_note=0");
            sb.Append("&shipping=" + shipping);
            sb.Append($"&custom={this.User.Identity.GetUserId()}");
            sb.Append("&bn=PP-BuyNowBF:btn_buynow_LG.gif:NonHostedGuest");

            this.Response.Redirect(@"https://www.sandbox.paypal.com/webscr?" + sb.ToString());
        }

        [HttpPost]
        public ActionResult Info()
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.Expect100Continue = true;

            string strSandbox = "https://ipnpb.sandbox.paypal.com/cgi-bin/webscr";
            string strLive = "https://ipnpb.paypal.com/cgi-bin/webscr";

            var req = (HttpWebRequest)WebRequest.Create(strSandbox);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] param = this.Request.BinaryRead(System.Web.HttpContext.Current.Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);
            strRequest = strRequest + "&cmd=_notify-validate";
            req.ContentLength = strRequest.Length;
            Logger(string.Format("strRequest = [{0}]", strRequest));

            using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), Encoding.ASCII))
            {
                streamOut.Write(strRequest);
                streamOut.Close();
                using (StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    string strResponse = streamIn.ReadToEnd();
                    streamIn.Close();
                    Logger(string.Format("Response was [{0}]", strResponse));

                    if (strResponse == "VERIFIED")
                    {
                        var userId = System.Web.HttpContext.Current.Request.Form["custom"];
                        var model = new PayPalPayment
                        {
                            mc_gross = System.Web.HttpContext.Current.Request.Form["mc_gross"] != null ? System.Web.HttpContext.Current.Request.Form["mc_gross"] : " ",
                            payer_id = System.Web.HttpContext.Current.Request.Form["payer_id"] != null ? System.Web.HttpContext.Current.Request.Form["payer_id"] : " ",
                            payment_date = System.Web.HttpContext.Current.Request.Form["payment_date"] != null ? System.Web.HttpContext.Current.Request.Form["payment_date"] : " ",
                            payment_status = System.Web.HttpContext.Current.Request.Form["payment_status"] != null ? System.Web.HttpContext.Current.Request.Form["payment_status"] : " ",
                            mc_fee = System.Web.HttpContext.Current.Request.Form["mc_fee"] != null ? System.Web.HttpContext.Current.Request.Form["mc_fee"] : " ",
                            transaction_id = System.Web.HttpContext.Current.Request.Form["txn_id"] != null ? System.Web.HttpContext.Current.Request.Form["txn_id"] : " ",
                            business = System.Web.HttpContext.Current.Request.Form["business"] != null ? System.Web.HttpContext.Current.Request.Form["business"] : " ",
                            verify_sign = System.Web.HttpContext.Current.Request.Form["verify_sign"] != null ? System.Web.HttpContext.Current.Request.Form["verify_sign"] : " ",
                            payment_type = System.Web.HttpContext.Current.Request.Form["payment_type"] != null ? System.Web.HttpContext.Current.Request.Form["payment_type"] : " ",
                            payment_fee = System.Web.HttpContext.Current.Request.Form["payment_fee"] != null ? System.Web.HttpContext.Current.Request.Form["payment_fee"] : " ",
                            receiver_id = System.Web.HttpContext.Current.Request.Form["receiver_id"] != null ? System.Web.HttpContext.Current.Request.Form["receiver_id"] : " ",
                            txn_type = System.Web.HttpContext.Current.Request.Form["txn_type"] != null ? System.Web.HttpContext.Current.Request.Form["txn_type"] : " ",
                            ipn_track_id = System.Web.HttpContext.Current.Request.Form["ipn_track_id"] != null ? System.Web.HttpContext.Current.Request.Form["ipn_track_id"] : " ",
                            payment_gross = System.Web.HttpContext.Current.Request.Form["payment_gross"] != null ? System.Web.HttpContext.Current.Request.Form["payment_gross"] : " ",
                            receiver_email = System.Web.HttpContext.Current.Request.Form["receiver_email"] != null ? System.Web.HttpContext.Current.Request.Form["receiver_email"] : " ",
                            payer_email = System.Web.HttpContext.Current.Request.Form["payer_email"] != null ? System.Web.HttpContext.Current.Request.Form["payer_email"] : " ",
                            item_name = System.Web.HttpContext.Current.Request.Form["item_name"] != null ? System.Web.HttpContext.Current.Request.Form["item_name"] : " ",
                            mc_currency = System.Web.HttpContext.Current.Request.Form["mc_currency"] != null ? System.Web.HttpContext.Current.Request.Form["mc_currency"] : " ",
                            subscr_id = System.Web.HttpContext.Current.Request.Form["subscr_id"] != null ? System.Web.HttpContext.Current.Request.Form["subscr_id"] : " ",
                            amount3 = System.Web.HttpContext.Current.Request.Form["amount3"] != null ? System.Web.HttpContext.Current.Request.Form["amount3"] : " ",
                            recurring = System.Web.HttpContext.Current.Request.Form["recurring"] != null ? System.Web.HttpContext.Current.Request.Form["recurring"] : " ",
                            reattempt = System.Web.HttpContext.Current.Request.Form["reattempt"] != null ? System.Web.HttpContext.Current.Request.Form["reattempt"] : " ",
                            subscr_date = System.Web.HttpContext.Current.Request.Form["subscr_date"] != null ? System.Web.HttpContext.Current.Request.Form["subscr_date"] : " ",
                            period3 = System.Web.HttpContext.Current.Request.Form["period3"] != null ? System.Web.HttpContext.Current.Request.Form["period3"] : " ",
                            mc_amount3 = System.Web.HttpContext.Current.Request.Form["mc_amount3"] != null ? System.Web.HttpContext.Current.Request.Form["mc_amount3"] : " ",
                            UserPaymentId = userId
                        };
                        bool isAmountTrue = true;
                        bool isCurrencyTrue = true;
                        var nextPaymentDay = DateTime.UtcNow;

                        var isAllReadyUsed = this.payPalService.GetAllPayPalPayment().FirstOrDefault(x => x.transaction_id == model.transaction_id) != null ? false : true;
                        try
                        {
                            var currentUser = this.userService.GetById(userId);
                            if (model.payment_status == "Completed" && isAllReadyUsed && isAmountTrue && isCurrencyTrue)
                            {
                                //currentUser
                            }
                            else if (model.txn_type == "subscr_cancel" &&
                                this.payPalService.GetAllPayPalPayment()
                                .FirstOrDefault(x => x.subscr_id == model.subscr_id) != null)
                            {
                                var payment = this.payPalService.GetAllPayPalPayment().FirstOrDefault(x => x.subscr_id == model.subscr_id);
                                payment.SubscriptionCancleDate = DateTime.UtcNow;
                                payment.ReceivedResponse = JsonConvert.SerializeObject(model);

                            }

                            this.payPalService.Save(model);
                            //this.users.Update(currentUser);
                        }
                        catch (Exception exception)
                        {
                            Logger("Something happened while saving the paypal transaction into database");
                            Logger(string.Format("The exception Message was [{0}]", exception.ToString()));
                            Logger(string.Format($"Transaction Number and Payer_ID  was [{model.transaction_id}] and [{model.payer_id}]"));
                        }
                    }
                    else if (strResponse == "INVALID")
                    {
                        Logger("INVALID");
                    }
                    else
                    {
                        Logger("Something unexpected happened");
                    }

                    return this.RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpPost]
        public ActionResult CheckExistingCoupom(string coupon)
        {
            bool ifCouponExist = false;
            try
            {
                //ifCouponExist = this.admin.GetCoupon(coupon) == null ? true : false;
                return Json(!ifCouponExist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public static void Logger(string mes)
        {
#if (!DEBUG)
            System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Content\\PayPalIPN.txt", true);
#else
            System.IO.StreamWriter file = new System.IO.StreamWriter("G:\\PayPalIPNTest.txt", true);
#endif
            file.WriteLine($"Start Date = {DateTime.UtcNow.ToString()}!");
            file.WriteLine(mes);
            file.WriteLine($"End Date = {DateTime.UtcNow.ToString()}!");
            file.WriteLine("==========================================");
            file.Close();
        }
    }
}