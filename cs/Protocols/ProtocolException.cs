using System;

namespace BinkyRailways.Protocols
{
    public class ProtocolException : Exception
    {
        public ProtocolException(string message)
            : base(message)
        {            
        }

        public ProtocolException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
