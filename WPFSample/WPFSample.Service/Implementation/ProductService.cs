using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WPFSample.Domain;
using WPFSample.Repository.Contract;
using WPFSample.Service.Contract;
using System.Linq;

namespace WPFSample.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;

        public ProductService(IProductRepository productRepository, IProductImageRepository productImageRepository)
        {
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
        }

        public async Task AddProductAsync(Product product, IList<FileStream> filesWindow)
        {
            AddImagesToProduct(product, filesWindow);

            await _productRepository.AddProductAsync(product);

            SaveImages(product, filesWindow);
        }

        private static void SaveImages(Product product, IList<FileStream> filesWindow)
        {
            string rootPath = AppDomain.CurrentDomain.BaseDirectory;

            string pathImagesProduct = $"{rootPath}/{product.Id}";

            Directory.CreateDirectory(pathImagesProduct);

            foreach (var item in filesWindow)
            {
                string fileName = Path.GetFileName(item.Name);

                using (FileStream fs = new FileStream($"{pathImagesProduct}/{fileName}", FileMode.Create))
                {
                    item.Seek(0, SeekOrigin.Begin);
                    item.CopyTo(fs);
                }
            }
        }

        private void AddImagesToProduct(Product product, IList<FileStream> filesWindow)
        {
            product.ProductImages = filesWindow.Select(f => new ProductImage()
            {
                Path = Path.GetFileName(f.Name)
            
            }).ToList();        
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

        public async Task<string> GetPathFirstImage(int idProduct) 
        {
            string filePath = string.Empty;

            var productImage =  await _productImageRepository.GetFirstImage(idProduct);

            if (productImage != null)
            {
                string rootPath = $"{AppDomain.CurrentDomain.BaseDirectory}/{idProduct}";

                if (Directory.Exists(rootPath)) 
                {
                    filePath = $"{rootPath}/{productImage.Path}";                    
                }                
            }

            return filePath;
        }

        public void UpdateQuantityProducts(int quantity)
        {
            
        }
    }
}