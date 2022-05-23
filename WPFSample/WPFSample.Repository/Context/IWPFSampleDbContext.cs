using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.Domain;

namespace WPFSample.Repository.Context
{
    public interface IWPFSampleDbContext
    {
        DbSet<Product> Products { get;}
        DbSet<ProductImage> ProductImages { get;}

        int SaveChanges();
    }
}
