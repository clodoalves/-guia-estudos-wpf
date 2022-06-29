using WPFSample.Domain;
using WPFSample.Repository.Contract;
using System.Linq;

namespace WPFSample.Repository.Implementation
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public Product GetProductByTitle(string title) 
        {
            return dbContext.Products.Where(p => p.Title == title).FirstOrDefault();
        }
    }
}