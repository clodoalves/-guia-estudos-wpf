using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.App.Common;

namespace WPFSample.App.ViewModels.Implementation
{
    public class ShopItemViewModel : BindableBase, INavigationAware
    {
        #region Properties

        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private double _price;
        public double Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

        private string _pathImage;
        public string PathImage
        {
            get { return _pathImage; }
            set { SetProperty(ref _pathImage, value); }
        }

        #endregion


        #region Delegate Commands 

        public DelegateCommand DetailsProductCommand => _detailsProductCommand ?? (_detailsProductCommand = new DelegateCommand(ExecuteDetailsCommand));

        private DelegateCommand _detailsProductCommand;

        private void ExecuteDetailsCommand() 
        {
            RegionManager.RequestNavigate("MainRegion", $"{PageTokens.PRODUCT_DETAILS_WINDOW}?parameter={Id}");
        }

        #endregion

        #region Navigation
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
        IRegionManager RegionManager { get { return ServiceLocator.Current.GetInstance<IRegionManager>(); } }
        #endregion
    }
}
