using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading; 
using System.Windows;
using System.Windows.Input;
using As.DynamicLink.Audio;
using System.Runtime.InteropServices; // DllImportをするために必要
using System.Windows.Interop; // Window Handleの取得等に必要

namespace As.Classes
{
    public class AudioDeviceSwitcher
    {

        public struct AudioDeviceDesc
        {
            public string Name { get; set; }
            public int ID { get; set; }
        }

        public AudioDeviceSwitcher()
        { 

        }

        public AudioDeviceDesc GetWaveMapperDesc()
        {
            const int WaveMapperID = -1;
            AudioDeviceDesc DeviceDesc = new AudioDeviceDesc();
            DeviceDesc.Name = "Wave Mapper";
            DeviceDesc.ID = WaveMapperID;
            return DeviceDesc;
        }

        public List<AudioDeviceDesc> GetAvailableAudioDeviceList()
        {
            const int MS_SYS_NO_ERROR = 0;
            List<AudioDeviceDesc> AudioDeviceList = new List<AudioDeviceDesc>();
            AudioDevice.WaveOutCaps OutCaps = new AudioDevice.WaveOutCaps();

            AudioDeviceList.Add(GetWaveMapperDesc());

            for (int i = 0; i < AudioDevice.waveOutGetNumDevs(); i++)
            {
                int Result = AudioDevice.waveOutGetDevCaps(i, ref OutCaps, Marshal.SizeOf(typeof(AudioDevice.WaveOutCaps)));

                AudioDeviceDesc DeviceDesc = new AudioDeviceDesc();
                DeviceDesc.Name = (Result == MS_SYS_NO_ERROR) ? OutCaps.szPname : "No Name";
                DeviceDesc.ID = i;
                AudioDeviceList.Add(DeviceDesc);
            }
            return AudioDeviceList;
        }

        public void SwitchAudioDevice(AudioDeviceDesc To, Window window)
        {
            //waveOutOpen(&hWaveOut,WAVE_MAPPER,&wfe,(DWORD)hWnd,0,CALLBACK_WINDOW);
            WindowInteropHelper HostWindow = new WindowInteropHelper(window);
            IntPtr HostWindowHandle = HostWindow.Handle;

            int WaveHandle = 0;
            AudioDevice.WaveFormatEx format =new AudioDevice.WaveFormatEx();
            AudioDevice.WaveOutCaps OutCaps = new AudioDevice.WaveOutCaps();
            AudioDevice.waveOutGetDevCaps(To.ID, ref OutCaps, Marshal.SizeOf(typeof(AudioDevice.WaveOutCaps)));
            int Code = AudioDevice.waveOutOpen(WaveHandle, To.ID, ref format, HostWindowHandle.ToInt32(), 0, 0);

        }
    }

}
