using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;
using WPFSample.Service.Contract;

namespace WPFSample.App.ViewModels.Implementation
{
    public class ProductListItemViewModel : BindableBase
    {
        private readonly IProductService _productService;

        public ProductListItemViewModel(IProductService productService)
        {
            _productService = productService;
        }

        #region Properties
        public int Id { get; set; }

        private string _descricao;
        public string Title
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

        #endregion

        #region Delegate Commands
        private DelegateCommand _selectProduct;
        public DelegateCommand SelectProduct => _selectProduct ?? (_selectProduct = new DelegateCommand(ExecuteSelectProduct));
        private DelegateCommand _deleteProduct;
        public DelegateCommand DeleteProduct => _deleteProduct ?? (_deleteProduct = new DelegateCommand(ExecuteDeleteProduct));
        #endregion

        #region Private Methods
        private void ExecuteSelectProduct()
        {
            RegionManager.RequestNavigate("MainRegion", $"ProductFormWindow?parameter={Id}");
        }

        private void ExecuteDeleteProduct() 
        {
            _productService.DeleteProductAsync(Id);

            RegionManager.RequestNavigate("MainRegion", "ProductsWindow");

        }
        #endregion

        #region Navigation 

        IRegionManager RegionManager { get { return ServiceLocator.Current.GetInstance<IRegionManager>(); } }

        #endregion
    }
}