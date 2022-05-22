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

        public ProductService(){}

        public ProductService(IProductRepository productRepository, IProductImageRepository productImageRepository)
        {
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
        }

        public void AddProduct(Product product, IList<FileStream> filesWindow)
        {
            AddImagesToProduct(product, filesWindow);

            _productRepository.AddProduct(product);

            SaveImages(product, filesWindow);
        }

        private void SaveImages(Product product, IList<FileStream> filesWindow)
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

        public void DeleteProduct(int id)
        {
            Product product = _productRepository.GetProductById(id);

            _productRepository.DeleteProduct(product);
        }

        public IList<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);
        }

        public string GetPathFirstImage(int idProduct)
        {
            string filePath = string.Empty;

            ProductImage productImage = _productImageRepository.GetFirstImage(idProduct);

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