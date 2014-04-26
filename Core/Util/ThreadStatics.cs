using System;

namespace BinkyRailways.Core.Util
{
    /// <summary>
    /// Thread static shared instances.
    /// </summary>
    public static class ThreadStatics
    {
        [ThreadStatic] 
        private static Random random;

        /// <summary>
        /// Gets a thread static random instance.
        /// </summary>
        public static Random Random
        {
            get { return random ?? (random = new Random(Environment.TickCount)); }
        }
    }
}
