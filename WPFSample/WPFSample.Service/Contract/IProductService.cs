using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.Domain;
using System.IO;

namespace WPFSample.Service.Contract
{
    public interface IProductService
    {
        void AddProduct(Product product, IList<FileStream> filesWindow);

        IList<Product> GetAllProducts();

        Product GetProductById(int id);

        void UpdateProduct(Product product);

        void DeleteProduct(int id);

        //Task<ProductImage> GetFirstImage(int idProduct);

        string GetPathFirstImage(int idProduct);
        void UpdateQuantityProducts(int quantity);
    }
}
