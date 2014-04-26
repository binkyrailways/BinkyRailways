using System;
using System.Globalization;

namespace BinkyRailways.Protocols.Ecos.Objects
{
    /// <summary>
    /// Feedback object (id=dynamic)
    /// </summary>
    public class FeedbackModule : Object
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public FeedbackModule(Client client, int id)
            : base(client, id)
        {
        }

        /// <summary>
        /// Gets the number of ports on this module
        /// </summary>
        public int GetPortCount()
        {
            var raw = Get(OptPorts);
            int value;
            if (!string.IsNullOrEmpty(raw) && int.TryParse(raw, out value))
                return value;
            return 0;
        }

        /// <summary>
        /// Gets the current state of this module.
        /// </summary>
        public int GetState() { return ParseState(Get(OptState)); }

        /// <summary>
        /// Parse a state value to a bit based number.
        /// </summary>
        protected static int ParseState(string raw)
        {
            if (string.IsNullOrEmpty(raw))
                return 0;
            if (!raw.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                return 0;
            raw = raw.Substring(2);
            return Convert.ToInt32(raw, 16);
        }
    }
}
