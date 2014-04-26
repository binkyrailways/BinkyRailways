using System;

namespace LocoNetToolBox.Devices
{
    /// <summary>
    /// Eventhandler extensions
    /// </summary>
    public static class EventExtensions 
    {
        /// <summary>
        /// Fire if not null
        /// </summary>
        public static void Fire(this EventHandler handler, object sender)
        {
            if (handler != null)
            {
                handler(sender, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Fire if not null
        /// </summary>
        public static void Fire<T>(this EventHandler<T> handler, object sender, T args)
            where T : EventArgs
        {
            if (handler != null)
            {
                handler(sender, args);
            }
        }
    }
}
