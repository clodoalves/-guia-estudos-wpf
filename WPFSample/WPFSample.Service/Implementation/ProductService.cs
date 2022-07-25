using System;
using System.Collections.Generic;
using System.IO;
using WPFSample.Domain;
using WPFSample.Repository.Contract;
using WPFSample.Service.Contract;
using System.Linq;
using WPFSample.Service.Exceptions.Product;
using System.Text;
using WPFSample.Repository;

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

        public void AddOrUpdateProduct(Product product)
        {
            ValidateRequiredFields(product);
            ValidateNumericFields(product);
            ValidateLimitCharacters(product);

            if (product.Id != 0)
            {
                Product databaseRegister = GetProductById(product.Id);

                databaseRegister.Title = product.Title;
                databaseRegister.Description = product.Description;
                databaseRegister.Price = product.Price;
                databaseRegister.Quantity = product.Quantity;

                _productRepository.Update(databaseRegister);
            }
            else
            {
                _productRepository.Add(product);
            }

            SaveImages(product);
        }

        private void ValidateRequiredFields(Product product)
        {
            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(product.Title))
            {
                sb.AppendLine($"{nameof(product.Title)} is required!");
            }

            if (product.Price == 0)
            {
                sb.AppendLine($"{nameof(product.Price)} is required!");
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

            if (product.Title.Length > MaxLengthConstValues.MAX_LENGTH_PRODUCT_TITLE)
            {
                sb.AppendLine($"{nameof(product.Title)} has limit of {MaxLengthConstValues.MAX_LENGTH_PRODUCT_TITLE} characteres");
            }

            if (product.Description.Length > MaxLengthConstValues.MAX_LENGTH_PRODUCT_DESCRIPTION)
            {
                sb.AppendLine($"{nameof(product.Description)} has limit of {MaxLengthConstValues.MAX_LENGTH_PRODUCT_DESCRIPTION} characteres");
            }

            if (product.Price.ToString().Length > MaxLengthConstValues.MAX_LENGTH_PRODUCT_PRICE)
            {
                sb.AppendLine($"{nameof(product.Price)} has limit of {MaxLengthConstValues.MAX_LENGTH_PRODUCT_PRICE} characteres");
            }

            if (product.Quantity.ToString().Length > MaxLengthConstValues.MAX_LENGTH_PRODUCT_QUANTITY)
            {
                sb.AppendLine($"{nameof(product.Quantity)} has limit of {MaxLengthConstValues.MAX_LENGTH_PRODUCT_QUANTITY} characteres");
            }

            if (sb.Length > 0)
                throw new FieldExceedCaracterLimitException(sb.ToString());
        }

        private void SaveImages(Product product)
        {
            string rootPath = AppDomain.CurrentDomain.BaseDirectory;

            string pathImagesProduct = $"{rootPath}/{product.Id}";

            Directory.CreateDirectory(pathImagesProduct);

            foreach (var productImage in product.ProductImages)
            {
                FileStream originalFile = new FileStream(productImage.Path, FileMode.Open, FileAccess.Read);

                string fileName = Path.GetFileName(productImage.Path);

                using (FileStream fs = new FileStream($"{pathImagesProduct}/{fileName}", FileMode.Create))
                {
                    originalFile.Seek(0, SeekOrigin.Begin);
                    originalFile.CopyTo(fs);
                }
            }
        }

        public void DeleteProduct(int id)
        {
            Product product = _productRepository.GetById(id);

            _productRepository.Delete(product);
        }

        public IList<Product> GetAllProducts()
        {
            return _productRepository.GetAll().ToList();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetById(id);
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