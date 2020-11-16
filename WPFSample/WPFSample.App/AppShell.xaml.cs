using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFSample.App.ViewModels;

namespace WPFSample.App
{
    /// <summary>
    /// Lógica interna para AppShell.xaml
    /// </summary>
    public partial class AppShell : Window
    {

        public AppShell(IRegionManager regionManager)
        {
            InitializeComponent();
            DataContext = new AppShellViewModel();
        }
    }
}
