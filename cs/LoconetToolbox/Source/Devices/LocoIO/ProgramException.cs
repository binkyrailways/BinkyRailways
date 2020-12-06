using System;

namespace LocoNetToolBox.Devices.LocoIO
{
    /// <summary>
    /// Signals error during programming.
    /// </summary>
    public class ProgramException : Exception
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="message"></param>
        public ProgramException(string message) : base(message)
        {
            
        }
    }
}
