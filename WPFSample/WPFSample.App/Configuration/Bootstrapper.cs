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
using WPFSample.App.Quartz;

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
            Container.RegisterType(typeof(IProductFormWindowViewModel), typeof(ProductFormWindowViewModel));
            Container.RegisterType(typeof(IProductListWindowViewModel), typeof(ProductListWindowViewModel));
            Container.RegisterType(typeof(IShopWindowViewModel), typeof(ShopWindowViewModel));
            Container.RegisterType(typeof(IProductDetailsWindowViewModel), typeof(ProductDetailsWindowViewModel));

            //Repositories
            Container.RegisterType(typeof(IProductRepository), typeof(ProductRepository));
            Container.RegisterType(typeof(IProductImageRepository), typeof(ProductImageRepository));

            //Services
            Container.RegisterType(typeof(IProductService), typeof(ProductService));

            StarterJob starter = new StarterJob(Container);

            starter.UpdateQuantityProductsTrigger();
            starter.UnlockWindowsTrigger();
            starter.TimeoutTrigger();
            starter.DarkModeChangeTrigger();
        }
    }
}