using System.Collections.Generic;
using NUnit.Framework;
using System.IO;
using WPFSample.Domain;
using WPFSample.Service.Contract;
using WPFSample.Service.Implementation;
using WPFSample.Service.Exceptions.Product;
using Moq;
using WPFSample.Repository.Contract;
using System.Linq;

namespace WPFSample.Test.Service
{
    [TestFixture]
    public class ProductServiceTest
    {
        private Product _mockProduct;
        IList<FileStream> _files;
        private IProductService _productService;
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<IProductImageRepository> _mockProductImageRepository;

        [SetUp]
        public void ConfigureDependecies()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductImageRepository = new Mock<IProductImageRepository>();
            _files = new List<FileStream>();
            _productService = new ProductService(_mockProductRepository.Object, _mockProductImageRepository.Object);
            _mockProduct = new Product();

            _mockProduct.Title = "New Smartphone";
            _mockProduct.Description = "This is such a nice smartphone";
            _mockProduct.Price = 1000;
            _mockProduct.Quantity = 15;
            _mockProduct.ProductImages = new List<ProductImage>();
        }

        [Test]
        public void AddProductWithoutTitleTest()
        {
            _mockProduct.Title = string.Empty;
            
            Assert.Throws(typeof(RequiredFieldException), () => _productService.AddOrUpdateProduct(_mockProduct));
        }

        [Test]
        public void AddProductWithoutPriceTest()
        {
            _mockProduct.Price = 0;
           
            Assert.Throws(typeof(RequiredFieldException), () => _productService.AddOrUpdateProduct(_mockProduct));
        }

        [Test]
        public void AddProductWithPriceLessThanZeroTest()
        {
            _mockProduct.Price = -2;
         
            Assert.Throws(typeof(NumericFieldLessThanZeroException), () => _productService.AddOrUpdateProduct(_mockProduct));
        }

        [Test]
        public void AddProductWithQuantityLessThanZeroTest()
        {       
            _mockProduct.Quantity = -5;

            Assert.Throws(typeof(NumericFieldLessThanZeroException), () => _productService.AddOrUpdateProduct(_mockProduct));
        }

        [Test]
        public void AddProductWithTitleThatExceedsLimitCharactersTest()
        {
            _mockProduct.Title = "Product with loooooooooooooooooooooooooooooooooooooooooooooooooog title";
            
            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(_mockProduct));
        }

        [Test]
        public void AddProductWithDescriptionThatExceedsLimitCharactersTest()
        {         
            _mockProduct.Description = "Product with loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong description";
         
            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(_mockProduct));
        }

        [Test]
        public void AddProductWithPriceThatExceedsLimitNumbersTest()
        {
            _mockProduct.Price = 100000000000;
            
            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(_mockProduct));
        }

        [Test]
        public void AddProductWithQuantityThatExceedsLimitNumbersTest()
        {
            _mockProduct.Quantity = 100000000;

            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(_mockProduct));
        }

        [Test]
        public void SaveNewProductWithoutImagesTest()
        {
            int newProductId = 1;

            Product newProduct = new Product()
            {
                Id = newProductId,
                Title = "New Smartphone",
                Description = "This is such a nice smartphone",
                Price = 1000,
                Quantity = 15
            };

            _mockProductRepository.Setup(s => s.Add(It.IsAny<Product>()))
                .Callback((Product p) => p = newProduct);
            _mockProductRepository.Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(newProduct);
     
            _productService.AddOrUpdateProduct(_mockProduct);

            Product savedProduct = _productService.GetProductById(newProductId);

            Assert.AreEqual(newProductId, savedProduct.Id);
        }

        [Test]
        public void UpdateTitleProductTest()
        {
            int productId = 110;
            _mockProduct.Id = productId;           
            _mockProductRepository.Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(_mockProduct);

            Product originalProduct = _productService.GetProductById(_mockProduct.Id);            
            string newTitle = "New title";
            originalProduct.Title = newTitle;
            _productService.AddOrUpdateProduct(originalProduct);

            Product updatedProduct = _productService.GetProductById(productId);

            Assert.AreEqual(newTitle, updatedProduct.Title);
        }

        [Test]
        public void UpdateDescriptionProductTest()
        {
            int productId = 110; 
            _mockProductRepository.Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(_mockProduct);

            Product originalProduct = _productService.GetProductById(productId);            
            string newDescription = "This is a pretty nice product";
            originalProduct.Description = newDescription;
            _productService.AddOrUpdateProduct(originalProduct);

            Product updatedProduct = _productService.GetProductById(productId);

            Assert.AreEqual(newDescription, updatedProduct.Description);
        }

        [Test]
        public void UpdatePriceProductTest() 
        {
            int productId = 110;
            _mockProduct.Id = productId;
            _mockProductRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(_mockProduct);

            Product originalProduct = _productService.GetProductById(productId);
            int newPrice = 3500;
            originalProduct.Price = newPrice;
            _productService.AddOrUpdateProduct(originalProduct);

            Product updatedProduct = _productService.GetProductById(productId);
            
            Assert.AreEqual(_mockProduct.Price, updatedProduct.Price);
        }

        [Test]
        public void UpdateQuantityProductTest() 
        {
            int productId = 110;
            _mockProduct.Id = productId;

            _mockProductRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(_mockProduct);

            Product originalProduct = _productService.GetProductById(productId);
            int newQuantity = 300;
            originalProduct.Quantity = newQuantity;
            _productService.AddOrUpdateProduct(originalProduct);

            Product updatedProduct = _productService.GetProductById(productId);

            Assert.AreEqual(newQuantity, updatedProduct.Quantity);
        }

        [TearDown]
        public void ClearConfiguration()
        {
            _mockProduct = null;
            _files = null;
            _productService = null;
            _mockProductRepository = null;
            _mockProductImageRepository = null;
        }
    }
}
