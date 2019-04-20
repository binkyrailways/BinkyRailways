using System;

namespace LocoNetToolBox.Devices.LocoIO
{
    /// <summary>
    /// Mode for a single connector (8 pins)
    /// </summary>
    public abstract partial class ConnectorMode 
    {
        private readonly PinMode[] pins;
        private readonly int addressCount;
        private readonly string[] subModes;
        private readonly string[] addressNames;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected ConnectorMode(string name, int addressCount, string[] addressNames, params PinMode[] pins)
            : this(name, addressCount, addressNames, null, pins)
        {            
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        protected ConnectorMode(string name, int addressCount, string[] addressNames, string[] subModes, params PinMode[] pins)
        {
            if (pins.Length > 8)
            {
                throw new ArgumentException("Invalid number of pins");
            }
            if (addressCount != addressNames.Length)
            {
                throw new ArgumentException("Invalid number of address names");
            }
            this.pins = pins;
            this.addressCount = addressCount;
            this.addressNames = addressNames;
            this.subModes = subModes ?? new string[0];
            Name = name;
        }

        /// <summary>
        /// Human readable name of this mode.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the number of pins used in this mode.
        /// </summary>
        public int PinCount { get { return pins.Length; } }

        /// <summary>
        /// Gets the number of port addresses used in this mode.
        /// </summary>
        public int AddressCount { get { return addressCount; } }

        /// <summary>
        /// Gets the name of an address at the given index (0..AddressCount-1)
        /// </summary>
        public string GetAddressName(int index)
        {
            return addressNames[index];
        }

        /// <summary>
        /// Gets all sub modes supported by this connector mode.
        /// Will never return null.
        /// </summary>
        public string[] SubModes { get { return subModes; } }

        /// <summary>
        /// Gets a pin mode by index.
        /// </summary>
        protected PinMode GetPin(int index)
        {
            return pins[index]; 
        }
        
        /// <summary>
        /// Gets the name
        /// </summary>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Use this mode to create a configuration using the
        /// given addresses for the given connector.
        /// </summary>
        public ConnectorConfig CreateConfig(Connector connector, AddressList addresses, int subMode)
        {
            var pinConfigs = new PinConfig[PinCount];
            var pinOffset = GetPinOffset(subMode);
            for (int i = 0; i < PinCount; i++)
            {
                var pin = i + 1 + pinOffset;
                if (connector == Connector.Second)
                    pin += 8;
                pinConfigs[i] = new PinConfig(pin);
            }
            var result = new ConnectorConfig(pinConfigs);
            Configure(result, addresses, subMode);
            return result;
        }

        /// <summary>
        /// Gets the offset added to each pin number for the given submode.
        /// </summary>
        public virtual int GetPinOffset(int subMode)
        {
            return 0;
        }

        /// <summary>
        /// Use this mode to configure the given target.
        /// </summary>
        protected abstract void Configure(ConnectorConfig target, AddressList addresses, int subMode);
    }
}
