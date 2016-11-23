using EuroPallets.Data;
using EuroPallets.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading;
using System.Globalization;

namespace EuroPallets.Controllers
{
    public abstract class BaseController : Controller
    {
        // GET: Base
        public BaseController(IEvroPalletsData data)
        {
            this.Data = data;
        }

        protected BaseController(IEvroPalletsData data, User userProfile)
          : this(data)
        {
            this.UserProfile = userProfile;
        }

        protected IEvroPalletsData Data { get; set; }

        protected User UserProfile { get; private set; }

        //Set Culture and translations
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var language = this.Request.RequestContext.RouteData.Values["language"].ToString();

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            base.OnActionExecuting(filterContext);
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.GetUserName();
                this.UserProfile = this.Data.Users
                    .All()
                    .FirstOrDefault(x => x.UserName == username);
            }

            var result = base.BeginExecute(requestContext, callback, state);

            //if (!requestContext.HttpContext.Request.IsAjaxRequest())
            //{
            //    // This should be after base.BeginExecute so we can access TempData
            //    var systemMessages = this.PrepareSystemMessages();
            //    this.ViewBag.SystemMessages = systemMessages;
            //}

            return result;
        }

        //private object PrepareSystemMessages()
        //{
        //    var messages = new List<SystemMessage>();
        //    if (this.TempData.ContainsKey(SystemMessageType.Information.ToString()))
        //    {
        //        messages.Add(new SystemMessage
        //        {
        //            Content = this.TempData[SystemMessageType.Information.ToString()].ToString(),
        //            Type = SystemMessageType.Information
        //        });
        //    }

        //    return messages;
        //}
    }
}