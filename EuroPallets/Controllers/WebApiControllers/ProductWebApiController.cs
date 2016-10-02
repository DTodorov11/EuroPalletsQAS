﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using EuroPallets.Data;
using EuroPallets.Models;
using EuroPallets.Common;

namespace EuroPallets.Controllers.WebApiControllers
{
    public class ProductWebApiController : ApiController
    {
        public ProductWebApiController()
        {
            this.Data = new EvroPalletsData(new EuroPalletsDbContext());
        }

        private IEvroPalletsData Data;


        [HttpGet]
        public Dictionary<string,string> AddItemToCard(int productId, string userName)
        {
            var productToAdd = this.Data.EuroPalletFurnitures.Find(productId);
            User UserProfile = this.Data.Users.All().FirstOrDefault(x => x.UserName == userName);

            Dictionary<string, string> statusAndMessage = new Dictionary<string, string>();

            if (User.Identity.IsAuthenticated)
            {
                if (UserProfile.ShopingCart == null)
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

                    UserProfile.ShopingCart = cartId;
                    this.Data.SaveChanges();

                    statusAndMessage.Add("Success", GlobalConstants.SuccessfullAddProductToCard);
                }
                else
                {
                    if (UserProfile.ShopingCart.ShopingCartEuroPalllets.Any(x => x.EuroPalletFurnitureID == productToAdd.Id))
                    {
                        UserProfile.ShopingCart.ShopingCartEuroPalllets.FirstOrDefault(x => x.EuroPalletFurnitureID == productToAdd.Id).ItemQuantity += 1;
                    }
                    else
                    {
                        UserProfile.ShopingCart.ShopingCartEuroPalllets.Add(new ShopingCartEuroPalllets()
                        {
                            EuroPalletFurnitureID = productToAdd.Id,
                            ShopingCartID = UserProfile.ShopingCart.Id,
                            ItemQuantity = 1
                        });

                    }
                }

                this.Data.SaveChanges();

                statusAndMessage.Add("Success", GlobalConstants.SuccessfullAddProductToCard);
            }

            else
            {
                statusAndMessage.Add("Error", GlobalConstants.RegistrationIsNeeded);

                //TODO
                //ADD TO ANONYMOUSE SHOPING CART
            }

            while (true)
            {
                string asd = "";

                asd = asd + "?";
            }

            return statusAndMessage;
        }
    }
}