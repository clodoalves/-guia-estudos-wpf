using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.Domain;
using WPFSample.Repository.Context;
using WPFSample.Repository.Contract;

namespace WPFSample.Repository.Implementation
{
    public class ProductImageRepository : IProductImageRepository
    {
        public ProductImage GetFirstImage(int idProduct)
        {
            using (var db = new WPFSampleDbContext())
            {
                return db.ProductImages.Where(p => p.ProductId == idProduct).FirstOrDefault();
            }
        }
    }
}
