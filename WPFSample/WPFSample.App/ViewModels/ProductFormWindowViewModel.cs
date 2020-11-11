using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.Domain;
using WPFSample.Service.Contract;

namespace WPFSample.App.ViewModels
{
    public class ProductFormWindowViewModel : BindableBase
    {
        private readonly IProductService _productService;

        public ProductFormWindowViewModel()
        {

        }

        public ProductFormWindowViewModel(IProductService productService)
        {
            this._productService = productService;
        }

        public int Code { get; set; }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }

        private double _price;
        public double Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }
    
        public async void AddProduct()
        {
            Product product = BindProductObject();

           await _productService.AddProductAsync(product);
        }

        private Product BindProductObject()
        {
            return new Product()
            {
                Description = Description,
                Price = Price,
                Quantity = Quantity
            };
        }
    }
}
