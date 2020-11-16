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
            ViewEditProductCommand = new DelegateCommand(ViewEditProductAction);
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
        private void ViewEditProductAction() 
        {
            RegionManager.RequestNavigate("MainRegion", "ProductsWindow");
        }
        public DelegateCommand ViewProductFormCommand { get; }
        public DelegateCommand ViewProductsCommand { get; }
        public DelegateCommand ViewEditProductCommand { get; }
        IRegionManager RegionManager { get { return ServiceLocator.Current.GetInstance<IRegionManager>(); } }
    }
}