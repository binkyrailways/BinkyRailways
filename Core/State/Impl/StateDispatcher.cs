namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// Dispatcher used to control concurrency for all messages that alter the state
    /// of a railway.
    /// </summary>
    public sealed class StateDispatcher : AsynchronousWorker, IStateDispatcher
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal StateDispatcher() : base("StateDispatcher")
        {
        }
    }
}
