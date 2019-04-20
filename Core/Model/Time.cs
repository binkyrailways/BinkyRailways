using System;
using System.ComponentModel;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Model time.
    /// </summary>
    [TypeConverter(typeof(TimeTypeConverter))]
    public struct Time : IComparable<Time>
    {
        public static readonly Time MinValue = new Time(0, 0);
        public static readonly Time MaxValue = new Time(23, 59);

        private const int MinuteMultiplier = 60;
        private const int HourMultiplier = MinuteMultiplier * 60;
        private readonly long value;

        /// <summary>
        /// Initialize model time from human time.
        /// </summary>
        public Time(DateTime humanTime, double speedFactor)
        {
            var humanValue = (humanTime.Hour * HourMultiplier) + (humanTime.Minute * MinuteMultiplier) + humanTime.Second;
            value = (long)(humanValue * speedFactor);
        }

        /// <summary>
        /// Initialize model time from human time.
        /// </summary>
        internal Time(int hour, int minute)
        {
            value = (hour * HourMultiplier) + (minute * MinuteMultiplier);
        }

        /// <summary>
        /// Gets the hour of day (0..23)
        /// </summary>
        public int Hour
        {
            get { return (int)((value / HourMultiplier) % 24); }
        }

        /// <summary>
        /// Gets the minute of the hour (0..59)
        /// </summary>
        public int Minute
        {
            get { return (int)((value / MinuteMultiplier) % 60); }
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(Time other)
        {
            var h1 = Hour;
            var h2 = other.Hour;
            if (h1 < h2) return -1;
            if (h1 > h2) return 1;

            var m1 = Minute;
            var m2 = other.Minute;
            if (m1 < m2) return -1;
            if (m1 > m2) return 1;

            return 0;
        }

        /// <summary>
        /// Convert to string
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}:{1:D2}", Hour, Minute);
        }

        /// <summary>
        /// Try to parse a string into a time.
        /// </summary>
        public static bool TryParse(string s, out Time value)
        {
            value = MinValue;
            if (string.IsNullOrEmpty(s))
                return true;

            var parts = s.Split(':');
            if (parts.Length == 2)
            {
                int hour, minute;
                if (int.TryParse(parts[0], out hour) && int.TryParse(parts[1], out minute))
                {
                    value = new Time(hour, minute);
                    return true;
                }
            }
            return false;
        }

        public static bool operator <(Time a, Time b) { return (a.CompareTo(b) < 0); }
        public static bool operator <=(Time a, Time b) { return (a.CompareTo(b) <= 0); }
        public static bool operator >(Time a, Time b) { return (a.CompareTo(b) > 0); }
        public static bool operator >=(Time a, Time b) { return (a.CompareTo(b) >= 0); }
        public static bool operator ==(Time a, Time b) { return (a.CompareTo(b) == 0); }
        public static bool operator !=(Time a, Time b) { return (a.CompareTo(b) != 0); }
    }
}
