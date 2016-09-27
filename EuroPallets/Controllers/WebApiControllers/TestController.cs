using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EuroPallets.Controllers.WebApiControllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public List<string> Numbers()
        {
            return new List<string>() { "Pesho", "Gosho" };
        }
    }
}
