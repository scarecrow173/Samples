using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices; // DllImportをするために必要
using System.Threading; // イベントの別スレッド処理に必要
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop; // Window Handleの取得等に必要

namespace As.Classes
{
    class HotkeyRegister
    {
        // HotKey Message ID
        private const int WM_HOTKEY = 0x0312;

        private IntPtr HostWindowHandle;

        // ホットキーIDとイベントで対になるディクショナリ
        private Dictionary<int, EventHandler> HotkeyEvents;

        // 戻り値：成功 = 0以外、失敗 = 0（既に他が登録済み)
        [DllImport("user32.dll")]
        private static extern int RegisterHotKey(IntPtr hWnd, int id, int MOD_KEY, int VK);

        // 戻り値：成功 = 0以外、失敗 = 0
        [DllImport("user32.dll")]
        private static extern int UnregisterHotKey(IntPtr hWnd, int id);

        // 初期化
        public HotkeyRegister(Window window)
        {
            // WindowのHandleを取得
            WindowInteropHelper HostWindow = new WindowInteropHelper(window);
            this.HostWindowHandle = HostWindow.Handle;

            // ホットキーのイベントハンドラを設定
            ComponentDispatcher.ThreadPreprocessMessage += ComponentDispatcher_ThreadPreprocessMessage;

            // イベントディクショナリを初期化
            HotkeyEvents = new Dictionary<int, System.EventHandler>();
        }

        // HotKeyの動作を設定する
        public void ComponentDispatcher_ThreadPreprocessMessage(ref MSG Message, ref bool IsHandled)
        {
            // ホットキーを表すメッセージであるか否か
            if (Message.message != WM_HOTKEY)
                return;

            int HotkeyID = Message.wParam.ToInt32();
            if (!this.HotkeyEvents.Any((x) => x.Key == HotkeyID)) 
                return;

            // 両方を満たす場合は登録してあるホットキーのイベントを実行
            new ThreadStart(
                () => HotkeyEvents[HotkeyID](this, EventArgs.Empty)
            ).Invoke();
        }

        // HotKeyの登録
        public void RegisterControledHotkey(ModifierKeys ModifierKey, Key Trigger, EventHandler eh)
        {
            // 0xc000～0xffff はDLL用なので使用不可能 0x0000～0xbfff はIDとして使用可能
            const int AvailableHotkeyMax = 0xc000;

            int ModifierKeyID = (int)ModifierKey;
            int TriggerID = KeyInterop.VirtualKeyFromKey(Trigger);

            // HotKey登録時に指定するIDを決定する
            
            int i = 0;
            while ((++i < AvailableHotkeyMax) && RegisterHotKey(this.HostWindowHandle, i, ModifierKeyID, TriggerID) == 0) ;

            if (i < AvailableHotkeyMax)
            {
                this.HotkeyEvents.Add(i, eh);
            }
        }


        // HotKeyの全開放
        public void UnregisterControledHotkey()
        {
            foreach (int RegistedHotkeyID in this.HotkeyEvents.Keys)
            {
                UnregisterHotKey(this.HostWindowHandle, RegistedHotkeyID);
            }
        }
    }
}
