namespace EuroPallets.Data.Migrations
{
    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EuroPalletsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EuroPalletsDbContext context)
        {
            if (!context.Roles.Any())
            {
                SeedRoles(context);
            }
            if (!context.Users.Any())
            {
                SeedAdmins(context);
            }

            if (context.EuroPalletFurnituries.Count() < 10)
            {
                SeedEuroPalletsFurnitures(context);
            }
        }

        private static void SeedRoles(EuroPalletsDbContext context)
        {
            using (var roleStore = new RoleStore<IdentityRole>(context))
            {
                using (var roleManager = new RoleManager<IdentityRole>(roleStore))
                {
                    var admin = new IdentityRole { Name = GlobalConstants.AdministratorRoleName };
                    var agancy = new IdentityRole { Name = GlobalConstants.CustomerRoleName };

                    roleManager.Create(admin);
                    roleManager.Create(agancy);
                }
            }
        }
        private static void SeedAdmins(EuroPalletsDbContext context)
        {
            using (var userStore = new UserStore<User>(context))
            {
                using (var userManager = new UserManager<User>(userStore))
                {
                    var administratorUserName = "admin@admin.com";
                    var administratorPassword = "admin@admin.com";

                    var user = new User
                    {
                        UserName = administratorUserName,
                        Email = administratorUserName
                    };

                    userManager.Create(user, administratorPassword);
                    userManager.AddToRole(user.Id, GlobalConstants.AdministratorRoleName);
                }
            }
        }

        private static void SeedEuroPalletsFurnitures(EuroPalletsDbContext context)
        {
            var image = GetPhoto(@"C:\Users\Daniel\Documents\GitHub\EuroPallets\EuroPallets\Content\images\Products\diy-furniture-from-euro-pallets-101-craft-ideas-for-wood-pallets-41-359.jpg");
            var image2 = GetPhoto(@"C:\Users\Daniel\Documents\GitHub\EuroPallets\EuroPallets\Content\images\Products\60-diy-furniture-from-euro-pallets-amazing-craft-ideas-for-you-41-130.jpg");

            for (int i = 0; i < 10; i++)
            {
                context.EuroPalletFurnituries.Add(new EuroPalletFurniture()
                {
                    DeliveryPrice = 5,
                    Description = "Test description",
                    EuroPalletImages = new List<EuroPalletImage>()
                    {
                        new EuroPalletImage
                        {
                            Image = image
                        },
                        new EuroPalletImage
                        {
                            Image = image2
                        },
                    },
                    IsAvailable = true,
                    Name = "Столове",
                    Price = 200,
                    Quantity = 5,
                    Rating = 5,
                });
            }
            context.SaveChanges();
        }

        private static byte[] GetPhoto(string filePath)
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
