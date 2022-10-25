using Kakuro.Engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Windows.Core
{
    [Serializable]
    public class CheckPoint
    {
        public string Title { get; private set; }
        public Time Time { get; private set; }
        public DateTime DateTime { get; private set; }
        public KakuroBoard Instance { get; private set; }
        public CheckPoint() { }
        public CheckPoint(string title, Time time, KakuroBoard instance, DateTime dateTime)
        {
            Title = title;
            Time = time;
            Instance = instance;
            DateTime = dateTime;
        }

        public override string ToString()
        {
            return String.Format("{0} | {1} | {2}", Title, Time.ToString(), DateTime.ToString("d"));
        }
    }
}
