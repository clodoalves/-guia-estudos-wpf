using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.Domain;

namespace WPFSample.Repository.Contract
{
    public interface IProductRepository
    {
        void AddProduct(Product product);

        IList<Product> GetAllProducts();

        Product GetProductById(int id);
        void UpdateProduct(Product product);

        void DeleteProduct(Product product);
    }
}
