using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WPFSample.Domain;
using WPFSample.Repository.Contract;
using WPFSample.Service.Contract;
using System.Linq;
using WPFSample.Service.Exceptions.Product;
using System.Text;

namespace WPFSample.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;

        public ProductService() { }

        public ProductService(IProductRepository productRepository, IProductImageRepository productImageRepository)
        {
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
        }

        public void AddProduct(Product product, IList<FileStream> filesWindow)
        {
            ValidateRequiredFields(product);
            ValidateNumericFields(product);
            ValidateLimitCharacters(product);

            AddImagesToProduct(product, filesWindow);

            _productRepository.AddProduct(product);

            SaveImages(product, filesWindow);
        }

        private void ValidateRequiredFields(Product product)
        {
            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(product.Title))
            {
                sb.AppendLine($"{nameof(product.Title)} is required!");
            }
            if (string.IsNullOrWhiteSpace(product.Description))
            {
                sb.AppendLine($"{nameof(product.Description)} is required!");
            }
            if (product.Price == 0)
            {
                sb.AppendLine($"{nameof(product.Price)} is required!");
            }

            if (product.Quantity == 0)
            {
                sb.AppendLine($"{nameof(product.Quantity)} is required!");
            }

            if (sb.Length > 0)
                throw new RequiredFieldException(sb.ToString());
        }
        private void ValidateNumericFields(Product product)
        {
            StringBuilder sb = new StringBuilder();

            if (product.Price < 0)
            {
                sb.AppendLine($"{product.Price} less than zero");
            }
            if (product.Quantity < 0)
            {
                sb.AppendLine($"{product.Quantity} less than zero");
            }

            if (sb.Length > 0)
                throw new NumericFieldLessThanZeroException(sb.ToString());
        }
        private void ValidateLimitCharacters(Product product)
        {
            StringBuilder sb = new StringBuilder();

            if (product.Title.Length > 25)
            {
                sb.AppendLine($"{nameof(product.Title)} has limit of 25 characteres");
            }

            if (product.Description.Length > 80)
            {
                sb.AppendLine($"{nameof(product.Description)} has limit of 200 characteres");
            }

            if (product.Price.ToString().Length > 10)
            {
                sb.AppendLine($"{nameof(product.Price)} has limit of 10 characteres");
            }

            if (product.Quantity.ToString().Length > 4)
            {
                sb.AppendLine($"{nameof(product.Quantity)} has limit of 4 characteres");
            }

            if (sb.Length > 0)
                throw new FieldExceedCaracterLimitException(sb.ToString());
        }

        private void AddImagesToProduct(Product product, IList<FileStream> filesWindow)
        {
            product.ProductImages = filesWindow.Select(f => new ProductImage()
            {
                Path = Path.GetFileName(f.Name)

            }).ToList();
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