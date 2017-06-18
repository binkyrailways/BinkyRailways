namespace LocoNetToolBox.Devices.LocoIO
{
    /// <summary>
    /// Port address list.
    /// </summary>
    public sealed class AddressList
    {
        private readonly int[] list;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal AddressList(int length)
        {
            list = new int[length];
            for (int i = 0; i < length; i++)
            {
                list[i] = i + 1;
            }
        }

        /// <summary>
        /// Gets number of elements in this list.
        /// </summary>
        public int Length
        {
            get { return list.Length; }
        }

        /// <summary>
        /// Gets/sets address at given index.
        /// </summary>
        public int this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }
    }
}
