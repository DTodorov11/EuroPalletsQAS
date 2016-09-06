using EuroPallets.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroPallets.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Show(int id)
        {
            EuroPalletsDbContext context = new EuroPalletsDbContext();

            var euroPallet = context.EuroPalletFurnituries.FirstOrDefault(x => x.Id == id);

            var euroPalletsImg = euroPallet.EuroPalletImages.FirstOrDefault().Image;

            return File(euroPalletsImg, "image/jpg");
        }
        public ActionResult Details(int imgId, int id)
        {
            EuroPalletsDbContext context = new EuroPalletsDbContext();

            var euroPallet = context.EuroPalletFurnituries.FirstOrDefault(x => x.Id == id);

            var euroPalletsImg = euroPallet.EuroPalletImages.FirstOrDefault(x => x.Id == imgId).Image;

            return File(euroPalletsImg, "image/jpg");
        }
    }
}