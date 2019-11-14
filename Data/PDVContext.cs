using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PDV.API.Data.Entities;

namespace PDV.API.Data
{
    public class PDVContext : IdentityDbContext<Account,AppRole,string>
    {
        /*Inicio zona de DbSets*/
        public DbSet<Store> Stores { get; set; }
        public DbSet<AccountStore> AccountStores { get; set; }
        /*Fin zona de DbSets*/
        public PDVContext(DbContextOptions<PDVContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //https://docs.microsoft.com/en-us/ef/core/modeling/

            modelBuilder.Entity<Store>(x =>
            {
                x.ToTable("Stores");
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).ValueGeneratedOnAdd();
                x.Property(x => x.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<AccountStore>(x =>{
                x.ToTable("AccountStores");
                x.HasKey(x => new { x.AccountId, x.StoreId });
                x.HasOne(x => x.Account).WithMany().HasForeignKey(x => x.AccountId);
                x.HasOne(x => x.Store).WithMany().HasForeignKey(x => x.StoreId);
            });

            modelBuilder.Entity<Product>(x => {
                x.ToTable("Products");
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).ValueGeneratedOnAdd();
                x.HasOne(x => x.Store).WithMany().HasForeignKey(x => x.StoreId).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}