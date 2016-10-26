﻿
namespace EuroPallets.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class User : IdentityUser
    {
        //public int? ShopingCartEuroPallletsId { get; set; }
        //public virtual ShopingCartEuroPalllets ShopingCartEuroPalllets { get; set; }

        public int? ShopingCartId { get; set; }
        public virtual ShopingCart ShopingCart { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public static string GetRole()
        {
            return User.GetRole();
        }
    }
}
