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
using WPFSample.App.ViewModels.Contract;
using WPFSample.Domain;
using WPFSample.Service.Contract;
using WPFSample.Service.Exceptions;

namespace WPFSample.App.ViewModels.Implementation
{
    public class ProductFormWindowViewModel : BindableBase, INavigationAware, IProductFormWindowViewModel
    {
        private readonly IProductService _productService;

        public ProductFormWindowViewModel(IProductService productService)
        {
            _productService = productService;
        }

        #region Properties
        public int Id { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                SetProperty(ref _title, value);

            }
        }

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

        private ObservableCollection<ProductImageViewModel> _images;
        public ObservableCollection<ProductImageViewModel> Images
        {
            get { return _images; }
            set
            {
                if (SetProperty(ref _images, value))
                {

                }
            }
        }

        private bool _allImagesChecked;

        public bool AllImagesChecked
        {
            get 
            {
                return _allImagesChecked; 
            }
            set 
            {
                SetProperty(ref _allImagesChecked, value);
            } 
        }

        #endregion

        #region Delegate Commands 
        public DelegateCommand AddProduct => _addProduct ?? (_addProduct = new DelegateCommand(ExecuteUpdateOrAddProduct));

        private Func<bool> CanAddProduct()
        {
            return new Func<bool>(() => !string.IsNullOrWhiteSpace(Title));
        }

        private DelegateCommand _addProduct;

        public DelegateCommand AddImage => _addImages ?? (_addImages = new DelegateCommand(ExecuteAddImages));

        private DelegateCommand _addImages;

        public DelegateCommand DeleteCheckedImages => _deleteCheckedProducts ?? (_deleteCheckedProducts = new DelegateCommand(ExecuteDeleteCheckedImages));

        private DelegateCommand _deleteCheckedProducts;

        public DelegateCommand CheckAllImages => _checkAllImages ?? (_deleteCheckedProducts = new DelegateCommand(ExecuteCheckAllImages));

        public DelegateCommand _checkAllImages; 

        #endregion

        #region Private methods
        private void ExecuteAddImages()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                foreach (string fileName in openFileDialog.FileNames)
                {
                    FileStream filestream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                    ProductImageViewModel productImageViewModel = new ProductImageViewModel(filestream);

                    Images.Add(productImageViewModel);
                }
            }

            RaisePropertyChanged("Images");
            AllImagesChecked = false;
        }

        private Product BindToProduct()
        {
            return new Product()
            {
                Id = Id,
                Title = Title,
                Description = Description,
                Price = Price,
                Quantity = Quantity
            };
        }

        private void ExecuteUpdateOrAddProduct()
        {
            try
            {
                Product product = BindToProduct();

                IList<FileStream> sources = Images.Select(i => i.Source).ToList();

                AddImagesToProduct(product, sources);

                _productService.AddOrUpdateProduct(product);

                RegionManager.RequestNavigate("MainRegion", "ProductsWindow");
            }
            catch (BusinessException ex)
            {

            }
            catch (DatabaseException ex)
            {

            }
            catch (Exception ex)
            {

            }
        }

        private void ExecuteDeleteCheckedImages()
        {
            foreach (ProductImageViewModel image in Images.Where(i => i.CheckedToDelete).ToList())
            {
                Images.Remove(image);
            }

            RaisePropertyChanged(nameof(Images));
        }

        private void ExecuteCheckAllImages()
        {
            if (AllImagesChecked)
            {
                Images.Select(i => { i.CheckedToDelete = true; return i; }).ToList();
            }
            else 
            {
                Images.Select(i => { i.CheckedToDelete = false; return i; }).ToList();
            }

            RaisePropertyChanged(nameof(Images));
        }

        private void ClearViewModel()
        {
            Id = 0;
            Title = string.Empty;
            Description = string.Empty;
            Price = 0;
            Quantity = 0;
        }

        private void BindToViewModel(Product product)
        {
            Id = product.Id;
            Title = product.Title;
            Description = product.Description;
            Price = product.Price;
            Quantity = product.Quantity;
        }

        private void AddImagesToProduct(Product product, IList<FileStream> filesWindow)
        {
            product.ProductImages = filesWindow.Select(f => new ProductImage()
            {
                Path = Path.GetFileName(f.Name)

            }).ToList();
        }


        #endregion

        #region Navigation

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.Any())
            {
                int id = int.Parse(navigationContext.Parameters.First().Value.ToString());

                var product = _productService.GetProductById(id);

                BindToViewModel(product);
            }
            else
            {
                ClearViewModel();
            }

            Images = new ObservableCollection<ProductImageViewModel>();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        IRegionManager RegionManager { get { return ServiceLocator.Current.GetInstance<IRegionManager>(); } }
        #endregion
    }
}
