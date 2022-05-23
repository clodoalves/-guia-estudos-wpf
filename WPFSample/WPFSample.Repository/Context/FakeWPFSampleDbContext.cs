using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.Domain;

namespace WPFSample.Repository.Context
{
    class FakeWPFSampleDbContext : IWPFSampleDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
