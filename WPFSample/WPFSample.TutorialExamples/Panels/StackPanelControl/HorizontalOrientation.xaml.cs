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

namespace WPFSample.TutorialExamples.Panels.StackPanelControl
{
    /// <summary>
    /// Interaction logic for HorizontalOrientation.xaml
    /// </summary>
    public partial class HorizontalOrientation : Window
    {
        public HorizontalOrientation()
        {
            InitializeComponent();
        }

        private void BtnDefault_Click(object sender, RoutedEventArgs e)
        {
            FirstButton.VerticalAlignment = VerticalAlignment.Stretch;
            SecondButton.VerticalAlignment = VerticalAlignment.Stretch;
            ThirdButton.VerticalAlignment = VerticalAlignment.Stretch;
            FourthButton.VerticalAlignment = VerticalAlignment.Stretch;
            FifthButton.VerticalAlignment = VerticalAlignment.Stretch;
            SixthButton.VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void BtnAlignment_Click(object sender, RoutedEventArgs e)
        {
            FirstButton.VerticalAlignment = VerticalAlignment.Top;
            SecondButton.VerticalAlignment = VerticalAlignment.Center;
            ThirdButton.VerticalAlignment = VerticalAlignment.Bottom;
            FourthButton.VerticalAlignment = VerticalAlignment.Bottom;
            FifthButton.VerticalAlignment = VerticalAlignment.Center;
            SixthButton.VerticalAlignment = VerticalAlignment.Top;
        }
    }
}
