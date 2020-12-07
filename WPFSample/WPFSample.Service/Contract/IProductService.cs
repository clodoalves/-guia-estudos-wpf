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
        Task AddProductAsync(Product product, IList<FileStream> filesWindow);

        Task<IList<Product>> GetAllProducts();

        Task<Product> GetProductById(int id);

        Task UpdateProductAsync(Product product);

        Task DeleteProductAsync(int id);

        //Task<ProductImage> GetFirstImage(int idProduct);

        Task<string> GetPathFirstImage(int idProduct);
        void UpdateQuantityProducts(int quantity);
    }
}
