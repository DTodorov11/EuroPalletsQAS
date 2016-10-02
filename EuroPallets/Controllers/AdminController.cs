using EuroPallets.Common;
using EuroPallets.Models;
using EuroPallets.ViewModels.ProductsViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroPallets.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminPanel()
        {
            return View();
        }

        public ActionResult EmailEditor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmailEditor(string text, string type)
        {
            if (text != " " && type != " " && type != "Select Type")
            {
                try
                {
                    string templateName = type + ".txt";
                    using (System.IO.StreamWriter writer = new StreamWriter(GlobalConstants.EmailtemplateFolder + templateName, false))
                    {
                        writer.WriteLine(text);
                        writer.Close();
                    }
                    return this.View();
                }
                catch (Exception ex) { }
            }
            return this.RedirectToAction("Index", "Admin");
        }

        public ActionResult ShowEmailTemplates(string text)
        {
            string templateName = text + ".txt";

            if (System.IO.File.Exists(GlobalConstants.EmailtemplateFolder + templateName))
            {
                var fileContents = System.IO.File.ReadAllText(this.Server.MapPath("/Content/EmailEditor/" + templateName));
                return this.Content(fileContents);
            }

            return this.Json("<h1><span style=\"font-weight: bold; \">No Template</span></h1>");
        }

        public ActionResult UploadNewItem()
        {
            return this.View(new EuroPalletFurniture());
        }

        [HttpPost]
        public ActionResult UploadNewItem(ProductsViewModel model)
        {
            return this.View();
        }
    }
}