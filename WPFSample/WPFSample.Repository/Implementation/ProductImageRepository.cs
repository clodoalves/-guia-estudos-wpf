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
        public async Task<ProductImage> GetFirstImage(int idProduct)
        {
            using (var db = new WPFSampleDb())
            {
                return await db.ProductImages.Where(p => p.ProductId == idProduct).FirstOrDefaultAsync();
            }
        }
    }
}
