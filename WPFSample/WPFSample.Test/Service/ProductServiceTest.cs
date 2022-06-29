using System.Collections.Generic;
using NUnit.Framework;
using System.IO;
using WPFSample.Domain;
using WPFSample.Service.Contract;
using WPFSample.Service.Implementation;
using WPFSample.Service.Exceptions.Product;
using Moq;
using WPFSample.Repository.Contract;

namespace WPFSample.Test.Service
{
    [TestFixture]
    public class ProductServiceTest
    {
        private Product _product;
        IList<FileStream> _files;
        private IProductService _productService;
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<IProductImageRepository> _mockProductImageRepository;

        [SetUp]
        public void ConfigureDependecies()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductImageRepository = new Mock<IProductImageRepository>();

            _product = new Product();
            _files = new List<FileStream>();
            _productService = new ProductService(_mockProductRepository.Object, _mockProductImageRepository.Object);
        }

        [Test]
        public void AddProductWithoutTitleTest()
        {
            _product.Title = string.Empty;
            _product.Description = "New product";
            _product.Price = 100;
            _product.Quantity = 1;

            Assert.Throws(typeof(RequiredFieldException), () => _productService.AddOrUpdateProduct(_product, _files));
        }

        [Test]
        public void AddProductWithoutPrice()
        {
            _product.Title = "New laptop";
            _product.Description = "New product";
            _product.Price = 0;
            _product.Quantity = 10;

            Assert.Throws(typeof(RequiredFieldException), () => _productService.AddOrUpdateProduct(_product, _files));
        }

        [Test]
        public void AddProductWithPriceLessThanZero()
        {
            _product.Title = "New laptop";
            _product.Description = "New product";
            _product.Price = -2;
            _product.Quantity = 10;

            Assert.Throws(typeof(NumericFieldLessThanZeroException), () => _productService.AddOrUpdateProduct(_product, _files));
        }

        [Test]
        public void AddProductWithQuantityLessThanZero()
        {
            _product.Title = "New laptop";
            _product.Description = "New product";
            _product.Price = 3;
            _product.Quantity = -5;

            Assert.Throws(typeof(NumericFieldLessThanZeroException), () => _productService.AddOrUpdateProduct(_product, _files));
        }

        [Test]
        public void AddProductWithTitleThatExceedsLimitCharacters()
        {
            _product.Title = "Product with loooooooooooooooooooooooooooooooooooooooooooooooooog title";
            _product.Description = "New product";
            _product.Price = 3;
            _product.Quantity = 1;

            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(_product, _files));
        }

        [Test]
        public void AddProductWithDescriptionThatExceedsLimitCharacters()
        {
            _product.Title = "New laptop";
            _product.Description = "Product with loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong description";
            _product.Price = 3;
            _product.Quantity = 1;

            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(_product, _files));
        }

        [Test]
        public void AddProductWithPriceThatExceedsLimitNumbers()
        {
            _product.Title = "New laptop";
            _product.Description = "New product";
            _product.Price = 100000000000;
            _product.Quantity = 1;

            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(_product, _files));
        }

        [Test]
        public void AddProductWithQuantityThatExceedsLimitNumbers()
        {
            _product.Title = "New laptop";
            _product.Description = "New product";
            _product.Price = 3;
            _product.Quantity = 100000000;

            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(_product, _files));
        }

        [Test]
        public void SaveNewProductWithoutImages()
        {
            Product newProduct = new Product()
            {
                Id = 0,
                Title = "Laptop",
                Description = "New Brand Laptop",
                Price = 2000,
                Quantity = 2
            };

            IList<FileStream> productImages = new List<FileStream>();
            _productService.AddOrUpdateProduct(newProduct, productImages);

            _mockProductRepository.Verify();
        
        }

        [Test]
        public void UpdateTitleProduct()
        {
            int productId = 110;
            string newTitle = "Test2";
            Product updatedProduct = new Product
            {
                Id = productId,
                Title = newTitle,
                Description = string.Empty,
                Price = 100,
                Quantity = 0,
                ProductImages = new List<ProductImage>()
            };

            _mockProductRepository.Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(new Product { Id = productId, Title = "Test", Price = 100 });

            _productService.AddOrUpdateProduct(updatedProduct, _files);

            Product searchedProduct = _productService.GetProductById(productId);

            Assert.IsTrue(searchedProduct.Title == newTitle);
        }

        [TearDown]
        public void ClearConfiguration()
        {
            _product = null;
            _files = null;
            _productService = null;
        }
    }
}
