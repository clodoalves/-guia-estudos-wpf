using Microsoft.EntityFrameworkCore;
using WPFSample.Domain;
using WPFSample.Repository.Mapping;

namespace WPFSample.Repository.Context
{
    public class WPFSampleDbContext : DbContext, IDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=D:\data.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ProductImageMap());
        }
    }
}
