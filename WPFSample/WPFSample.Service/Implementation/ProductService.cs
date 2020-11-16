using System.Collections.Generic;
using System.Threading.Tasks;
using WPFSample.Domain;
using WPFSample.Repository.Contract;
using WPFSample.Service.Contract;

namespace WPFSample.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddProductAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            Product product = await _productRepository.GetProductById(id);

            await _productRepository.DeleteProductAsync(product);
        }

        public async Task<IList<Product>> GetAllProducts() 
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<Product> GetProductById(int id) 
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
        }
    }
}