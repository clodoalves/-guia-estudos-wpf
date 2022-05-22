using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using WPFSample.Domain;
using WPFSample.Service.Contract;
using WPFSample.Service.Implementation;
using WPFSample.Service.Exceptions.Product;

namespace WPFSample.Test.Service
{
    [TestFixture]
    public class ProductServiceTest
    {
        private Product _product;
        IList<FileStream> _files;
        private IProductService _productService;

        [SetUp]
        public void ConfigureDependecies()
        {
            _product = new Product();
            _files = new List<FileStream>();
            _productService = new ProductService();
        }

        [Test]
        public void AddProductWithoutTitleTest()
        {
            _product.Title = string.Empty;
            _product.Description = "New product";
            _product.Price = 100;
            _product.Quantity = 1;
            
            Assert.Throws(typeof(RequiredFieldException), () => _productService.AddProduct(_product, _files));
        }

        [Test]
        public void AddProductWithoutDescriptionTest() 
        {
            _product.Title = "New laptop";
            _product.Description = string.Empty;
            _product.Price = 100;
            _product.Quantity = 1;

            Assert.Throws(typeof(RequiredFieldException), () => _productService.AddProduct(_product, _files));
        }

        [Test]
        public void AddProductWithoutQuantity() 
        {
            _product.Title = "New laptop";
            _product.Description = "New product";
            _product.Price = 100;
            _product.Quantity = 0;

            Assert.Throws(typeof(RequiredFieldException), () => _productService.AddProduct(_product, _files));
        }

        [Test]
        public void AddProductWithoutPrice() 
        {
            _product.Title = "New laptop";
            _product.Description = "New product";
            _product.Price = 0;
            _product.Quantity = 10;

            Assert.Throws(typeof(RequiredFieldException), () => _productService.AddProduct(_product, _files));
        }

        [Test]
        public void AddProductWithPriceLessThanZero() 
        {
            _product.Title = "New laptop";
            _product.Description = "New product";
            _product.Price = -2;
            _product.Quantity = 10;

            Assert.Throws(typeof(NumericFieldLessThanZeroException), () => _productService.AddProduct(_product, _files));
        }

        [Test]
        public void AddProductWithQuantityLessThanZero() 
        {
            _product.Title = "New laptop";
            _product.Description = "New product";
            _product.Price = 3;
            _product.Quantity = -5;

            Assert.Throws(typeof(NumericFieldLessThanZeroException), () => _productService.AddProduct(_product, _files));
        }

        [Test]
        public void AddProductWithTitleThatExceedsLimitCharacters() 
        {
            _product.Title = "Product with loooooooooooooooooooooooooooooooooooooooooooooooooog title";
            _product.Description = "New product";
            _product.Price = 3;
            _product.Quantity = 1;

            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddProduct(_product, _files));
        }

        [Test]
        public void AddProductWithDescriptionThatExceedsLimitCharacters()
        { 
            _product.Title = "New laptop";
            _product.Description = "Product with loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong description";
            _product.Price = 3;
            _product.Quantity = 1;

            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddProduct(_product, _files));
        }

        [Test]
        public void AddProductWithPriceThatExceedsLimitNumbers()
        {
            _product.Title = "New laptop";
            _product.Description = "New product";
            _product.Price = 100000000000;
            _product.Quantity = 1;

            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddProduct(_product, _files));
        }

        [Test]
        public void AddProductWithQuantityThatExceedsLimitNumbers() 
        {
            _product.Title = "New laptop";
            _product.Description = "New product";
            _product.Price = 3;
            _product.Quantity = 100000000;

            Assert.Throws(typeof(FieldExceedCaracterLimitException), () => _productService.AddProduct(_product, _files));
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
