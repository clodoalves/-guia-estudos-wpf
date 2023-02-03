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
using System.Runtime.InteropServices.WindowsRuntime;
using NUnit.Framework.Constraints;

namespace WPFSample.Test.Service
{
    [TestFixture]
    public class ProductServiceTest
    {
        private IProductService _productService;
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<IProductImageRepository> _mockProductImageRepository;

        [SetUp]
        public void ConfigureDependecies()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductImageRepository = new Mock<IProductImageRepository>();
        }

        [Test]
        public void AddProductWithoutTitleTest()
        {
            Product product = new Product()
            {
                Id = 1,
                Description = "This is a nice smartphone",
                Title = string.Empty,
                Price = 1000,
                Quantity = 1
            };

            Assert.Throws(typeof(RequiredFieldException), () => _productService.AddOrUpdateProduct(product));
        }

        [Test]
        public void AddProductWithTitleThatExceedsLimitCharactersTest()
        {
            Product product = new Product()
            {
                Id = 1,
                Description = "This is a nice smartphone",
                Title = "Product with loooooooooooooooooooooooooooooooooooooooooooooooooog title",
                Price = 1000,
                Quantity = 1
            };

            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(product));
        }

        [Test]
        public void AddProductWithoutPriceTest()
        {
            Product product = new Product()
            {
                Id = 1,
                Description = "This is a nice smartphone",
                Title = string.Empty,
                Price = 0,
                Quantity = 1
            };

            Assert.Throws(typeof(RequiredFieldException), () => _productService.AddOrUpdateProduct(product));
        }

        [Test]
        public void AddProductWithPriceLessThanZeroTest()
        {
            Product product = new Product()
            {
                Id = 1,
                Description = "This is a nice smartphone",
                Title = string.Empty,
                Price = -1,
                Quantity = 1
            };

            Assert.Throws(typeof(NumericFieldLessThanZeroException), () => _productService.AddOrUpdateProduct(product));
        }

        [Test]
        public void AddProductWithQuantityLessThanZeroTest()
        {
            Product product = new Product()
            {
                Id = 1,
                Description = "This is a nice smartphone",
                Title = string.Empty,
                Price = 1000,
                Quantity = -1
            };

            Assert.Throws(typeof(NumericFieldLessThanZeroException), () => _productService.AddOrUpdateProduct(product));
        }

        [Test]
        public void AddProductWithQuantityThatExceedsLimitNumbersTest()
        {
            Product product = new Product()
            {
                Id = 1,
                Description = "This is a nice smartphone",
                Title = string.Empty,
                Price = 1000,
                Quantity = 100000000
            };

            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(product));
        }

        [Test]
        public void AddProductWithDescriptionThatExceedsLimitCharactersTest()
        {
            Product product = new Product()
            {
                Id = 1,
                Description = "Product with loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong description",
                Title = "Nice product",
                Price = 1000,
                Quantity = 1
            };

            product.Description = "";

            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(product));
        }

        [Test]
        public void AddProductWithPriceThatExceedsLimitNumbersTest()
        {
            Product product = new Product()
            {
                Id = 1,
                Description = "This is a nice smartphone",
                Title = "Nice product",
                Price = 100000000000,
                Quantity = 1
            };

            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(product));
        }

        [Test]
        public void SaveNewProductWithoutImagesTest()
        {
            //arrange
            _productService = new ProductService(_mockProductRepository.Object, _mockProductImageRepository.Object);

            //act
            Product productAdded = _productService.AddOrUpdateProduct(new Product() {Title = "New product", Price = 1000 });

            //assert
            Assert.IsTrue(productAdded != null);
        }

        [Test]
        public void SaveNewProductWithImages() 
        {
            //arrange
            _productService = new ProductService(_mockProductRepository.Object, _mockProductImageRepository.Object);

            //act
            Product product = new Product()
            {
                Title = "New Product",
                Price = 1000,
                ProductImages = new List<ProductImage>()
                {
                    new ProductImage() { Path = @"C:\\FakePath\Image1" },                              
                    new ProductImage() { Path = @"C:\\FakePath\Image2" }                              
                } 
            };

            //act
            Product productAdded = _productService.AddOrUpdateProduct(product);

            //assert
            Assert.IsTrue(productAdded != null);
        }

        [Test]
        public void UpdateTitleProductTest()
        {
            //int productId = 110;
            //string oldTitle = "Old title";
            //string newTitle = "New title";

            //_mockProduct.Id = productId;
            //_mockProductRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(new Product() { Id = productId, Title = oldTitle, Price = 100, ProductImages = new List<ProductImage>() });
            //_mockProductRepository.Setup(s => s.Update(It.IsAny<Product>())).Callback(() => new Product() { Id = productId, Title = newTitle, Price = 100, ProductImages = new List<ProductImage>() });

            //_productService = new ProductService(_mockProductRepository.Object, _mockProductImageRepository.Object);

            //Product databaseProduct = _productService.GetProductById(productId);
            //databaseProduct.Title = newTitle;
            //var foo = _productService.AddOrUpdateProduct(databaseProduct);

            //Assert.AreEqual(newTitle, databaseProduct.Title);
        }

        [Test]
        public void UpdateDescriptionProductTest()
        {
            //int productId = 110;
            //_mockProductRepository.Setup(s => s.GetById(It.IsAny<int>()))
            //    .Returns(_mockProduct);

            //Product originalProduct = _productService.GetProductById(productId);
            //string newDescription = "This is a pretty nice product";
            //originalProduct.Description = newDescription;
            //_productService.AddOrUpdateProduct(originalProduct);

            //Product updatedProduct = _productService.GetProductById(productId);

            //Assert.AreEqual(newDescription, updatedProduct.Description);
        }

        [Test]
        public void UpdatePriceProductTest()
        {
            //int productId = 110;
            //_mockProduct.Id = productId;
            //_mockProductRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(_mockProduct);

            //Product originalProduct = _productService.GetProductById(productId);
            //int newPrice = 3500;
            //originalProduct.Price = newPrice;
            //_productService.AddOrUpdateProduct(originalProduct);

            //Product updatedProduct = _productService.GetProductById(productId);

            //Assert.AreEqual(_mockProduct.Price, updatedProduct.Price);
        }

        [Test]
        public void UpdateQuantityProductTest()
        {
            //int productId = 110;
            //_mockProduct.Id = productId;

            //_mockProductRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(_mockProduct);

            //Product originalProduct = _productService.GetProductById(productId);
            //int newQuantity = 300;
            //originalProduct.Quantity = newQuantity;
            //_productService.AddOrUpdateProduct(originalProduct);

            //Product updatedProduct = _productService.GetProductById(productId);

            //Assert.AreEqual(newQuantity, updatedProduct.Quantity);
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
            _productService = null;
            _mockProductRepository = null;
            _mockProductImageRepository = null;
        }
    }
}
