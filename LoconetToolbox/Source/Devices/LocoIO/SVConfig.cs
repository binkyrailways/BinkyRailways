using System;

namespace LocoNetToolBox.Devices.LocoIO
{
    /// <summary>
    /// Configuration of a single SV.
    /// </summary>
    public sealed class SVConfig : IComparable<SVConfig>
    {
        public event EventHandler ValueChanged;

        private byte value;

        /// <summary>
        /// Default ctor
        /// </summary>
        public SVConfig(int index)
        {
            this.Index = index;
        }

        /// <summary>
        /// SV index (1..)
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// SV value
        /// </summary>
        public byte Value
        {
            get { return value; }
            set { if (this.value != value) { this.value = value; ValueChanged.Fire(this); } }
        }

        /// <summary>
        /// Is the given value valid
        /// </summary>
        public bool Valid { get; set; }

        /// <summary>
        /// Compare on index
        /// </summary>
        public int CompareTo(SVConfig other)
        {
            if (this.Index < other.Index) { return -1; }
            if (this.Index > other.Index) { return 1; }
            return 0;
        }
    }
}
