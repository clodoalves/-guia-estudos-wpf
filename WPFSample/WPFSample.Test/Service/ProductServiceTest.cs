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
            _product.Price = 100;
            _product.Quantity = 1;
            _product.Id = 1;
            _productService.AddProduct(_product, _files);
        }

        [TearDown]
        public void ClearConfiguration()
        {
            _product = null;
        }
    }
}
