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

            _mockProduct.Title = "Good product";
            _mockProduct.Description = "New brand product";
            _mockProduct.Price = 100;
            _mockProduct.Quantity = 1;
        }

        [Test]
        public void AddProductWithoutTitleTest()
        {
            _mockProduct.Title = string.Empty;
            
            Assert.Throws(typeof(RequiredFieldException), () => _productService.AddOrUpdateProduct(_mockProduct, _files));
        }

        [Test]
        public void AddProductWithoutPrice()
        {
            _mockProduct.Price = 0;
           
            Assert.Throws(typeof(RequiredFieldException), () => _productService.AddOrUpdateProduct(_mockProduct, _files));
        }

        [Test]
        public void AddProductWithPriceLessThanZero()
        {
            _mockProduct.Price = -2;
         
            Assert.Throws(typeof(NumericFieldLessThanZeroException), () => _productService.AddOrUpdateProduct(_mockProduct, _files));
        }

        [Test]
        public void AddProductWithQuantityLessThanZero()
        {       
            _mockProduct.Quantity = -5;

            Assert.Throws(typeof(NumericFieldLessThanZeroException), () => _productService.AddOrUpdateProduct(_mockProduct, _files));
        }

        [Test]
        public void AddProductWithTitleThatExceedsLimitCharacters()
        {
            _mockProduct.Title = "Product with loooooooooooooooooooooooooooooooooooooooooooooooooog title";
            
            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(_mockProduct, _files));
        }

        [Test]
        public void AddProductWithDescriptionThatExceedsLimitCharacters()
        {         
            _mockProduct.Description = "Product with loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong description";
         
            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(_mockProduct, _files));
        }

        [Test]
        public void AddProductWithPriceThatExceedsLimitNumbers()
        {
            _mockProduct.Price = 100000000000;
            
            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(_mockProduct, _files));
        }

        [Test]
        public void AddProductWithQuantityThatExceedsLimitNumbers()
        {
            _mockProduct.Quantity = 100000000;

            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(_mockProduct, _files));
        }

        [Test]
        public void SaveNewProductWithoutImages()
        {
            int newProductId = 1;

            _mockProduct.Id = newProductId;
        
            _mockProductRepository.Setup(s => s.Add(It.IsAny<Product>())).Callback((Product p) => p = _mockProduct);
            _mockProductRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(_mockProduct);

            Product newProduct = new Product()
            {
                Title = "Good product",
                Description = "New brand product",
                Price = 100,
                Quantity = 1
            };
            
            IList<FileStream> productImages = new List<FileStream>();

            _productService.AddOrUpdateProduct(newProduct, productImages);

            Product savedProduct = _productService.GetProductById(newProductId);

            Assert.AreEqual(savedProduct.Id, newProductId);
        }

        [Test]
        public void UpdateTitleProduct()
        {
            int productId = 110;

            Product mockedProduct = new Product()
            {
                Id = productId,
                Title = "Old laptop",
                Description = "Good laptop",
                Price = 3000,
                Quantity = 1
            };

            _mockProductRepository.Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(mockedProduct);

            string newTitle = "New laptop";

            Product updatedProduct = mockedProduct;
            updatedProduct.Title = newTitle;

            _productService.AddOrUpdateProduct(updatedProduct, _files);

            Product databaseProduct = _productService.GetProductById(productId);

            Assert.IsTrue(databaseProduct.Title == newTitle);
        }

        [Test]
        public void UpdateDescriptionProduct()
        {
            int productId = 100;

            Product mockedProduct = new Product
            {
                Id = productId,
                Title = "New Smartphone",
                Description = "This is such a nice smartphone",
                Price = 1000,
                Quantity = 1
            };

            _mockProductRepository.Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(mockedProduct);

            Product updatedProduct = mockedProduct;
            string newDescription = "This is a pretty nice smartphone";
            updatedProduct.Description = newDescription;

            _productService.AddOrUpdateProduct(updatedProduct, _files);

            Product databaseProduct = _productService.GetProductById(productId);

            Assert.AreEqual(databaseProduct.Description, newDescription);
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
