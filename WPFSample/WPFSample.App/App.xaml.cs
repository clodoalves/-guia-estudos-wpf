using Microsoft.Practices.Unity;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPFSample.App.Configuration;
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

            await WPFSampleDbContext.GetInstance().Database.EnsureCreatedAsync();

            new Bootstrapper().Run();
        }
    }
}