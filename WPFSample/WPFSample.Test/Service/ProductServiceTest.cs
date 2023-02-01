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
        private IProductService _productService;
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<IProductImageRepository> _mockProductImageRepository;

        [SetUp]
        public void ConfigureDependecies()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductImageRepository = new Mock<IProductImageRepository>();
            _mockProduct = new Product();
        }

        [Test]
        public void AddProductWithoutTitleTest()
        {
            //Arrange
            Product product = new Product()
            {
                Id = 1,
                Description = "This is a nice smartphone",
                Title = string.Empty,
                Price = 1000,
                Quantity = 1
            };

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
            //arrange
            _mockProduct.Id = 1;

            _mockProductRepository.Setup(s => s.Add(It.IsAny<Product>()));
            _mockProductRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(_mockProduct);
            _productService = new ProductService(_mockProductRepository.Object, _mockProductImageRepository.Object);

            //act
            _productService.AddOrUpdateProduct(_mockProduct);
            Product savedProduct = _productService.GetProductById(1000);

            //assert
            Assert.AreEqual(_mockProduct.Id, savedProduct.Id);
        }

        [Test]
        public void UpdateTitleProductTest()
        {
            int productId = 110;
            string oldTitle = "Old title";
            string newTitle = "New title";

            _mockProduct.Id = productId;
            _mockProductRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(new Product() { Id = productId, Title = oldTitle, Price = 100, ProductImages = new List<ProductImage>() });
            _mockProductRepository.Setup(s => s.Update(It.IsAny<Product>())).Callback(() => new Product() { Id = productId, Title = newTitle, Price = 100, ProductImages = new List<ProductImage>() });

            _productService = new ProductService(_mockProductRepository.Object, _mockProductImageRepository.Object);

            Product databaseProduct = _productService.GetProductById(productId);
            databaseProduct.Title = newTitle;
            var foo = _productService.AddOrUpdateProduct(databaseProduct);

            Assert.AreEqual(newTitle, databaseProduct.Title);
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

        [Test]
        public void SaveNewProductWithOneImageTest()
        {
            //TODO
        }

        public void SaveNewProductWithMultipleImagesTest()
        {
            //TODO
        }

        [TearDown]
        public void ClearConfiguration()
        {
            _mockProduct = null;
            _productService = null;
            _mockProductRepository = null;
            _mockProductImageRepository = null;
        }
    }
}
