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
using WPFSample.App.ViewModels.Contract;

namespace WPFSample.App.Views
{
    /// <summary>
    /// Lógica interna para ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : UserControl
    {
        public ProductsWindow(IProductListWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
