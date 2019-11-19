using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PDV.API.Data.Entities;
using System.Text.RegularExpressions;

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

            AdjustNamesForPostgreSqlStandard(modelBuilder);
        }
        /// <summary>
        /// Cambia los nombres al format: xxx_xx_xx
        /// </summary>
        /// <param name="modelBuilder">modelod de tablas</param>
        private void AdjustNamesForPostgreSqlStandard(ModelBuilder modelBuilder) {

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Replace table names
                entity.SetTableName(entity.GetTableName().ToSnakeCase());
                // Replace column names            
                foreach (var property in entity.GetProperties())
                    property.SetColumnName(property.GetColumnName().ToSnakeCase());
                
                foreach (var key in entity.GetKeys())
                    key.SetName(key.GetName().ToSnakeCase());
                
                foreach (var key in entity.GetForeignKeys())
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
                
                foreach (var index in entity.GetIndexes())
                    index.SetName(index.GetName().ToSnakeCase());   
            }
        }

       
    }
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }
    }
}