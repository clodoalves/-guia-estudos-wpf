using Microsoft.Practices.Unity;
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
using WPFSample.App.ViewModels.Contract;
using WPFSample.App.ViewModels.Implementation;

namespace WPFSample.App.Views
{
    /// <summary>
    /// Interaction logic for ProductFormWindow.xaml
    /// </summary>
    public partial class ProductFormWindow : UserControl
    {
        public ProductFormWindow(IProductFormWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public void checkImage_Unchecked(object sender, RoutedEventArgs e)
        {
            var productImages = products.ItemsSource.Cast<ProductImageViewModel>();

            if (productImages != null && productImages.Any(i => !i.CheckedToDelete)) 
            {
                checkAllImages.IsChecked = false;
            }
        }

        private void checkImage_Checked(object sender, RoutedEventArgs e)
        {
            var productImages = products.ItemsSource.Cast<ProductImageViewModel>();

            if (productImages != null && productImages.All(i => i.CheckedToDelete))
            {
                checkAllImages.IsChecked = true;
            }
        }
    }
}
