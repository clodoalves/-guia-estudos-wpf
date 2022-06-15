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
        public void AddProduct(Product product)
        {
            using (var db = new WPFSampleDbContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        public void DeleteProduct(Product product)
        {
            using (var db = new WPFSampleDbContext()) 
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }

        public IList<Product> GetAllProducts()
        {
            using (var db = new WPFSampleDbContext()) 
            {
                return db.Products.ToList();
            }
        }

        public Product GetProductById(int id)
        {
            using (var db = new WPFSampleDbContext())
            {
                return db.Products.Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var db = new WPFSampleDbContext()) 
            {
                db.Products.Update(product);

                db.SaveChanges();
            } 
        }
    }
}