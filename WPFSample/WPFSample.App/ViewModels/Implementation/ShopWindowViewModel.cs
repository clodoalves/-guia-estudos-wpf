using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.App.ViewModels.Contract;
using WPFSample.Domain;
using WPFSample.Service.Contract;

namespace WPFSample.App.ViewModels.Implementation
{
    public class ShopWindowViewModel: BindableBase, INavigationAware, IShopWindowViewModel
    {
        private readonly IProductService _productService;

        public ShopWindowViewModel(IProductService productService)
        {
            this._productService = productService;
        }

        #region Properties
        private ObservableCollection<ShopItemViewModel> _itemsShop;
        public ObservableCollection<ShopItemViewModel> ItemsShop
        {
            get { return _itemsShop; }
            set { SetProperty(ref _itemsShop, value); }
        }
        #endregion

        #region Navegation 

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            IList<Product> products = await _productService.GetAllProducts();

            ItemsShop =  BindToShopItemViewModel(products);
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        #endregion

        #region Private Methods

        private ObservableCollection<ShopItemViewModel> BindToShopItemViewModel(IList<Product> products)
        {
            var shopItems = products.Select(p => new ShopItemViewModel()
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                PathImage = _productService.GetPathFirstImage(p.Id).Result
            }).ToList();

            return new ObservableCollection<ShopItemViewModel>(shopItems);
        }


        #endregion
    }
}
