using Microsoft.EntityFrameworkCore;

using WPFSample.Domain;

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
    }
}
