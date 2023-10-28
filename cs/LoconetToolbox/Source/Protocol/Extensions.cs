using System;

namespace LocoNetToolBox.Protocol
{
    public static class Extensions
    {
        /// <summary>
        /// Are the given bits all set in the given value.
        /// </summary>
        public static bool IsSet(this DirFunc value, DirFunc bits)
        {
            return ((value & bits) == bits);
        }

        /// <summary>
        /// Are the given bits all set in the given value.
        /// </summary>
        public static bool IsSet(this SlotStatus1 value, SlotStatus1 bits)
        {
            return ((value & bits) == bits);
        }

        /// <summary>
        /// Are the given bits all set in the given value.
        /// </summary>
        public static bool IsSet(this SlotStatus2 value, SlotStatus2 bits)
        {
            return ((value & bits) == bits);
        }

        /// <summary>
        /// Are the given bits all set in the given value.
        /// </summary>
        public static bool IsSet(this Sound value, Sound bits)
        {
            return ((value & bits) == bits);
        }

        /// <summary>
        /// Are the given bits all set in the given value.
        /// </summary>
        public static bool IsSet(this TrackStatus value, TrackStatus bits)
        {
            return ((value & bits) == bits);
        }
    }
}
