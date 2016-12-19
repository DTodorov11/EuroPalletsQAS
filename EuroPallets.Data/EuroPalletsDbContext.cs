
namespace EuroPallets.Data
{
    using EuroPallets.Models;
    using EuroPallets.Models.Base;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EuroPalletsDbContext : IdentityDbContext<User>, IEuroPalletsDbContext
    {
        public EuroPalletsDbContext()
            : base("EuroPalletsDbContext", throwIfV1Schema: false)
        {
            //disable lazy loading and proxy creation to avoid
            //circular reference
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EuroPalletsDbContext, Configuration>());
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }

        public static EuroPalletsDbContext Create()
        {
            return new EuroPalletsDbContext();
        }

        public virtual IDbSet<Category> Categories { get; set; }
        public virtual IDbSet<EuroPalletFurniture> EuroPalletFurnituries { get; set; }
        public virtual IDbSet<EuroPalletImage> EuroPalletImages { get; set; }
        public virtual IDbSet<GlobalCategory> GlobalCategories { get; set; }
        public virtual IDbSet<ShopingCart> ShopingCart { get; set; }
        public virtual IDbSet<Specification> Specifications { get; set; }
        public virtual IDbSet<ShopingCartEuroPalllets> ShopingCartEuroPalllets { get; set; }
        public virtual IDbSet<AnonymousShopingCart> AnonymousShopingCarts { get; set; }
        public virtual IDbSet<SubCategory> SubCategories { get; set; }
        public virtual IDbSet<PayPalPayment> PayPalPayments { get; set; }
        public virtual IDbSet<Filters> Filter { get; set; }
        public virtual IDbSet<FilterChaildName> FilterChaildName { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            modelBuilder.Entity<Specification>().HasKey(x => x.EuroPalletFurnitureId).HasRequired(x=>x.EuroPalletFurniture);
            modelBuilder.Entity<User>().HasOptional(x => x.ShopingCart).WithOptionalPrincipal(x => x.User);


            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();

            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO
                //EXCEPTION
               throw new Exception(ex.ToString());
            }
        }
        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
