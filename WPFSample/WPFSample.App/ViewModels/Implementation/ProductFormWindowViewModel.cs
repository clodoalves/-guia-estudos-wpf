using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPFSample.App.ViewModels.Contract;
using WPFSample.Domain;
using WPFSample.Service.Contract;


namespace WPFSample.App.ViewModels.Implementation
{
    public class ProductFormWindowViewModel : BindableBase, INavigationAware, IProductFormWindowViewModel
    {
        private readonly IProductService _productService;

        public ProductFormWindowViewModel(IProductService productService)
        {
            _productService = productService;
        }

        public int Id { get; set; }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
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

        private ObservableCollection<FileStream> images;
        public ObservableCollection<FileStream> Images
        {
            get { return images; }
            set { SetProperty(ref images, value); }
        }

        private DelegateCommand _addProduct;

        public DelegateCommand AddProduct => _addProduct ?? (_addProduct = new DelegateCommand(ExecuteUpdateOrAddProduct));
                                        
        private async void ExecuteUpdateOrAddProduct()
        {
            if (Id != 0)
            {
                Product product = await _productService.GetProductById(Id);

                product.Description = Description;
                product.Price = Price;
                product.Quantity = Quantity;

                await _productService.UpdateProductAsync(product);
            }
            else 
            {
                Product product = BindToProductObject();
                await _productService.AddProductAsync(product, Images);
            }

            RegionManager.RequestNavigate("MainRegion", "ProductsWindow");
        }

        private DelegateCommand _addImages;

        public DelegateCommand AddImage => _addImages ?? (_addImages = new DelegateCommand(ExecuteAddImages));

        private void ExecuteAddImages() 
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            //openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog().GetValueOrDefault()) 
            {
                foreach (string fileName in openFileDialog.FileNames)
                {
                    FileStream filestream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                    images.Add(filestream);       
                }
            }
        }

        private Product BindToProductObject()
        {
            return new Product()
            {
                Description = Description,
                Price = Price,
                Quantity = Quantity
            };
        }
        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.Any())
            {
                int id = int.Parse(navigationContext.Parameters.First().Value.ToString());

                var product = await _productService.GetProductById(id);

                BindToViewModel(product);
            }
            else
            {
                ClearViewModel();
            }

            Images = new ObservableCollection<FileStream>();
        }

        private void ClearViewModel()
        {
            Id = 0;
            Description = string.Empty;
            Price = 0;
            Quantity = 0;
        }
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        private void BindToViewModel(Product product) 
        {
            Id = product.Id;
            Description = product.Description;
            Price = product.Price;
            Quantity = product.Quantity;
        }

        IRegionManager RegionManager { get { return ServiceLocator.Current.GetInstance<IRegionManager>(); } }
    }
}
