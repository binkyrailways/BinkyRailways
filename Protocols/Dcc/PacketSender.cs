using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using LocoNetToolBox.Devices;

namespace BinkyRailways.Protocols.Dcc
{
    /// <summary>
    /// Sender that keeps sending the last modified packet for each used loc.
    /// </summary>
    public class PacketSender : IDisposable
    {
        /// <summary>
        /// Fired after the last priority packet was send.
        /// </summary>
        public event EventHandler PriorityPacketsEmpty;

        private readonly Slot[] slots = new Slot[128];
        private readonly object slotsLock = new object();
        private readonly List<byte[]> priorityPackets = new List<byte[]>();
        private readonly object prioritySlotsLock = new object();
        private SerialPort port;
        private readonly byte[] idlePacket;
        private Thread thread;
        private bool stop;

        /// <summary>
        /// Default ctor
        /// </summary>
        public PacketSender(string portName)
        {
            idlePacket = PacketTranslater.Translate(Packets.CreateIdle());
            PortName = portName;
        }

        /// <summary>
        /// Serial port name
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// Start this sender on a new thread.
        /// </summary>
        public void Start()
        {
            if (thread != null)
                return;
            ResetState();
            Open();
            thread = new Thread(Run);
            thread.Start();
        }

        /// <summary>
        /// Stop the thread.
        /// </summary>
        public void Stop()
        {
            stop = true;
        }

        /// <summary>
        /// Queue a speed and direction packet for the given address.
        /// </summary>
        public void SendSpeedAndDirection(int address, byte[] packet)
        {
            var slot = GetSlot(address);
            slot.SpeedPacket = packet;
            lock (prioritySlotsLock)
            {
                priorityPackets.Add(packet);
            }
        }

        /// <summary>
        /// Open the serial port
        /// </summary>
        private void Open()
        {
            if (port == null)
            {
                port = new SerialPort(PortName, 19200, Parity.None, 8, StopBits.One);
                port.Open();
            }
        }

        /// <summary>
        /// Keep sending packets.
        /// </summary>
        private void Run()
        {
            try
            {
                var lastPacketWasPriorityPacket = true;
                var nextSlotIndex = 0;
                while (!stop)
                {
                    if (nextSlotIndex >= slots.Length)
                    {
                        nextSlotIndex = 0;
                    }

                    try
                    {
                        byte[] packet = null;
                        Slot slot = null;
                        var noPriorityPackets = false;

                        lock (prioritySlotsLock)
                        {
                            if (priorityPackets.Count > 0)
                            {
                                packet = priorityPackets[0];
                                priorityPackets.RemoveAt(0);
                                lastPacketWasPriorityPacket = true;
                            }
                            else
                            {
                                noPriorityPackets = true;
                            }
                        }

                        if (noPriorityPackets && lastPacketWasPriorityPacket)
                        {
                            PriorityPacketsEmpty.Fire(this);
                            lastPacketWasPriorityPacket = false;
                        }

                        if (packet == null)
                        {
                            lock (slotsLock)
                            {
                                while ((slot == null) && (nextSlotIndex < slots.Length))
                                {
                                    slot = slots[nextSlotIndex++];
                                }
                            }
                            if (slot != null)
                            {
                                packet = slot.NextPacket();
                            }
                            if (packet == null)
                            {
                                packet = idlePacket;
                            }
                        }

                        port.Write(packet, 0, packet.Length);

                        if (packet == idlePacket)
                        {
                            Thread.Sleep(5);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            finally
            {
                thread = null;
                port.Close();
                port = null;
                ResetState();
            }
        }

        /// <summary>
        /// Gets the slot with the given address.
        /// Create a new slot if needed.
        /// </summary>
        private Slot GetSlot(int address)
        {
            var firstFree = -1;
            lock (slotsLock)
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    var slot = slots[i];
                    if (slot == null)
                    {
                        if (firstFree < 0)
                            firstFree = i;
                    }
                    else
                    {
                        if (slot.Address == address)
                        {
                            return slot;
                        }
                    }
                }
                var newSlot = new Slot(address);
                slots[firstFree] = newSlot;
                return newSlot;
            }
        }

        /// <summary>
        /// Reset the entire state
        /// </summary>
        private void ResetState()
        {
            lock (slotsLock)
            {
                Array.Clear(slots, 0, slots.Length);
            }
            lock (prioritySlotsLock)
            {
                priorityPackets.Clear();
            }
            stop = false;
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        void IDisposable.Dispose()
        {
            Stop();
        }

        private class Slot
        {
            private readonly int address;
            private int last;
            public byte[] SpeedPacket;
            public byte[] FunctionPacket;

            /// <summary>
            /// Default ctor
            /// </summary>
            public Slot(int address)
            {
                this.address = address;
            }

            public int Address
            {
                get { return address; }
            }

            public byte[] NextPacket()
            {
                if ((last == 0) && (SpeedPacket != null))
                {
                    last = 1;
                    return SpeedPacket;
                }
                else if (FunctionPacket != null)
                {
                    last = 0;
                    return FunctionPacket;
                }
                return null;
            }
        }
    }
}
