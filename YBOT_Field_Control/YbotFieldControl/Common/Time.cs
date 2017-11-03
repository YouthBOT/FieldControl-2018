using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace YbotFieldControl
{
    class Time
    {
        private Stopwatch _elapsedTime = new Stopwatch(); //How much time has gone by
        private DateTime _GameClock;                      //Master Clock
        private bool _timesUp = false;                    //Time up flag

        public Stopwatch elapsedTime     //How much time has gone by
        {
            get
            {
                return _elapsedTime;
            }
        }

        public DateTime GameClock        //Master Clock
        {
            get
            {
                return _GameClock;
            }
        }

        public bool timesUp             //Time up flag
        {
            get
            {
                return _timesUp;
            }
            set
            {
                _timesUp = value;
            }
        }

        /// <summary>
        /// Count down timer
        /// </summary>
        /// <param name="_min">Start Mins</param>
        /// <param name="_sec">Start Secs</param>
        public void countDownStart(int _min, int _sec)
        {
            _GameClock = DateTime.Now + new TimeSpan(0, _min, _sec);
            _elapsedTime.Restart();
        }

        /// <summary>
        /// Count down timer update returns formated time
        /// </summary>
        /// <returns>Current Time</returns>
        public string countDownStatus()
        {
            TimeSpan sp = _GameClock - DateTime.Now;
            if ((sp.Minutes <= 0) && (sp.Seconds <= 0))
            {
                _timesUp = true;
                return ("0:00");
            }
            else
            {
                _timesUp = false;
                return string.Format("{0}:{1:00}", sp.Minutes, sp.Seconds);
            }

        }

        /// <summary>
        /// Checks if given time has passed
        /// </summary>
        /// <param name="_totalsec">Number of secs</param>
        /// <returns>true = times up; false = time isn't up</returns>
        public bool Timer(int _totalsec)
        {
            double _etime = _elapsedTime.Elapsed.TotalSeconds;
            if (_etime >= _totalsec) return true;
            else return false;
        }
    }
}
