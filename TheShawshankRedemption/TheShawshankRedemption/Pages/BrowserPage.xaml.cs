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
using System.IO;
using System.Xml.Serialization;
namespace TheShawshankRedemption.Pages
{
    /// <summary>
    /// Interaction logic for BrowserPage.xaml
    /// </summary>
    public partial class BrowserPage : UserControl
    {

        public BrowserPage()
        {
            InitializeComponent();

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
        }
        private void Front_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StackPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Browser.Height = e.NewSize.Height - MenuPanel.RenderSize.Height;
        }

        private void URL_KeyDown(object sender, KeyEventArgs e)
        {
            // Focusされていなければ無視
            if (!URLBox.IsFocused)
                return;
            // EnterKeyでなければ無視
            if (e.Key != Key.Return)
                return;
            // 現在と同じページであれば無視
            if (Browser.Source.ToString() == URLBox.Text)
                return;

            Browser.Source = new Uri(URLBox.Text);
            UpdateHistory(URLBox.Text);
        }
        private void UpdateHistory(string URL)
        {

        }

    }
}
