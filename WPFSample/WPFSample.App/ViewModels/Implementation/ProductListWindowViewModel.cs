using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using WPFSample.App.ViewModels.Contract;
using WPFSample.Domain;
using WPFSample.Service.Contract;

namespace WPFSample.App.ViewModels.Implementation
{
    public class ProductListWindowViewModel : BindableBase, INavigationAware, IProductListWindowViewModel
    {
        private readonly IProductService _productService;

        private ObservableCollection<ProductListItemViewModel> _items;

        public ObservableCollection<ProductListItemViewModel> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public ProductListWindowViewModel(IProductService productService)
        {
            _productService = productService;
        }

        private IList<ProductListItemViewModel> BindToProductListItemViewModel(IList<Product> products) 
        {
            return products.Select(x => new ProductListItemViewModel(_productService)
            {
                Id = x.Id,                
                Title = x.Title,
                Price = x.Price
            }).ToList();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var list = _productService.GetAllProducts().Result;

            var items = BindToProductListItemViewModel(list);

            Items = new ObservableCollection<ProductListItemViewModel>(items);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}