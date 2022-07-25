using System.Collections.Generic;
using WPFSample.Domain;
using System.IO;

namespace WPFSample.Service.Contract
{
    public interface IProductService
    {
        void AddOrUpdateProduct(Product product);

        IList<Product> GetAllProducts();

        Product GetProductById(int id);

        void DeleteProduct(int id);

        string GetPathFirstImage(int idProduct);
        void UpdateQuantityProducts(int quantity);
    }
}
