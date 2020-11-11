using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace WPFSample.App.ViewModels
{
    public class ProductListPageViewModel : BindableBase
    {
        private ObservableCollection<ProductListItemViewModel> _productListItems { get; set; }

        public ObservableCollection<ProductListItemViewModel> ProductListItems
        {
            get { return _productListItems; }
            set
            {
                if (_productListItems != value)
                {
                    _productListItems = value;

                    //OnPropertyChanged("ProductListItems");
                }
            }
        }

        public ProductListPageViewModel(NavigationService navigation)
        {
      
        }
    }
}
