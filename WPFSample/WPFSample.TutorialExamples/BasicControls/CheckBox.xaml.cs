using System.Windows;

namespace WPFSample.TutorialExamples.BasicControls
{
    /// <summary>
    /// Interaction logic for CheckBox.xaml
    /// </summary>
    public partial class CheckBox : Window
    {
        public CheckBox()
        {
            InitializeComponent();
        }

        private void cbAllFeatures_CheckedChanged(object sender, RoutedEventArgs e)
        {
            bool currentVal = (cbAllFeatures.IsChecked.Value == true);
            cbFeatureAbc.IsChecked = currentVal;
            cbFeatureXyz.IsChecked = currentVal;
            cbFeatureWww.IsChecked = currentVal;
        }

        private void cbFeature_CheckedChanged(object sender, RoutedEventArgs e)
        {
            cbAllFeatures.IsChecked = null;

			if((cbFeatureAbc.IsChecked == true) && (cbFeatureXyz.IsChecked == true) && (cbFeatureWww.IsChecked == true))
            {
                cbAllFeatures.IsChecked = true;
            }
            if ((cbFeatureAbc.IsChecked == false) && (cbFeatureXyz.IsChecked == false) && (cbFeatureWww.IsChecked == false))
            {
                cbAllFeatures.IsChecked = false;
            }
        }
    }
}
