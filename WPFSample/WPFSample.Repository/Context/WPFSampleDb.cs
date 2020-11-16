using Microsoft.EntityFrameworkCore;

using WPFSample.Domain;

namespace WPFSample.Repository.Context
{
    public class WPFSampleDb : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source= data.db");
        }
    }
}
