using Prism.Mvvm;
using Prism.Regions;
using System.Linq;
using WPFSample.App.ViewModels.Contract;
using WPFSample.Domain;
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

        #region Properties

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private double _price;
        public double Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

        private string _image;
        public string Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        private int _rating;
        public int Rating
        {
            get { return _rating; }
            set { SetProperty(ref _rating, value); }
        }
      
        #endregion

        #region Navigation 

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.Any())
            {
                int id = int.Parse(navigationContext.Parameters.First().Value.ToString());

                var product = _productService.GetProductById(id);

                BindToViewModel(product);
            }
        }

        #endregion

        private void BindToViewModel(Product product)
        {
            Title = product.Title;
            Description = product.Description;
            Price = product.Price;
            Image = _productService.GetPathFirstImage(product.Id);
            Rating = 3;
        }
    }
}
