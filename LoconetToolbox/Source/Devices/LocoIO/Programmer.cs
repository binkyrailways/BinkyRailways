using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LocoNetToolBox.Protocol;

namespace LocoNetToolBox.Devices.LocoIO
{
    /// <summary>
    /// LocoIO programmer
    /// </summary>
    public class Programmer
    {
        private const int ATTEMPTS = 3;
        private readonly LocoNetAddress address;
        private readonly int timeout;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Programmer(LocoNetAddress address)
        {
            this.address = address;
            timeout = 2000;
        }

        /// <summary>
        /// Gets address of device that is programmed.
        /// </summary>
        public LocoNetAddress Address { get { return address; } }

        /// <summary>
        /// Read a single SV variable at the given index.
        /// </summary>
        internal bool TryReadSV(LocoBuffer lb, int index, out byte value)
        {
            var cmd = new PeerXferRequest1
                          {
                              Command = PeerXferRequest.Commands.Read,
                              SvAddress = index,
                              DestinationLow = address.Address,
                              SubAddress = address.SubAddress,
                          };

            var result = cmd.ExecuteAndWaitForResponse<PeerXferResponse>(
                lb,
                x => (address.Equals(x.Source) && (x.SvAddress == index) && (x.OriginalCommand == PeerXferRequest.Commands.Read)),
                timeout);
            if (result != null)
            {
                value = result.Data1;
                return true;
            }
            value = 0;
            return false;
        }

        /// <summary>
        /// Write a single SV variable at the given index with the given value.
        /// </summary>
        internal bool WriteSV(LocoBuffer lb, int index, byte value)
        {
            var cmd = new PeerXferRequest1
            {
                Command = PeerXferRequest.Commands.Write,
                SvAddress = index,
                DestinationLow = address.Address,
                SubAddress = address.SubAddress,
                Data1 = value,
            };
            var result = cmd.ExecuteAndWaitForResponse<PeerXferResponse>(
                lb,
                x => (address.Equals(x.Source) && (x.SvAddress == index) && (x.OriginalCommand == PeerXferRequest.Commands.Write)),
                timeout);
            return (result != null) && (result.Data1 == value);
        }

        /// <summary>
        /// Read the given set of SV values into configs.
        /// </summary>
        public void Read(LocoBuffer lb, IEnumerable<SVConfig> configs)
        {
            var list = configs.ToList();
            list.Sort();

            foreach (var iterator in configs)
            {
                var config = iterator;
                byte value;
                config.Valid = TryReadSV(lb, config.Index, out value);
                config.Value = value;
            }
        }

        /// <summary>
        /// Write the given set of SV values
        /// </summary>
        internal void Write(LocoBuffer lb, IEnumerable<SVConfig> configs)
        {
            var list = configs.ToList();
            list.Sort();

            foreach (var config in configs)
            {
                var ok = false;
                for (int attempt = 0; !ok && (attempt < ATTEMPTS); attempt++)
                {
                    // Write
                    ok = WriteSV(lb, config.Index, config.Value);

                    // Wait a while
                    if (!ok)
                    {
                        Thread.Sleep(100);
                    }
                }
                if (!ok)
                {
                    throw new ProgramException(string.Format("Failed to write SV {0}", config.Index));
                }
            }
        }

        /// <summary>
        /// Write a single SV variable at the given index with the given value.
        /// </summary>
        internal bool ChangeAddress(LocoBuffer lb, int address, int subAddress)
        {
            var cmd = new PeerXferRequest1
            {
                Command = PeerXferRequest.Commands.Write,
                SvAddress = 1,
                DestinationLow =this.address.Address,
                SubAddress = this.address.SubAddress,
                Data1 = (byte)address,
            };
            var result = cmd.ExecuteAndWaitForResponse<PeerXferResponse>(
                lb,
                x => ((x.SvAddress == 1) && (x.OriginalCommand == PeerXferRequest.Commands.Write)),
                timeout);

            if (result == null)
                throw new TimeoutException("Program index 1 failed");

            cmd = new PeerXferRequest1
            {
                Command = PeerXferRequest.Commands.Write,
                SvAddress = 2,
                DestinationLow = this.address.Address,
                SubAddress = this.address.SubAddress,
                Data1 = (byte)subAddress,
            };
            result = cmd.ExecuteAndWaitForResponse<PeerXferResponse>(
                lb,
                x => ((x.SvAddress == 2) && (x.OriginalCommand == PeerXferRequest.Commands.Write)),
                timeout);

            if (result == null)
                throw new TimeoutException("Program index 2 failed");

            return true;
        }
    }
}
