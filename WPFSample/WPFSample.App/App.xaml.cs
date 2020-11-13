using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFSample.App.ViewModels;
using WPFSample.App.ViewModels.Contract;
using WPFSample.App.ViewModels.Implementation;
using WPFSample.App.Views;
using WPFSample.Repository.Context;
using WPFSample.Repository.Contract;
using WPFSample.Repository.Implementation;
using WPFSample.Service.Contract;
using WPFSample.Service.Implementation;

namespace WPFSample.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IUnityContainer container = new UnityContainer();

            //criacao de banco de dados
            using (var db = new WPFSampleDb())
            {
                await db.Database.EnsureCreatedAsync();
            }

            //View Models
            container.RegisterType<IProductFormWindowViewModel, ProductFormWindowViewModel>();
            container.RegisterType<IProductListWindowViewModel, ProductListWindowViewModel>();

            //Repositories
            container.RegisterType<IProductRepository, ProductRepository>();

            //Services
            container.RegisterType<IProductService, ProductService>();

            var inicialWindow = container.Resolve<ProductFormWindow>();

            inicialWindow.Show();
        }
    }
}
