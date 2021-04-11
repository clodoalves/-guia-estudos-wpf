
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WPFSample.TutorialExamples.BasicControls
{
    /// <summary>
    /// Interaction logic for TextBlock.xaml
    /// </summary>
    public partial class TextBlock : Window
    {
        public TextBlock()
        {
            InitializeComponent();        
            TextBlockCodeBehind.TextWrapping = TextWrapping.Wrap;
            TextBlockCodeBehind.Margin = new Thickness(10);
            TextBlockCodeBehind.Inlines.Add("An example on ");
            TextBlockCodeBehind.Inlines.Add(new Run("the TextBlock control ") { FontWeight = FontWeights.Bold });
            TextBlockCodeBehind.Inlines.Add("using ");
            TextBlockCodeBehind.Inlines.Add(new Run("inline ") { FontStyle = FontStyles.Italic });
            TextBlockCodeBehind.Inlines.Add(new Run("text formatting ") { Foreground = Brushes.Blue });
            TextBlockCodeBehind.Inlines.Add("from ");
            TextBlockCodeBehind.Inlines.Add(new Run("Code-Behind") { TextDecorations = TextDecorations.Underline });
            TextBlockCodeBehind.Inlines.Add(".");
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }
    }
}
