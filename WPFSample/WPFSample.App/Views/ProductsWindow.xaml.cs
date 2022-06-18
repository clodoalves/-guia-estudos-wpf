using System.Windows.Controls;
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
