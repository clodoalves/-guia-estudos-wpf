using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.App.ViewModels.Contract;
using WPFSample.Service.Contract;

namespace WPFSample.App.ViewModels.Implementation
{
    public class ProductDetailsWindowViewModel : BindableBase, IProductDetailsWindowViewModel, INavigationAware
    {
        private readonly IProductService _productService;

        public ProductDetailsWindowViewModel(IProductService productService)
        {
            this._productService = productService;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters != null) 
            {
            
            }
        }
    }
}
