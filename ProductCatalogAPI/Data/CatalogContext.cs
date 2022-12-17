using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Domain;

namespace ProductCatalogAPI.Data
{
    public class CatalogContext : DbContext
    {
        public DbSet<CatalogType> CatalogTypes { get; set; }    
        public DbSet<CatalogBrand> CatalogBrands { get; set; }  
        public DbSet<CatalogItem> Catalog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogType>(e => )
        }
    }
}
