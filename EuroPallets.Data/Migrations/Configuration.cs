namespace EuroPallets.Data.Migrations
{
    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
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
    }
}
