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
            if (!context.Categories.Any())
            {
                SeedCategories(context);
            }
            if (!context.Filter.Any())
            {
                SeedFilter(context);
            }
        }

        private void SeedFilter(EuroPalletsDbContext context)
        {
            List<Filters> filterToSeed = new List<Filters>()
            {
                new Filters()
                {
                    Name ="Цена",
                    FilterChaildName = new List<FilterChaildName>()
                    {
                        new FilterChaildName() {Name = "Всички"},
                        new FilterChaildName() {Name = "0 - 100"},
                        new FilterChaildName() {Name = "100 - 200"},
                        new FilterChaildName() {Name = "200 - 350"},
                        new FilterChaildName() {Name = "350 - 500"},
                        new FilterChaildName() {Name = "над 500"},
                    }
                },
                 new Filters()
                {
                    Name ="Категория",
                    FilterChaildName = new List<FilterChaildName>()
                    {
                        new FilterChaildName() {Name = "Всички"},
                        new FilterChaildName() {Name = "Кухненски маси"},
                        new FilterChaildName() {Name = "Холни маси"},
                        new FilterChaildName() {Name = "Маси за кафе"},
                        new FilterChaildName() {Name = "Арт маси"},
                    }
                },
                  new Filters()
                {
                    Name ="Размер",
                    FilterChaildName = new List<FilterChaildName>()
                    {
                        new FilterChaildName() {Name = "Всички"},
                        new FilterChaildName() {Name = "Малък"},
                        new FilterChaildName() {Name = "Среден"},
                        new FilterChaildName() {Name = "Голям"},
                    }
                },
                    new Filters()
                {
                    Name ="Материал",
                    FilterChaildName = new List<FilterChaildName>()
                    {
                        new FilterChaildName() {Name = "Всички"},
                        new FilterChaildName() {Name = "Дърво"},
                        new FilterChaildName() {Name = "Дърво и стъкло"},
                        new FilterChaildName() {Name = "Дърво и друго"},
                    }
                },
                     new Filters()
                {
                    Name ="Цвят",
                    FilterChaildName = new List<FilterChaildName>()
                    {
                        new FilterChaildName() {Name = "Всички"},
                        new FilterChaildName() {Name = "Необработен"},
                        new FilterChaildName() {Name = "Боядисан"},
                        new FilterChaildName() {Name = "Лакиран"},
                    }
                },
            };

            context.Categories.FirstOrDefault(x => x.Name == "Маси").Filter = filterToSeed;
            context.SaveChanges();
        }

        private void SeedCategories(EuroPalletsDbContext context)
        {
            ICollection<Category> categoryToSeed = new List<Category>()
            {
                new Category() { Name="Маси" },
                new Category() { Name="Дивани" },
                new Category() { Name="Легла" },
                new Category() { Name="Спални" },
                new Category() { Name="Секции" },
                new Category() { Name="Закачалки" },
                new Category() { Name="Арт" },
                new Category() { Name="Кухня" },
                new Category() { Name="Фотьойли" },
                new Category() { Name="Столове" },
                new Category() { Name="Висящи мебели" },
                new Category() { Name="Гардероби" },
                new Category() { Name="Поставка за обувки" },
            };

            foreach (Category category in categoryToSeed)
            {
                context.Categories.Add(category);
            }

            context.GlobalCategories.FirstOrDefault(x => x.Name == "Дом").Category = categoryToSeed;
            context.SaveChanges();
        }

        private void SeedSubCategories(EuroPalletsDbContext context)
        {
            List<SubCategory> furnituresSubCategories = new List<SubCategory>()
            {
                new SubCategory() { Name="Маси за кафе" },
                new SubCategory() { Name="Кухненски маси" },
                new SubCategory() { Name="Помощни маси" },
                new SubCategory() { Name="Други" },
            };
            var globalCategoryToMapp = context.GlobalCategories.FirstOrDefault(x => x.Name == "Продукти");

            //globalCategoryToMapp.SubCategories = furnituresSubCategories;
            context.SaveChanges();
        }

        private void SeedGlobalCategories(EuroPalletsDbContext context)
        {
            List<GlobalCategory> globalCategoryToAdd = new List<GlobalCategory>()
            {
                new GlobalCategory() {Name = "Градина" },
                new GlobalCategory() {Name = "Дом" },
                new GlobalCategory() {Name = "Заведения" },
                new GlobalCategory() {Name = "Хотели" },
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
            var image = GetPhoto(@"C:\Users\tododani\Documents\GitHub\QASDemo\EuroPalletsQAS\EuroPallets\Content\images\Products\diy-furniture-from-euro-pallets-101-craft-ideas-for-wood-pallets-41-359.jpg");
            var image2 = GetPhoto(@"C:\Users\tododani\Documents\GitHub\QASDemo\EuroPalletsQAS\EuroPallets\Content\images\Products\60-diy-furniture-from-euro-pallets-amazing-craft-ideas-for-you-41-130.jpg");

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
