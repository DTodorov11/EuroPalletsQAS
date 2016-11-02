using EuroPallets.Data;
using EuroPallets.Models;
using EuroPallets.ViewModels.MainViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Infrastructure.Mapping;

namespace EuroPallets.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IEvroPalletsData data)
            :base(data)
        {

        }

        public ActionResult Mebellete_Сайт_за_мебели_очаквайте_скоро()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View(this.Data.EuroPalletFurnitures.All());
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


        public static byte[] GetPhoto(string filePath)
        {
            FileStream stream = new FileStream(
                filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);

            reader.Close();
            stream.Close();

            return photo;
        }
    }
}