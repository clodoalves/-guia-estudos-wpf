using System.Windows;
using System.Windows.Controls;

namespace WPFSample.TutorialExamples.Panels.StackPanelControl
{
    /// <summary>
    /// Interaction logic for VerticalOrientation.xaml
    /// </summary>
    public partial class VerticalOrientation : Page
    {
        public VerticalOrientation()
        {
            InitializeComponent();
        }

        private void BtnDefault_Click(object sender, RoutedEventArgs e)
        {
            FirstButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            SecondButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            ThirdButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            FourthButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            FifthButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            SixthButton.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        private void BtnAlignment_Click(object sender, RoutedEventArgs e)
        {
            FirstButton.HorizontalAlignment = HorizontalAlignment.Left;
            SecondButton.HorizontalAlignment = HorizontalAlignment.Center;
            ThirdButton.HorizontalAlignment = HorizontalAlignment.Right;
            FourthButton.HorizontalAlignment = HorizontalAlignment.Right;
            FifthButton.HorizontalAlignment = HorizontalAlignment.Center;
            SixthButton.HorizontalAlignment = HorizontalAlignment.Left;
        }
    }
}