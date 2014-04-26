using System;

namespace BinkyRailways.Protocols.Dcc
{
    /// <summary>
    /// Helper class used to build up RS232 data bytes.
    /// </summary>
    internal sealed class Rs232Byte
    {
        private const bool StartBit = false;
        private const bool StopBit = true;
        private byte data;

        /// <summary>
        /// Gets/sets the bit at the given position.
        /// </summary>
        /// <param name="position">0 == start bit, 9 == stop bit, 1..8 == data bits</param>
        /// <returns></returns>
        public bool this[int position]
        {
            get
            {
                if ((position < 0) || (position > 9)) 
                    throw new ArgumentException("Invalid position: " + position);
                if (position == 0)
                    return StartBit;
                if (position == 9)
                    return StopBit;
                // data bits is LSB first
                position--;
                return ((data & (1 << position)) != 0);
            }
            set
            {
                if ((position < 0) || (position > 9))
                    throw new ArgumentException("Invalid position: " + position);
                if (position == 0)
                {
                    if (value != StartBit)
                        throw new ArgumentException("StartBit expected");
                }
                if (position == 9)
                {
                    if (value != StopBit)
                        throw new ArgumentException("StopBit expected");
                }
                // data bits is LSB first
                position--;
                if (value)
                    data = (byte)(data | (1 << position));
                else
                    data = (byte) (data & ~(1 << position));
            }
        }

        /// <summary>
        /// Gets/sets the entire data byte (without start and stop bits).
        /// </summary>
        public byte Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
