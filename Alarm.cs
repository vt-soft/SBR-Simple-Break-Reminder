using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SBR
{
    public class Alarm
    {

        Stopwatch watch;

        public Boolean IsRunning {get; private set;}

        // Set this value in case you want to mesuare decreasing time from value AlarmTimeSec.
        public int AlarmTimeSec {get; set;}

        public long RemainingSec
        {
            get
            {
                return (long)Math.Round(AlarmTimeSec - (watch.ElapsedMilliseconds/1000.0));
            }
        }

        // Especially for Iddle time we need to set elapsedSec to the specific value. So offest is in seconds.
        public long Offset { get; set; } = 0;
        public long ElapsedSec
        {
            get
            {
                return (long)Math.Round(Offset + (watch.ElapsedMilliseconds / 1000.0));
            }
        }

        //public long ElapsedMilliSec
        //{
        //    get
        //    {
        //        return Offset * 1000 + watch.ElapsedMilliseconds;
        //    }
        //}


        public Alarm()
        {
            watch = new Stopwatch();
            IsRunning = false;
        }

        public void Start()
        {
            watch.Start();
            IsRunning = true;
        }
        public void Stop()
        {
            watch.Stop();
            IsRunning = false;
        }

        // Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time.
        public void Restart()
        {
            watch.Restart();  
            IsRunning = true;
        }

        // Stops time interval measurement, resets the elapsed time to zero.
        public void Reset()
        {
            watch.Reset();  
            IsRunning = false;
        }




    }
}
