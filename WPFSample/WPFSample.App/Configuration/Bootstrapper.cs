using Prism.Unity;
using Microsoft.Practices.Unity;
using System.Windows;
using WPFSample.App.Views;
using WPFSample.Service.Contract;
using WPFSample.Service.Implementation;
using WPFSample.App.ViewModels.Contract;
using WPFSample.App.ViewModels.Implementation;
using WPFSample.Repository.Contract;
using WPFSample.Repository.Implementation;

namespace WPFSample.App.Configuration
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow.Show();
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<AppShell>();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            //Navigation
            Container.RegisterTypeForNavigation<ProductFormWindow>("ProductFormWindow");
            Container.RegisterTypeForNavigation<ProductsWindow>("ProductsWindow");
            Container.RegisterTypeForNavigation<ShopWindow>("ShopWindow");
            Container.RegisterTypeForNavigation<ProductDetailsWindow>("ProductDetailsWindow");

            //View Models
            Container.RegisterType<IProductFormWindowViewModel, ProductFormWindowViewModel>();
            Container.RegisterType<IProductListWindowViewModel, ProductListWindowViewModel>();
            Container.RegisterType<IShopWindowViewModel, ShopWindowViewModel>();
            Container.RegisterType<IProductDetailsWindowViewModel, ProductDetailsWindowViewModel>();

            //Repositories
            Container.RegisterType<IProductRepository, ProductRepository>();
            Container.RegisterType<IProductImageRepository, ProductImageRepository>();

            //Services
            Container.RegisterType<IProductService, ProductService>();
        }       
    }
}