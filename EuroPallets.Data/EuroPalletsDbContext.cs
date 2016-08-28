
namespace EuroPallets.Data
{
    using EuroPallets.Models;
    using EuroPallets.Models.Base;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
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
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }

        public static EuroPalletsDbContext Create()
        {
            return new EuroPalletsDbContext();
        }

        public virtual IDbSet<Category> Categories { get; set; }
        public virtual IDbSet<EuroPalletFurniture> EuroPalletFurnituries { get; set; }
        public virtual IDbSet<EuroPalletImages> EuroPalletImages { get; set; }
        public virtual IDbSet<GlobalCategory> GlobalCategories { get; set; }
        public virtual IDbSet<ShopingCart> ShopingCart { get; set; }
        public virtual IDbSet<Specification> Specifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            this.OnModelCreating(modelBuilder);
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
