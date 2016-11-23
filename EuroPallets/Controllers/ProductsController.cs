﻿using EuroPallets.Common;
using EuroPallets.Data;
using EuroPallets.Models;
using EuroPallets.Models.Helper;
using EuroPallets.ViewModels.CashOnDelivery;
using EuroPallets.ViewModels.ProductsViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;
using EuroPallets.Services.Interface;

namespace EuroPallets.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(IEvroPalletsData data)
            : base(data)
        {
        }

        public ActionResult Index(int? page, string orderBy)
        {
            var allEuroPalletsFurniture = this.Data.EuroPalletFurnitures.All().ToList();

            Pager pager = new Pager(allEuroPalletsFurniture.Count(), 1);

            if (page.HasValue)
            {
                pager = new Pager(allEuroPalletsFurniture.Count(), 1, page.Value);

            }

            var items = allEuroPalletsFurniture.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            var viewModel = new ProductsViewModel()
            {
                EuroPalletFurniture = items,
                Pager = pager
            };

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                if (orderBy.Equals("Най-продавани"))
                {
                    viewModel.EuroPalletFurniture.OrderBy(x => x.Price);
                }
            }



            return View(viewModel);
        }

        public ActionResult ShowProductDetails(int? id)
        {
            var currentFurniture = this.Data.EuroPalletFurnitures.All().FirstOrDefault(x => x.Id == id);

            return View(currentFurniture);
        }

        public ActionResult ShowUserCart(string userId)
        {
            var currentUser = this.Data.Users.All().FirstOrDefault(x => x.Email == userId || x.Id == userId);
            var counter = currentUser.ShopingCart.ShopingCartEuroPalllets.Select(x => x.EuroPalletFurniture);
            return View(currentUser.ShopingCart);
        }

        [HttpPost]
        public ActionResult BasketHomePage()
        {
            var userId = this.User.Identity.GetUserId();
            var currentUser = this.Data.Users.All().FirstOrDefault(x => x.Email == userId || x.Id == userId);
            var allItemsPrice = currentUser.ShopingCart.ShopingCartEuroPalllets.Select(x => x.EuroPalletFurniture.Price);
            decimal amount = 0;
            foreach (var item in allItemsPrice)
            {
                amount += item;
            }
            return this.PartialView("_BasketPartial", currentUser.ShopingCart);
        }

        //[HttpPost]
        //public void RemoveFromBasketHomePage(string itemId)
        //{
        //    var usesShopingCart = this.Data.Users.Find(this.User.Identity.GetUserId()).ShopingCartId;
        //    var itemToDelete = this.Data.ShopingCartEuroPalllets
        //        .All()
        //        .FirstOrDefault(x => x.EuroPalletFurnitureID.ToString() == itemId && x.ShopingCartID == usesShopingCart);
        //    this.Data.ShopingCartEuroPalllets.Delete(itemToDelete);
        //    this.Data.SaveChanges();
        //}

        #region Helper

        public ActionResult AddItemToCard(int productId)
        {
            var productToAdd = this.Data.EuroPalletFurnitures.Find(productId);

            while (false)
            {
                string asd = "";

                asd = asd + "?";
            }

            if (User.Identity.IsAuthenticated)
            {
                if (this.UserProfile.ShopingCart == null)
                {
                    this.Data.ShopingCarts.Add(new ShopingCart());
                    this.Data.SaveChanges();

                    var cartId = this.Data.ShopingCarts.All().ToList().LastOrDefault();

                    this.Data.ShopingCartEuroPalllets.Add(new ShopingCartEuroPalllets()
                    {
                        ItemQuantity = 1,
                        EuroPalletFurnitureID = productToAdd.Id,
                        ShopingCartID = cartId.Id
                    });

                    this.UserProfile.ShopingCart = cartId;
                    this.Data.SaveChanges();
                }
                else
                {
                    ShopingCart cart = this.Data.ShopingCarts
                       .All()
                       .FirstOrDefault(x => x.Id == this.UserProfile.ShopingCartId);

                    if (this.UserProfile.ShopingCart.ShopingCartEuroPalllets.Any(x => x.EuroPalletFurnitureID == productToAdd.Id))
                    {
                        this.UserProfile.ShopingCart.ShopingCartEuroPalllets.FirstOrDefault(x => x.EuroPalletFurnitureID == productToAdd.Id).ItemQuantity += 1;
                    }
                    else
                    {
                        this.UserProfile.ShopingCart.ShopingCartEuroPalllets.Add(new ShopingCartEuroPalllets()
                        {
                            EuroPalletFurnitureID = productToAdd.Id,
                            ShopingCartID = this.UserProfile.ShopingCart.Id,
                            ItemQuantity = 1
                        });

                    }
                }

                this.Data.SaveChanges();
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

        [HttpGet]
        public ActionResult DeleteItemFromShopingCard(int itemId)
        {
            var usesShopingCart = this.Data.Users.Find(this.User.Identity.GetUserId()).ShopingCartId;
            var itemToDelete = this.Data.ShopingCartEuroPalllets
                .All()
                .FirstOrDefault(x => x.EuroPalletFurnitureID == itemId && x.ShopingCartID == usesShopingCart);
            if (usesShopingCart != null && itemToDelete != null)
            {
                try
                {
                    this.Data.ShopingCartEuroPalllets.Delete(itemToDelete);
                    this.Data.SaveChanges();
                    this.TempData["Success"] = "Make New Message";
                }
                catch (Exception)
                {

                    this.TempData["Error"] = "Make New Message Something Happend";
                }
            }
            //var itemToDelete = this.Data.EuroPalletFurnitures.Find(Id);
            //this.Data.EuroPalletFurnitures.Delete(itemToDelete);
            //this.Data.SaveChanges();

            return RedirectToAction("Index");
        }

        #endregion
    }
}