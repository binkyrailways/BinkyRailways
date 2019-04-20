using System;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// Exception during locking
    /// </summary>
    public class LockException : Exception
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public LockException(string message)
            : base(message)
        {

        }
        /// <summary>
        /// Default ctor
        /// </summary>
        public LockException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
