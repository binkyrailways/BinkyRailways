using System;

namespace BinkyRailways.Core.Util
{
    /// <summary>
    /// Event args carrying a single object.
    /// </summary>
    public class ObjectEventArgs<T> : EventArgs
    {
        private readonly T @object;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ObjectEventArgs(T o)
        {
            @object = o;
        }

        /// <summary>
        /// Gets the object.
        /// </summary>
        public T Object
        {
            get { return @object; }
        }
    }
}
