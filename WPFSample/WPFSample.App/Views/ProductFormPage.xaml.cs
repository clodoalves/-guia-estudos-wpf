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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFSample.App.ViewModels;

namespace WPFSample.App.Views
{
    /// <summary>
    /// Interaction logic for ProductForm.xaml
    /// </summary>
    public partial class ProductFormPage : Page
    {
        public ProductFormPageViewModel ViewModel => (ProductFormPageViewModel) DataContext;

        public ProductFormPage()
        {
            InitializeComponent();
        }
    }
}
