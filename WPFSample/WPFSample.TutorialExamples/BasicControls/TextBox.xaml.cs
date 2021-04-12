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

namespace WPFSample.TutorialExamples.BasicControls
{
    /// <summary>
    /// Interaction logic for TextBox.xaml
    /// </summary>
    public partial class TextBox : Window
    {
        public TextBox()
        {
            InitializeComponent();
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {         
            txtStatus.Text   = $"Selection starts at character #{txtContent.SelectionStart} {Environment.NewLine}";
            txtStatus.Text += $"Selection is {txtContent.SelectionLength} characters(s) long {Environment.NewLine}";
            txtStatus.Text += $"Selected text: {txtContent.SelectedText}";
        }
    }
}
