using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SBR
{
    public static class IdleTimeDetection
    {
        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

       
        public static uint GetIdleTime()
        {
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            if (!GetLastInputInfo(ref lastInputInfo))
            {
                throw new Exception("Failed to get last input info.");
            }

            uint idleTime = (uint)Environment.TickCount - lastInputInfo.dwTime;
            return idleTime / 1000; // Convert milliseconds to seconds
        }

        // Because there is no way how to reset the idle time, we have added these two extra methods (see bellow).
        // We need to "reset" the idle time during the midnight.


        //private static uint _idleTimeBaseline = 0;

        //public static void ResetIdleTime()
        //{
            
        //    _idleTimeBaseline = GetRealIdleTime();
        //    Debug.Print($"Idle time reset to {_idleTimeBaseline} seconds. Time: " + DateTime.Now);

        //}
        //public static uint GetIdleTime()
        //{
        //    uint currentIdle = GetRealIdleTime();
        //    return currentIdle >= _idleTimeBaseline ? currentIdle - _idleTimeBaseline : 0;
        //}


    }
}