using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.Domain;
using WPFSample.Repository.Contract;
using WPFSample.Repository.Implementation;
using WPFSample.Service.Contract;
using WPFSample.Service.Implementation;

namespace WPFSample.Test.Service
{
    [TestFixture]
    public class ProductServiceTest
    {
        private Product _product;
        IList<FileStream> _filesWindow;
       private IProductService _productService;

       [SetUp]
        public void ConfigureDependecies() 
        {
            _product = new Product();
            _filesWindow = new List<FileStream>();
            _productService = new ProductService();
        }

        public void AddProductWithoutTitleTest()
        {
            _product.Title = string.Empty;
            _product.Price = 100;
            _product.Quantity = 1;
            _product.Id = 1;
            _productService.AddProductAsync(_product, _filesWindow);
        }

        public void ClearConfiguration() 
        {
            _product = null;
        }
    }
}
