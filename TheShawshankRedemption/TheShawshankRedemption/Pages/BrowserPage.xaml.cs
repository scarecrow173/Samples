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
        public static BrowserPage instance = null;
        public BrowserPage()
        {
            InitializeComponent();
            instance = this;
        }
        private void ButtonUpdate()
        {
            BackButton.IsEnabled = Browser.CanGoBack;
            ForwardButton.IsEnabled = Browser.CanGoForward;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Browser.CanGoBack)
                Browser.GoBack();
            ButtonUpdate();

        }
        private void Front_Click(object sender, RoutedEventArgs e)
        {
            if (Browser.CanGoForward)
                Browser.GoForward();
            ButtonUpdate();
        }

        private void StackPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Browser.Height = e.NewSize.Height - MenuPanel.RenderSize.Height;
        }

        // URL入力用TextBox上でEnterKeyの押下を判定
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
        }

        private void Browser_Initialized(object sender, EventArgs e)
        {
            ButtonUpdate();
        }

        private void Browser_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            BackButton.IsEnabled = Browser.CanGoBack;
            ForwardButton.IsEnabled = Browser.CanGoForward;
        }

        private void Browser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            ButtonUpdate();
            Classes.History.GetInstance.AddHistory(e.Uri.ToString());
        }

    }
}
