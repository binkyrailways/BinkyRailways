using BinkyRailways.Core.Logging;
using LocoNetToolBox.Protocol;
using NLog;

namespace BinkyRailways.Core.State.Impl.LocoBuffer
{
    /// <summary>
    /// Command station which drives a LocoBuffer
    /// </summary>
    partial class LocoBufferCommandStationState
    {
        private static readonly Logger lnLog = LogManager.GetLogger(LogNames.LocoNet);
        //private readonly Master lnMaster;

        /// <summary>
        /// Loconet message handler.
        /// </summary>
        /// <param name="message">The received message</param>
        /// <param name="decoded">The message in a decoded form (null if it cannot be decoded)</param>
        /// <returns>True if the message has been handled, false to pass the message to the next handler.</returns>
        private bool MessageProcessor(byte[] message, Message decoded)
        {
            // We're busy now
            networkIdle = false;
            RefreshIdle();

            // Decode the message
            var msg = Message.Decode(message);

            // Trace the message
            lnLog.Trace(() => "[" + Message.ToString(message) + "] " + msg);

            // Process the message
            return client.Process(msg);
        }
    }
}
