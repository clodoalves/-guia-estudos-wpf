using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Regions;

namespace WPFSample.App.ViewModels
{
    public class AppShellViewModel
    {
        public AppShellViewModel()
        {
            ViewProductFormCommand = new DelegateCommand(ViewProductFormAction);
            ViewProductsCommand = new DelegateCommand(ViewProductsAction);
            ViewShopCommand = new DelegateCommand(ViewShopAction);
        }
        private void ViewProductFormAction()
        {
            RegionManager.RequestNavigate("MainRegion", "ProductFormWindow");
        }
        private void ViewProductsAction()
        {
            RegionManager.RequestNavigate("MainRegion", "ProductsWindow",
            (NavigationResult nr) =>
            {
                var error = nr.Error;
                var result = nr.Result;
            });
        }
        private void ViewShopAction() 
        {
            RegionManager.RequestNavigate("MainRegion", "ShopWindow");
        }
        public DelegateCommand ViewProductFormCommand { get; }
        public DelegateCommand ViewProductsCommand { get; }
        public DelegateCommand ViewShopCommand { get; }
        IRegionManager RegionManager { get { return ServiceLocator.Current.GetInstance<IRegionManager>(); } }
    }
}