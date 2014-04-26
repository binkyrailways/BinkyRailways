using System;

namespace BinkyRailways.Protocols.Dcc
{
    /// <summary>
    /// Packet creators
    /// </summary>
    public static class Packets
    {
        private const int SpeedSteps128 = 128;
        private const int SpeedSteps28 = 28;
        private const int SpeedSteps14 = 14;
        private const int PreambleCount = 15;

        /// <summary>
        /// Create a standard 3 byte idle packet.
        /// </summary>
        internal static BitStream CreateIdle()
        {
            var bits = new bool[PreambleCount + 1 + (3 * 9)];
            for (var i = 0; i < PreambleCount; i++)
            {
                bits[i] = true;
            }
           
            var offset = PreambleCount + 1;
            Encode(bits, offset, 0xFF);
            offset += 9;
            Encode(bits, offset, 0);
            offset += 9;
            Encode(bits, offset, 0xFF);
            offset += 8;
            bits[offset] = true;
            return new BitStream(bits);
        }

        /// <summary>
        /// Create a standard 3 byte packet:
        /// address, data, error detection
        /// </summary>
        public static BitStream CreateSpeedAndDirection(int address, byte speed, bool direction, int speedSteps)
        {
            TestSpeedSteps(speedSteps);
            var addressBytes = (address > 127) ? 2 : 1;
            var dataBytes = (speedSteps == SpeedSteps128) ? 2 : 1;
            var bits = new bool[PreambleCount + 1 + ((addressBytes + dataBytes + 1) * 9)];
            var offset = PreambleCount + 1;
            byte error = 0;
            for (var i = 0; i < PreambleCount; i++)
            {
                bits[i] = true;
            }

            // First address byte
            if (addressBytes == 1)
            {
                // Single
                var value = (byte)(address & 0x7f);
                Encode(bits, offset, value);
                offset += 9;
                error ^= value;
            }
            else
            {
                // 2 address bytes
                var value1 = (byte)(0xc0 | ((address >> 8) & 0x3f));
                Encode(bits, offset, value1);
                offset += 9;
                var value2 = (byte)(address & 0xff);
                Encode(bits, offset, value2);
                offset += 9;
                error ^= value1;
                error ^= value2;
            }

            if (speedSteps != SpeedSteps128)
            {
                // 14 or 28 speed steps
                // data byte
                var data = 0x40;
                if (direction)
                    data |= 0x20;
                if (speedSteps == SpeedSteps14)
                {
                    // exclude E-stop
                    if (speed > 0)
                        speed++;

                    speed &= 0x0f;
                    data |= speed;
                }
                else
                {
                    // exclude E-stop
                    if (speed > 0)
                        speed += 3;

                    if ((speed & 0x01) == 1)
                        data |= 0x10;
                    data |= (byte)((speed >> 1) & 0x0f);
                }

                Encode(bits, offset, (byte)data);
                offset += 9;
                error ^= (byte)data;
            }
            else
            {
                // exclude E-stop
                if (speed > 0)
                    speed++;

                // 128 speed steps
                // data byte
                var data1 = 0x3f;
                var data2 = speed & 0x7f;
                if (direction)
                    data2 |= 0x80;

                Encode(bits, offset, (byte)data1);
                offset += 9;
                Encode(bits, offset, (byte)data2);
                offset += 9;
                error ^= (byte)data1;
                error ^= (byte)data2;
            }
            Encode(bits, offset, error);
            offset += 8;
            bits[offset] = true;
            return new BitStream(bits);
        }

        /// <summary>
        /// Encode the given byte value into a bit array starting at the given offset.
        /// </summary>
        private static void Encode(bool[] target, int targetOffset, byte value)
        {
            for (var i = 7; i >= 0; i--)
            {
                target[targetOffset + i] = ((value & 0x01) == 1);
                value >>= 1;
            }
        }

        /// <summary>
        /// Validate the given speed steps value
        /// </summary>
        private static void TestSpeedSteps(int speedSteps)
        {
            if ((speedSteps != SpeedSteps128) && (speedSteps != SpeedSteps28) && (speedSteps != SpeedSteps14))
                throw new ArgumentException("Invalid speed steps: " + speedSteps);
        }
    }
}
