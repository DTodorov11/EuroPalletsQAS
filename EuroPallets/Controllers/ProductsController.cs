using EuroPallets.Common;
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
using System.Data.Entity;

namespace EuroPallets.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(IEvroPalletsData data)
            : base(data)
        {
        }

        //IsAll filter is equals that take whole Category
        public ActionResult Index(int? page, string Category,bool isAll = false)
        {
            page = page == null ? 1 : page;

            List<EuroPalletFurniture> allEuroPalletsFurniture = new List<EuroPalletFurniture>();
            ICollection<Filters> filters = new List<Filters>();

            //Take Category
            if (isAll)
            {
                var allSubCategory = this.Data.Category.All()
                    .Include(x => x.SubCategory)
                    .Include(x => x.SubCategory.Select(y => y.EuroPalletFurniture))
                    .FirstOrDefault(x => x.Name == Category);

                filters = allSubCategory.Filter;

                foreach (var currentCtg in allSubCategory.SubCategory.ToList())
                {
                    allEuroPalletsFurniture.AddRange(currentCtg.EuroPalletFurniture);
                }
            }
            //Take SubCategory
            else
            {
                allEuroPalletsFurniture = this.Data.EuroPalletFurnitures.All().Where(x => x.SubCategory.Name == Category).ToList();

                var allCategory = this.Data.Category.All()
                                   .Include(x => x.SubCategory)
                                   .Include(x => x.SubCategory.Select(y => y.EuroPalletFurniture));

                foreach (var currentCtg in allCategory)
                {
                    if (currentCtg.SubCategory.Any(x => x.Name == Category))
                    {
                        var currentSubCat = currentCtg.SubCategory;
                    }
                }

            }

            Pager pager = new Pager(allEuroPalletsFurniture.Count(), 1);

            IEnumerable<EuroPalletFurniture> items = new List<EuroPalletFurniture>();

            if (page == 1)
            {
                items = allEuroPalletsFurniture.Take(pager.PageSize);
            }
            else
            {
                items = allEuroPalletsFurniture.OrderBy(x => x.CreatedOn).Skip(pager.CurrentPage * pager.PageSize).Take(pager.PageSize);
            }

            Session["Count"] = items.Count();

            var viewModel = new ProductsViewModel()
            {
                EuroPalletFurniture = items,
                Pager = pager,
                Filters = Models.Filter.GetAll()
            };


            return View(viewModel);
        }

        public ActionResult Ajax(int? page, string orderBy = "Най нови", int pageSize = 10)
        {
            page = page == null ? 1 : page;

            var allEuroPalletsFurniture = this.Data.EuroPalletFurnitures.All().ToList();

            orderBy = orderBy.Replace("\"", string.Empty).Replace(" ", string.Empty);

            Pager pager = new Pager(allEuroPalletsFurniture.Count(), page, pageSize);

            IEnumerable<EuroPalletFurniture> items = new List<EuroPalletFurniture>();

            if (page == 1)
            {
                items = allEuroPalletsFurniture.Take(pager.PageSize);
            }
            else
            {
                items = allEuroPalletsFurniture.OrderBy(x => x.CreatedOn).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);
            }

            Session["Count"] = items.Count();

            if (orderBy.Equals("Найнови"))
            {
                items = items.OrderBy(x => x.CreatedOn);
            }
            else if (orderBy.Equals("Ценавъзх."))
            {
                items = items.OrderBy(x => x.Price).ToList();
            }
            else if (orderBy.Equals("Ценанизх."))
            {
                items = items.OrderByDescending(x => x.Price).ToList();
            }
            else if (orderBy.Equals("Отстъпка%"))
            {
                //TODO Discount !
            }
            else // Рейтинг
            {
                items = items.OrderBy(x => x.Rating);
            }

            var viewModel = new ProductsViewModel()
            {
                EuroPalletFurniture = items,
                Pager = pager,
                Filters = Models.Filter.GetAll()
            };

            return this.PartialView("_ProductsPartial", viewModel);
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