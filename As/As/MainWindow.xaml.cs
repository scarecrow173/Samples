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

namespace As
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Classes.HotkeyRegister MainHotkeyRegister = null;
        Classes.AudioDeviceSwitcher DeviceSwitcher = null;
        public MainWindow()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += (sender, e) => this.DragMove();

            MainHotkeyRegister = new Classes.HotkeyRegister(this);
            MainHotkeyRegister.RegisterControledHotkey(ModifierKeys.Alt, Key.A, new EventHandler(SwitchAudioDevicce));

            DeviceSwitcher = new Classes.AudioDeviceSwitcher();
        }

        private void SwitchAudioDevicce(object sender, EventArgs e)
        {
            var AudioList = DeviceSwitcher.GetAvailableAudioDeviceList();
            DeviceSwitcher.SwitchAudioDevice(AudioList[2], this);


        }

        private void Button_Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Maximize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void Button_Normal(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
