using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices; // DllImportをするために必要
using System.Threading; // イベントの別スレッド処理に必要
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop; // Window Handleの取得等に必要

namespace As.Classes
{
    public class AudioDeviceSwitcher
    {
        [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Auto)]
        public struct WaveOutCaps
        {
            public short wMid;
            public short wPid;
            public uint vDriverVersion;
            // 32 : max product name length (including NULL)
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szPname;
            public int dwFormats;
            public short wChannels;
            public short wReserved1;
            public int dwSupport;
        }

        [StructLayout(LayoutKind.Explicit, Size = 18)]
		public struct WAVEFORMATEX
		{
			[FieldOffset(0)]
			public ushort wFormatTag;

			[FieldOffset(2)]
			public ushort nChannels;

			[FieldOffset(4)]
			public uint nSamplesPerSec;

			[FieldOffset(8)]
			public uint nAvgBytesPerSec;

			[FieldOffset(12)]
			public ushort nBlockAlign;

			[FieldOffset(14)]
			public ushort wBitsPerSample;

			[FieldOffset(16)]
			public ushort cbSize;
		}

		[StructLayout(LayoutKind.Explicit, Size = 4)]
		public struct HWAVEOUT__
		{
			[FieldOffset(0)]
			public int unused;
		}

        [StructLayout(LayoutKind.Explicit, Size = 32)]
        public struct wavehdr_tag
        {
            [FieldOffset(0)]
            public unsafe sbyte* lpData;

            [FieldOffset(4)]
            public uint dwBufferLength;

            [FieldOffset(8)]
            public uint dwBytesRecorded;

            [FieldOffset(12)]
            public uint dwUser;

            [FieldOffset(16)]
            public uint dwFlags;

            [FieldOffset(20)]
            public uint dwLoops;

            [FieldOffset(24)]
            public unsafe wavehdr_tag* lpNext;

            [FieldOffset(28)]
            public uint reserved;
        }
		
        [DllImport("winmm.dll", EntryPoint = "waveOutOpen", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe uint waveOutOpen(HWAVEOUT__** phwo, uint uDeviceID, WAVEFORMATEX* pwfx, uint dwCallback, uint dwInstance, uint fdwOpen);

        [DllImport("winmm.dll", EntryPoint = "waveOutPrepareHeader", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe uint waveOutPrepareHeader(HWAVEOUT__* hwo, wavehdr_tag* pwh, uint cbwh);

        [DllImport("winmm.dll", EntryPoint = "waveOutWrite", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe uint waveOutWrite(HWAVEOUT__* hwo, wavehdr_tag* pwh, uint cbwh);

        [DllImport("winmm.dll", EntryPoint = "waveOutClose", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe uint waveOutClose(HWAVEOUT__* hwo);

        [DllImport("winmm.dll", EntryPoint = "waveOutReset", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe uint waveOutReset(HWAVEOUT__* hwo);

        [DllImport("winmm.dll", EntryPoint = "waveOutUnprepareHeader", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe uint waveOutUnprepareHeader(HWAVEOUT__* hwo, wavehdr_tag* pwh, uint cbwh);

        [DllImport("winmm.dll", SetLastError = true)]
        private static extern int waveOutGetNumDevs();

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int waveOutGetDevCaps(int uDeviceID, ref WaveOutCaps pwoc, int cbwoc);


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
            WaveOutCaps OutCaps = new WaveOutCaps();

            AudioDeviceList.Add(GetWaveMapperDesc());

            for (int i = 0; i < waveOutGetNumDevs(); i++)
            {
                int Result = waveOutGetDevCaps(i, ref OutCaps, Marshal.SizeOf(typeof(WaveOutCaps)));

                AudioDeviceDesc DeviceDesc = new AudioDeviceDesc();
                DeviceDesc.Name = (Result == MS_SYS_NO_ERROR) ? OutCaps.szPname : "No Name";
                DeviceDesc.ID = i;
                AudioDeviceList.Add(DeviceDesc);
            }
            return AudioDeviceList;
        }
    }

}
