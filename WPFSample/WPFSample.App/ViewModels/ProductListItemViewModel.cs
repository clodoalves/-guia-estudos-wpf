using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using WPFSample.Service.Contract;

namespace WPFSample.App.ViewModels
{
    public class ProductListItemViewModel : BindableBase
    {
        private readonly IProductService productService;

        public ProductListItemViewModel(IProductService productService)
        {
            this.productService = productService;
        }
        
        public int Code { get; set; }
        private string _descricao;

        public string Description
        {
            get { return _descricao; }
            set { SetProperty(ref _descricao, value); }
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
    }
}