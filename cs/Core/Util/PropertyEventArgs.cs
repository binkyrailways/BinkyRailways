using System;

namespace BinkyRailways.Core.Util
{
    /// <summary>
    /// Event args that has 1 property value
    /// </summary>
    public class PropertyEventArgs<T> : EventArgs
    {
        private readonly T value;

        /// <summary>
        /// Default ctor
        /// </summary>
        public PropertyEventArgs(T value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets the property value
        /// </summary>
        public T Value
        {
            get { return value; }
        }
    }
}
