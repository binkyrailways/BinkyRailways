using System;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// Dispatcher used to control concurrency for all messages that alter the state
    /// of a railway.
    /// </summary>
    public interface IStateDispatcher : IDisposable
    {
        /// <summary>
        /// Post the given action on the dispatch action list.
        /// The dispatcher will then perform the action on the dispatch thread.
        /// </summary>
        void PostAction(Action action);

        /// <summary>
        /// Post the given delayed action on the dispatch action list.
        /// The action will be performed at the given time.
        /// </summary>
        void PostDelayedAction(DateTime time, Action action);

        /// <summary>
        /// Post the given delayed action on the dispatch action list.
        /// The action will be performed after given timeout.
        /// </summary>
        void PostDelayedAction(TimeSpan timeout, Action action);

        /// <summary>
        /// Post the given action on the dispatcher thread and wait for the result.
        /// If the current thread is the dispatcher thread, then the action is run right away.
        /// </summary>
        T PostActionAndWait<T>(Func<T> action);
    }
}
