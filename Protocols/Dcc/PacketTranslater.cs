using System;
using System.Collections.Generic;

namespace BinkyRailways.Protocols.Dcc
{
    /// <summary>
    /// Class used to translate packet bytes into a RS232 data stream.
    /// The dataformat of RS232 is as follows:
    /// Startbits: 1 (always low)
    /// Stopbits:  1 (always high)
    /// Databits:  8
    /// Speed:     19200 baud
    /// 
    /// DCC 1 bit: 01    short low, short high
    /// DCC 0 bit: 0011  long low, long high. high may be longer
    /// 
    /// </summary>
    public class PacketTranslater
    {
        private const int StartPos = 0;
        private const int StopPos = 9;

        private readonly BitStream packet;
        private readonly List<byte> bytes = new List<byte>();
        private int position = 0; // position in current rs232 byte
        private readonly Rs232Byte currentByte = new Rs232Byte();
        private readonly List<ZeroBit> zeroBits = new List<ZeroBit>();

        /// <summary>
        /// Default ctor
        /// </summary>
        private PacketTranslater(BitStream packet)
        {
            this.packet = packet;
        }

        /// <summary>
        /// Translate the given packet into a RS232 data stream
        /// </summary>
        public static byte[] Translate(BitStream packet)
        {
            var translater = new PacketTranslater(packet);
            return translater.Translate();
        }

        /// <summary>
        /// Translate the given packet into a RS232 data stream
        /// </summary>
        private byte[] Translate()
        {

            while (packet.Remaining > 0)
            {
                // Check current byte overflow
                if (position == StopPos + 1)
                {
                    bytes.Add(currentByte.Data);
                    currentByte.Data = 0;
                    position = StartPos;
                }

                if (packet.Take())
                {
                    // "1"
                    if (position == StopPos)
                    {
                        // Go back to last "0"
                        RestartAtLast0(1);
                    }
                    else
                    {
                        // We have room for "1" bit
                        currentByte[position + 0] = false;
                        currentByte[position + 1] = true;
                        position += 2;
                    }
                }
                else
                {
                    // "0"
                    if (position <= 6)
                    {
                        // We have room for "0" bit

                        // Record position
                        var zeroBit = new ZeroBit(packet.Offset - 1, bytes.Count, position);
                        zeroBits.Add(zeroBit);

                        // Set bits
                        currentByte[position + 0] = false;
                        currentByte[position + 1] = false;
                        currentByte[position + 2] = true;
                        currentByte[position + 3] = true;
                        position += 4;
                    }
                    else
                    {
                        // Go back to last "0"
                        RestartAtLast0(10 - position);
                    }
                }
            }

            // Add last byte
            if (position != StartPos)
            {
                bytes.Add(currentByte.Data);
            }

            return bytes.ToArray();
        }

        /// <summary>
        /// Start translation at the last "0" 
        /// </summary>
        /// <param name="stretch">Number of positions to stretch the last "0"</param>
        private void RestartAtLast0(int stretch)
        {
            while (true)
            {
                if (zeroBits.Count == 0)
                    throw new ArgumentException("No earlier '0' bit available");

                var zeroBit = zeroBits[zeroBits.Count - 1];
                if (zeroBit.CanStretch(stretch))
                {
                    zeroBit.Restart(stretch, packet, bytes, currentByte, ref position);
                    return;
                }
                zeroBits.RemoveAt(zeroBits.Count - 1);
            }
        }

        /// <summary>
        /// Location and state of a "0" bit.
        /// </summary>
        private class ZeroBit
        {
            private readonly int packetOffset;
            private readonly int bytesLength;
            private readonly int position;
            private int stretch;

            public ZeroBit(int packetOffset, int bytesLength, int position)
            {
                this.packetOffset = packetOffset;
                this.bytesLength = bytesLength;
                this.position = position;
            }

            /// <summary>
            /// Can this bit be stretched by the given length?
            /// </summary>
            public bool CanStretch(int stretch)
            {
                var length = 4 + this.stretch + stretch;
                return (position + length <= 10);
            }

            /// <summary>
            /// Start translation at the last "0" 
            /// </summary>
            /// <param name="stretch">Number of positions to stretch the last "0"</param>
            public void Restart(int stretch, BitStream packet, List<byte> bytes, Rs232Byte currentByte, ref int position)
            {
                // Set packet offset
                packet.Offset = packetOffset + 1;

                // Reset current byte
                if (bytes.Count == bytesLength)
                {
                    // Still in same current byte
                }
                else
                {
                    // Restore current byte and trim bytes
                    currentByte.Data = bytes[bytesLength];
                    bytes.RemoveRange(bytesLength, bytes.Count - bytesLength);
                }

                // Reset position
                position = this.position;

                // Set stretched bits
                currentByte[position + 0] = false;
                currentByte[position + 1] = false;
                currentByte[position + 2] = true;
                currentByte[position + 3] = true;
                position += 4;
                this.stretch += stretch;
                for (int i = 0; i < this.stretch; i++)
                {
                    currentByte[position++] = true;
                }
            }
        }
    }
}
