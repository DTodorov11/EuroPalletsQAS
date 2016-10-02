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
            if (!context.GlobalCategories.Any())
            {
                SeedGlobalCategories(context);
            }
            if (!context.SubCategories.Any())
            {
                SeedSubCategories(context);
            }
        }

        private void SeedSubCategories(EuroPalletsDbContext context)
        {
            List<SubCategory> furnituresSubCategories = new List<SubCategory>()
            {
                new SubCategory() { Name="Столове" },
                new SubCategory() { Name="Дивани" },
                new SubCategory() { Name="Маси" },
                new SubCategory() { Name="Гардероби" },
                new SubCategory() { Name="Закачалки" },
                new SubCategory() { Name="Бюра" },
                new SubCategory() { Name="Секции" },
                new SubCategory() { Name="Други" },
            };
            var globalCategoryToMapp = context.GlobalCategories.FirstOrDefault(x => x.Name == "Продукти");

            globalCategoryToMapp.SubCategories = furnituresSubCategories;
            context.SaveChanges();
        }

        private void SeedGlobalCategories(EuroPalletsDbContext context)
        {
            List<GlobalCategory> globalCategoryToAdd = new List<GlobalCategory>()
            {
                new GlobalCategory() {Name = "Продукти" },
                new GlobalCategory() {Name = "Промоции" },
                new GlobalCategory() {Name = "Подаръци" },
                new GlobalCategory() {Name = "Страници" },
            };

            foreach (var item in globalCategoryToAdd)
            {
                context.GlobalCategories.Add(item);
            }

            context.SaveChanges();
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
            //var image = GetPhoto(@"C:\Users\LapTop\Documents\GitHub\EuroPalletsQAS\EuroPallets\Content\images\Products\diy-furniture-from-euro-pallets-101-craft-ideas-for-wood-pallets-41-359.jpg");
            //var image2 = GetPhoto(@"C:\Users\LapTop\Documents\GitHub\EuroPalletsQAS\EuroPallets\Content\images\Products\60-diy-furniture-from-euro-pallets-amazing-craft-ideas-for-you-41-130.jpg");
            var image = GetPhoto(@"C:\Users\tododani\Documents\GitHub\EuroPalletsQAS\EuroPallets\Content\images\Products\diy-furniture-from-euro-pallets-101-craft-ideas-for-wood-pallets-41-359.jpg");
            var image2 = GetPhoto(@"C:\Users\tododani\Documents\GitHub\EuroPalletsQAS\EuroPallets\Content\images\Products\60-diy-furniture-from-euro-pallets-amazing-craft-ideas-for-you-41-130.jpg");

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
                    Name_Description = "Тест описание на името",
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
