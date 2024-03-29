﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Core
{
    [Serializable]
    public class Time
    {
        private int hours;
        private int minutes;
        private int seconds;

        /// <summary>
        /// Consructor for Time class
        /// </summary>
        /// <param name="hours">Hours</param>
        /// <param name="minutes">Minutes</param>
        /// <param name="seconds">Seconds</param>
        public Time(int hours, int minutes, int seconds)
        {
            this.hours = 0;
            this.minutes = 0;
            this.seconds = 0;

            if (IsValidTime(hours, minutes, seconds))
            {
                this.hours = hours;
                this.minutes = minutes;
                this.seconds = seconds;
            }
        }

        /// <summary>
        /// Default constructor for Time class
        /// </summary>
        public Time()
        {
            hours = 0;
            minutes = 0;
            seconds = 0;
        }

        /// <summary>
        /// Hours
        /// </summary>
        public int Hours
        {
            get { return hours; }
            set { hours = value; }
        }

        /// <summary>
        /// Minutes
        /// </summary>
        public int Minutes
        {
            get { return minutes; }
            set { minutes = value; }
        }

        /// <summary>
        /// Seconds
        /// </summary>
        public int Seconds
        {
            get { return seconds; }
            set { seconds = value; }
        }
        /// <summary>
        /// Chech if time is valid
        /// </summary>
        /// <param name="hours">Hours</param>
        /// <param name="minutes">Minutes</param>
        /// <param name="seconds">Seconds</param>
        /// <returns>Returns <c>true</c> if time is valid</returns>
        public static bool IsValidTime(int hours, int minutes, int seconds)
        {
            return hours >= 0 && (minutes >= 0 && 59 >= minutes) && (seconds >= 0 && 59 >= seconds);
        }

        /// <summary>
        /// Get total seconds from time
        /// </summary>
        /// <returns>Total seconds</returns>
        public int TotalSeconds()
        {
            return hours * 60 * 60 + minutes * 60 + seconds;
        }

        /// <summary>
        /// Get current timestamp
        /// </summary>
        /// <returns>Time stamp in unix format</returns>
        public static long UnixTimeMillis()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// Increases seconds by 1
        /// </summary>
        public void IncreaseSecond()
        {
            if(!IsValidTime(hours, minutes, seconds + 1))
            {
                IncreaseMinute();
                seconds = 0;
            } else
            {
                seconds++;
            }
        }

        /// <summary>
        /// Increases minutes by 1
        /// </summary>
        public void IncreaseMinute()
        {
            if (!IsValidTime(hours, minutes + 1, seconds))
            {
                IncreaseHour();
                minutes = 0;
            }
            else
            {
                minutes++;
            }
        }

        /// <summary>
        /// Increases hours by 1
        /// </summary>
        public void IncreaseHour()
        {
            hours++;
        }

        /// <summary>
        /// Convert time to string
        /// </summary>
        /// <returns>String with time in HH:mm:ss format</returns>
        public override string ToString()
        {
            return String.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
    }
}