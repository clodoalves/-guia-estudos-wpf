using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
                await db.Products.AddAsync(product);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteProductAsync(Product product)
        {
            using (var db = new WPFSampleDb()) 
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
            }
        }

        public async Task<IList<Product>> GetAllProducts()
        {
            using (var db = new WPFSampleDb()) 
            {
                return await db.Products.ToListAsync();
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            using (var db = new WPFSampleDb()) 
            {
                return await db.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            using (var db = new WPFSampleDb()) 
            {
                db.Products.Update(product);

                await db.SaveChangesAsync();
            } 
        }
    }
}