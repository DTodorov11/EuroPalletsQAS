using EuroPallets.Data;
using EuroPallets.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Routing;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;

namespace EuroPallets.Controllers
{
    public class BaseWebApiController : ApiController
    {
        public BaseWebApiController(IEvroPalletsData data)
        {
            this.Data = data;
        }

        protected IEvroPalletsData Data { get; set; }

    }
}
