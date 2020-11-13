using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.App.ViewModels.Contract;
using WPFSample.Domain;
using WPFSample.Service.Contract;

namespace WPFSample.App.ViewModels.Implementation
{
    public class ProductFormWindowViewModel : BindableBase, IProductFormWindowViewModel
    {
        private readonly IProductService _productService;

        public ProductFormWindowViewModel()
        {

        }

        public ProductFormWindowViewModel(IProductService productService)
        {
            _productService = productService;
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

        private DelegateCommand _addProduct;

        public DelegateCommand AddProduct => _addProduct ?? (_addProduct = new DelegateCommand(ExecuteAddProduct));

        private async void ExecuteAddProduct()
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
