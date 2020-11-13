using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using WPFSample.App.ViewModels.Contract;
using WPFSample.Service.Contract;

namespace WPFSample.App.ViewModels.Implementation
{
    public class ProductListWindowViewModel : BindableBase, IProductListWindowViewModel
    {
        private readonly IProductService _productService;

        private ObservableCollection<ProductListItemViewModel> _items { get; set; }

        public ObservableCollection<ProductListItemViewModel> Items
        {
            get { return _items; }
            set
            {
                if (_items != value)
                {
                    _items = value;

                    //OnPropertyChanged("ProductListItems");
                }
            }
        }

        public ProductListWindowViewModel(IProductService productService)
        {
            this._productService = productService;

            _items = new ObservableCollection<ProductListItemViewModel>();
        }
    }
}
