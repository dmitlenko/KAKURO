using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Rankings
{
    class UserRank
    {
        /// <summary>
        /// Unique user ID
        /// </summary>
        public int UID { get; private set; }

        /// <summary>
        /// Hours taken to solve
        /// </summary>
        public int Hour { get; private set; }

        /// <summary>
        /// Minutes taken to solve
        /// </summary>
        public int Minutes { get; private set; }

        /// <summary>
        /// Seconds taken to solve
        /// </summary>
        public int Seconds { get; private set; }

        /// <summary>
        /// Constructor for user's rank
        /// </summary>
        /// <param name="uID">Unique user ID</param>
        /// <param name="hour">Hours taken to solve</param>
        /// <param name="minutes">Minutes taken to solve</param>
        /// <param name="seconds">Seconds taken to solve</param>
        public UserRank(int uID, int hour, int minutes, int seconds)
        {
            UID = uID;
            Hour = hour;
            Minutes = minutes;
            Seconds = seconds;
        }

        /// <summary>
        /// Gets time in string format
        /// </summary>
        /// <returns>Time in string format</returns>
        public string TimeString()
        {
            return String.Format("{0}:{1}:{2}",Hour, Minutes, Seconds);
        }

        /// <summary>
        /// Gets total amount of seconds 
        /// </summary>
        /// <returns>Seconds amount</returns>
        public int TotalSeconds()
        {
            return Hour * 3600 + Minutes * 60 + Seconds;
        }

        public bool Equals(UserRank other)
        {
            if (other == null) return false;

            return other.UID.Equals(UID) && other.TimeString().Equals(TimeString());
        }

        public static bool operator ==(UserRank obj1, UserRank obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return true;
            if (ReferenceEquals(obj1, null))
                return false;
            if (ReferenceEquals(obj2, null))
                return false;
            return obj1.Equals(obj2);
        }

        public static bool operator !=(UserRank obj1, UserRank obj2) => !(obj1 == obj2);

        public override bool Equals(object obj) => Equals(obj as UserRank);

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = UID.GetHashCode();
                hashCode = (hashCode * 397) ^ Hour.GetHashCode();
                hashCode = (hashCode * 397) ^ Minutes.GetHashCode();
                hashCode = (hashCode * 397) ^ Seconds.GetHashCode();
                return hashCode;
            }
        }
    }
}
