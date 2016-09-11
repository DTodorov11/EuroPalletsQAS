using EuroPallets.Common;
using EuroPallets.Data;
using EuroPallets.Models;
using EuroPallets.Models.Helper;
using EuroPallets.ViewModels.ProductsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

namespace EuroPallets.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(IEvroPalletsData data)
            : base(data)
        {

        }
        // GET: Products
        public ActionResult Index(int? page)
        {
            var allEuroPalletsFurniture = this.Data.EuroPalletFurnitures.All().ToList();

            var pager = new Pager(allEuroPalletsFurniture.Count(), page, 5);
            var items = allEuroPalletsFurniture.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            var viewModel = new ProductsViewModel()
            {
                EuroPalletFurniture = items,
                Pager = pager
            };

            return View(viewModel);
        }
        public ActionResult ShowProductDetails(int? id)
        {
            var currentFurniture = this.Data.EuroPalletFurnitures.All().FirstOrDefault(x => x.Id == id);

            return View(currentFurniture);
        }

        public ActionResult ShowUserCart(string userId)
        {
            //ShopingCart userCard = this.Data.Users.Find(userId).ShopingCartEuroPalllets.

            //return View(userCard);

            return View();
        }

        #region Helper

        public ActionResult AddItemToCard(int productId)
        {
            var productToAdd = this.Data.EuroPalletFurnitures.Find(productId);

            if (User.Identity.IsAuthenticated)
            {
                //TODO
                //CHECK IF ITEM IS ALLREADY EXIST
                if (this.UserProfile.ShopingCartEuroPalllets == null)
                {
                    this.Data.ShopingCarts.Add(new ShopingCart());
                    this.Data.SaveChanges();

                    var asd = this.Data.ShopingCarts.All().ToList().LastOrDefault().Id;

                    this.Data.ShopingCartEuroPalllets.Add(new ShopingCartEuroPalllets()
                    {
                        ItemQuantity = 1,
                        EuroPalletFurnitureID = productToAdd.Id,
                        ShopingCartID = asd
                    });

                    this.Data.SaveChanges();
                }
                else
                {
                    //if (this.UserProfile.ShopingCart.EuroPalletFurnitures.Any(x => x.Id == productToAdd.Id))
                    //{

                    //}
                    //else
                    //{
                    //    this.UserProfile.ShopingCart.EuroPalletFurnitures.Add(productToAdd);
                    //}

                    this.Data.SaveChanges();
                    var currentuser = this.UserProfile;
                }

            }
            else
            {
                TempData["Error"] = GlobalConstants.RegistrationIsNeeded;

                //TODO
                //ADD TO ANONYMOUSE SHOPING CART
            }


            TempData["Success"] = GlobalConstants.SuccessfullAddProductToCard;

            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult DeleteItemFromShopingCard(int itemId)
        {
            var itemToDelete = this.Data.EuroPalletFurnitures.Find(itemId);
            this.Data.EuroPalletFurnitures.Delete(itemToDelete);
            this.Data.SaveChanges();

            return RedirectToAction("Index");
        }

        #endregion
    }
}