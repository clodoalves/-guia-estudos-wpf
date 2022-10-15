using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WPFSample.App.ViewModels.Implementation;

namespace WPFSample.App.Converters
{
    public class ControlVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility buttonVisibility = Visibility.Collapsed;

            IList<ProductImageViewModel> images = value as IList<ProductImageViewModel>;

            if (images != null && images.Any())
                buttonVisibility = Visibility.Visible;

            return buttonVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
