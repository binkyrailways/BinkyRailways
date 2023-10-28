using System;

namespace BinkyRailways.Protocols.Dcc
{
    /// <summary>
    /// Wrap a stream of bits.
    /// </summary>
    public class BitStream
    {
        private readonly bool[] bits;
        private int offset;

        /// <summary>
        /// Default ctor
        /// </summary>
        public BitStream(bool[] bits)
        {
            this.bits = bits;
        }

        /// <summary>
        /// Gets the number of remaining bits.
        /// </summary>
        public int Remaining
        {
            get { return bits.Length - offset; }
        }

        /// <summary>
        /// Current offset
        /// </summary>
        public int Offset
        {
            get { return offset; }
            set
            {
                if ((value < 0) || (offset > bits.Length))
                    throw new ArgumentException("offset: " + offset);
                offset = value;
            }
        }

        /// <summary>
        /// Move the offset forward by delta bits.
        /// </summary>
        public void Seek(int delta)
        {
            offset = Math.Min(offset + delta, bits.Length);
        }

        /// <summary>
        /// Gets the bit at the current offset.
        /// Then move the offset to the next bit.
        /// </summary>
        public bool Take()
        {
            var result = bits[offset]; 
            Seek(1);
            return result;
        }

        /// <summary>
        /// Does the current head of the stream start with the given pattern?
        /// </summary>
        public bool StartsWith(bool[] pattern)
        {
            var patternLength = pattern.Length;
            if (patternLength > Remaining)
                return false;
            for (var i = 0; i < patternLength; i++)
            {
                if (bits[offset + i] != pattern[i])
                    return false;
            }
            return true;
        }
    }
}
