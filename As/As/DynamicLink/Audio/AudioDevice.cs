using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices; // DllImportをするために必要

namespace As.DynamicLink.Audio
{
    public class AudioDevice
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct MmTime
        {
            [FieldOffset(0)]
            public uint     Type;          // indicates the contents of the union

            [FieldOffset(4)]
            public uint     MilliSecond;             // milliseconds
            [FieldOffset(4)]
            public uint     Sample;         // samples
            [FieldOffset(4)]
            public uint     ByteCount;             // byte count
            [FieldOffset(4)]
            public uint     Ticks;          // ticks in MIDI stream

            // SMPTE
            [FieldOffset(4)]
            public byte     Hour;           // hours
            [FieldOffset(5)]
            public byte     Minite;            // minutes
            [FieldOffset(6)]
            public byte     Secound;            // seconds
            [FieldOffset(7)]
            public byte     Frame;          // frames
            [FieldOffset(8)]
            public byte     FPS;            // frames per second
            [FieldOffset(9)]
            public byte     Padding;          // pad
            [FieldOffset(10)]
            public byte     Padding0;
            [FieldOffset(11)]
            public byte     Padding1;

            // MIDI
            [FieldOffset(4)]
            public uint     SongPtrPosition;     // song pointer position
        };
      
        public struct WaveFormat
        {
            public int nAvgBytesPerSec;
            public short nBlockAlign;
            public short nChannels;
            public int nSamplesPerSec;
            public short wFormatTag;
        }

        [StructLayout(LayoutKind.Explicit, Size = 18)]
        public struct WaveFormatEx
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
        };

        public struct PcmWaveFormat
        {
            public short wBitsPerSample;
            public WaveFormat wf;
        }

        public struct WaveHdr
        {
            public int Reserved;
            public int dwBufferLength;
            public int dwBytesRecorded;
            public int dwFlags;
            public int dwLoops;
            public int dwUser;
            public string lpData;
            public int lpNext;
        };

        public struct WaveInCaps
        {
            public int dwFormats;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szPname;
            public int vDriverVersion;
            public short wChannels;
            public short wMid;
            public short wPid;
        };

        public struct WaveOutCaps
        {
            public int dwFormats;
            public int dwSupport;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szPname;
            public int vDriverVersion;
            public short wChannels;
            public short wMid;
            public short wPid;
        };

        [DllImport("winmm.dll")]
        public static extern int waveInAddBuffer(IntPtr hWaveIn, ref WaveHdr lpWaveInHdr, int uSize);

        [DllImport("winmm.dll")]
        public static extern int waveInClose(IntPtr hWaveIn);

        [DllImport("winmm.dll")]
        public static extern int waveInGetDevCaps(int uDeviceID, ref WaveInCaps lpCaps, int uSize);

        [DllImport("winmm.dll")]
        public static extern int waveInGetErrorText(int err, string lpText, int uSize);

        [DllImport("winmm.dll")]
        public static extern int waveInGetID(IntPtr hWaveIn, ref int lpuDeviceID);

        [DllImport("winmm.dll")]
        public static extern int waveInGetNumDevs();

        [DllImport("winmm.dll")]
        public static extern int waveInGetPosition(IntPtr hWaveIn, ref MmTime lpInfo, int uSize);

        [DllImport("winmm.dll")]
        public static extern int waveInMessage(IntPtr hWaveIn, int msg, int dw1, int dw2);

        [DllImport("winmm.dll")]
        public static extern int waveInOpen(int lphWaveIn, int uDeviceID, ref WaveFormat lpFormat, int dwCallback,
                                            int dwInstance, int dwFlags);

        [DllImport("winmm.dll")]
        public static extern int waveInPrepareHeader(IntPtr hWaveIn, ref WaveHdr lpWaveInHdr, int uSize);

        [DllImport("winmm.dll")]
        public static extern int waveInReset(IntPtr hWaveIn);

        [DllImport("winmm.dll")]
        public static extern int waveInStart(IntPtr hWaveIn);

        [DllImport("winmm.dll")]
        public static extern int waveInStop(IntPtr hWaveIn);

        [DllImport("winmm.dll")]
        public static extern int waveInUnprepareHeader(IntPtr hWaveIn, ref WaveHdr lpWaveInHdr, int uSize);

        [DllImport("winmm.dll")]
        public static extern int waveOutBreakLoop(IntPtr hWaveOut);

        [DllImport("winmm.dll")]
        public static extern int waveOutClose(IntPtr hWaveOut);

        [DllImport("winmm.dll")]
        public static extern int waveOutGetDevCaps(int uDeviceID, ref WaveOutCaps lpCaps, int uSize);

        [DllImport("winmm.dll")]
        public static extern int waveOutGetErrorText(int err, string lpText, int uSize);

        [DllImport("winmm.dll")]
        public static extern int waveOutGetID(IntPtr hWaveOut, ref int lpuDeviceID);

        [DllImport("winmm.dll")]
        public static extern int waveOutGetNumDevs();

        [DllImport("winmm.dll")]
        public static extern int waveOutGetPitch(IntPtr hWaveOut, ref int lpdwPitch);

        [DllImport("winmm.dll")]
        public static extern int waveOutGetPlaybackRate(IntPtr hWaveOut, ref int lpdwRate);

        [DllImport("winmm.dll")]
        public static extern int waveOutGetPosition(IntPtr hWaveOut, ref MmTime lpInfo, int uSize);

        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(int uDeviceID, ref int lpdwVolume);

        [DllImport("winmm.dll")]
        public static extern int waveOutMessage(IntPtr hWaveOut, int msg, int dw1, int dw2);

        [DllImport("winmm.dll")]
        public static extern int waveOutOpen(int lphWaveOut, int uDeviceID, ref WaveFormatEx lpFormat, int dwCallback,
                                             int dwInstance, int dwFlags);

        [DllImport("winmm.dll")]
        public static extern int waveOutPause(IntPtr hWaveOut);

        [DllImport("winmm.dll")]
        public static extern int waveOutPrepareHeader(IntPtr hWaveOut, ref WaveHdr lpWaveOutHdr, int uSize);

        [DllImport("winmm.dll")]
        public static extern int waveOutReset(IntPtr hWaveOut);

        [DllImport("winmm.dll")]
        public static extern int waveOutRestart(IntPtr hWaveOut);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetPitch(IntPtr hWaveOut, int dwPitch);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetPlaybackRate(IntPtr hWaveOut, int dwRate);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(int uDeviceID, int dwVolume);

        [DllImport("winmm.dll")]
        public static extern int waveOutUnprepareHeader(IntPtr hWaveOut, ref WaveHdr lpWaveOutHdr, int uSize);

        [DllImport("winmm.dll")]
        public static extern int waveOutWrite(IntPtr hWaveOut, ref WaveHdr lpWaveOutHdr, int uSize);

    }
}
