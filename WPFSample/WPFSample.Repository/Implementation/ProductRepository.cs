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
    public class ProductRepository : IProductRepository
    {
        public async Task AddProductAsync(Product product)
        {
            using (var db = new WPFSampleDb())
            {
                await db.Set<Product>().AddAsync(product);
                await db.SaveChangesAsync();
            }
        }

        public Task<IList<Product>> ListAllProducts()
        {
            throw new NotImplementedException();
        }
    }
}
