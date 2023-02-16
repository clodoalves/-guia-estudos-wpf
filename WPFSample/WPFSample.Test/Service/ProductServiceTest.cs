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

            _productService = new ProductService(_mockProductRepository.Object, _mockProductImageRepository.Object);
        }

        [Test]
        public void TestAddProductWithoutTitle()
        {
            //arrange
            Product product = new Product()
            {
                Description = "This is a nice smartphone",
                Title = string.Empty,
                Price = 1000,
                Quantity = 1
            };

            //act
            TestDelegate AddOrUpdateProductDelegate = () => _productService.AddOrUpdateProduct(product);

            //assert
            Assert.Throws(typeof(RequiredFieldException), AddOrUpdateProductDelegate);
        }

        [Test]
        public void TestAddProductWithTitleThatExceedsLimitCharacters()
        {
            //arrange
            Product product = new Product()
            {
                Description = "This is a nice smartphone",
                Title = "Product with loooooooooooooooooooooooooooooooooooooooooooooooooog title",
                Price = 1000,
                Quantity = 1
            };

            //act
            TestDelegate AddOrUpdateProductDelegate = () => _productService.AddOrUpdateProduct(product);

            //assert
            Assert.Throws(typeof(FieldExceedCaracterLimitException), AddOrUpdateProductDelegate);
        }

        [Test]
        public void TestAddProductWithDescriptionThatExceedsLimitCharacters()
        {
            //arrange
            Product product = new Product()
            {
                Description = "Product with loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong description",
                Title = "Nice product",
                Price = 1000,
                Quantity = 1
            };

            //act
            TestDelegate AddOrUpdateProductDelegate = () => _productService.AddOrUpdateProduct(product);

            //assert
            Assert.Throws(typeof(FieldExceedCaracterLimitException), AddOrUpdateProductDelegate);
        }

        [Test]
        public void TestAddProductWithQuantityLessThanZero()
        {
            Product product = new Product()
            {
                Description = "This is a nice smartphone",
                Title = "Nice smartphone",
                Price = 1000,
                Quantity = -1
            };

            Assert.Throws(typeof(NumericFieldLessThanZeroException), () => _productService.AddOrUpdateProduct(product));
        }

        [Test]
        public void TestAddProductWithQuantityThatExceedsLimitNumbers()
        {
            Product product = new Product()
            {
                Description = "This is a nice smartphone",
                Title = "Nice smartphone",
                Price = 1000,
                Quantity = 100000000
            };

            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(product));
        }


        [Test]
        public void TestAddProductWithoutPrice()
        {
            Product product = new Product()
            {
                Description = "This is a nice smartphone",
                Title = "Nice smartphone",
                Price = 0,
                Quantity = 1
            };

            Assert.Throws(typeof(RequiredFieldException), () => _productService.AddOrUpdateProduct(product));
        }

        [Test]
        public void TestAddProductWithPriceLessThanZero()
        {
            Product product = new Product()
            {
                Description = "This is a nice smartphone",
                Title = "Nice smartphone",
                Price = -1,
                Quantity = 1
            };

            Assert.Throws(typeof(NumericFieldLessThanZeroException), () => _productService.AddOrUpdateProduct(product));
        }

        [Test]
        public void TestAddProductWithPriceThatExceedsLimitNumbers()
        {
            Product product = new Product()
            {
                Description = "This is a nice smartphone",
                Title = "Nice product",
                Price = 100000000000,
                Quantity = 1
            };

            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddOrUpdateProduct(product));
        }

        [Test]
        public void TestSaveNewProducWithOnlyRequiredFields()
        {
            //arrange and act
            Product productAdded = _productService.AddOrUpdateProduct(new Product() { Title = "New product", Price = 1000 });

            //assert
            Assert.IsTrue(productAdded != null);
        }


        public void TestSaveNewProductWithAllFields()
        {
            //arrange and act        
            Product productAdded = _productService.AddOrUpdateProduct(new Product() { Title = "New product", Description = "It's a new product", Price = 1000, Quantity = 2 });

            //assert
            Assert.IsTrue(productAdded != null);
        }

        [Test]
        public void TestSaveNewProductWithOneImage()
        {
            //arrange
            Product product = new Product()
            {
                Title = "New Product",
                Price = 1000,
                ProductImages = new List<ProductImage>()
                {
                    new ProductImage() { Path = @"D:\FakeFolder\Image1.jpg" },
                }
            };

            Product productAdded = _productService.AddOrUpdateProduct(product);

            //assert
            Assert.IsTrue(productAdded != null);
        }

        [Test]
        public void TestSaveNewProductWithMultipleImages()
        {
            //arrange
            Product product = new Product()
            {
                Title = "New Product",
                Price = 1000,
                ProductImages = new List<ProductImage>()
                {
                    new ProductImage() { Path = @"D:\FakeFolder\Image1.jpg" },
                    new ProductImage() { Path = @"D:\FakeFolder\Image2.jpg" }
                }
            };

            Product productAdded = _productService.AddOrUpdateProduct(product);

            //assert
            Assert.IsTrue(productAdded != null);
        }


        [Test]
        public void TestUpdateTitleProduct()
        {

            //arrange
            int productId = 110;
            string newTitle = "New brand smartphone";
            _mockProductRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(new Product() { Id = productId, Title = "Old Smartphone", Price = 1000 });

            //Act
            Product product = _productService.GetProductById(productId);
            product.Title = newTitle;
            Product upToDateProduct = _productService.AddOrUpdateProduct(product);

            //Assert
            Assert.AreEqual(newTitle, upToDateProduct.Title);
        }

        [Test]
        public void TestUpdateDescriptionProduct()
        {
            //Arrange
            int productId = 1;
            string newDescription = "This is a new brand device";
            _mockProductRepository.Setup(p => p.GetById(It.IsAny<int>())).Returns(new Product() { Id = productId, Title = "Good product", Description = "Old description", Price = 100 });

            //Act
            Product product = _productService.GetProductById(productId);
            product.Description = newDescription;
            Product upToDateProduct = _productService.AddOrUpdateProduct(product);

            //Assert
            Assert.AreEqual(newDescription, upToDateProduct.Description);
        }

        [Test]
        public void TestUpdatePriceProduct()
        {
            //Arrange
            int productId = 2;
            int newPrice = 1000;
            _mockProductRepository.Setup(p => p.GetById(It.IsAny<int>())).Returns(new Product() { Id = productId, Title = "New brand smartphone", Price = 2000 });

            //Act
            Product product = _productService.GetProductById(productId);
            product.Price = newPrice;
            Product upToDateProduct = _productService.AddOrUpdateProduct(product);

            //Assert
            Assert.AreEqual(newPrice, upToDateProduct.Price);
        }

        [Test]
        public void TestUpdateQuantityProduct()
        {
            //arrange
            int productId = 2;
            int newQuantity = 100;
            _mockProductRepository.Setup(p => p.GetById(It.IsAny<int>())).Returns(new Product() { Id = productId, Title = "Good Product", Price = 1000, Quantity = 5 });

            //act
            Product product = _productService.GetProductById(productId);
            product.Quantity = newQuantity;

            Product upToDateProduct = _productService.AddOrUpdateProduct(product);

            //assert
            Assert.AreEqual(newQuantity, upToDateProduct.Quantity);
        }

        [Test]
        public void TestDeleteProductNonExisting() 
        {
            //arrange
            int productId = 4;

            //act 
            TestDelegate deleteProductDelegate = () => _productService.DeleteProduct(productId);

            //assert
            Assert.Throws(typeof(RegisterNotExistsException), deleteProductDelegate);
        }

        [Test]
        public void TestDeleteProduct()
        {
            //arrange
            int productId = 4;    
            _mockProductRepository.Setup(p => p.GetById(It.IsAny<int>())).Returns(new Product() { Id = productId, Title = "Product 4", Price = 1000 });

            //act 
            TestDelegate deleteProductDelegate = () => _productService.DeleteProduct(productId);

            //assert
            Assert.DoesNotThrow(deleteProductDelegate);
        }

        [Test]
        public void TestUpdateAllFieldsProduct()
        {
            //arrange
            string newTitle = "Smartphone";
            string newDescription = "Good Product";
            int newPrice = 1000;
            int newQuantity = 5;

            _mockProductRepository.Setup(p => p.GetById(It.IsAny<int>()))
                .Returns(
                new Product()
                {
                    Id = 123,
                    Title = "Title",
                    Description = "Description",
                    Price = 100,
                    Quantity = 1
                });

            //act
            Product originalProduct = _productService.GetProductById(123);
            originalProduct.Title = newTitle;
            originalProduct.Description = newDescription;
            originalProduct.Price = newPrice;
            originalProduct.Quantity = newQuantity;

            Product upToDateProduct = _productService.AddOrUpdateProduct(originalProduct);

            //asset
            Assert.AreEqual(newTitle, upToDateProduct.Title);
            Assert.AreEqual(newDescription, upToDateProduct.Description);
            Assert.AreEqual(newQuantity, upToDateProduct.Quantity);
            Assert.AreEqual(newPrice, upToDateProduct.Price);
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
