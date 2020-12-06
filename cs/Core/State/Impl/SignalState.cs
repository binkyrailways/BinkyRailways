using System.ComponentModel;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single signal.
    /// </summary>
    public abstract class SignalState : EntityState<ISignal>, ISignalState
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        protected SignalState(ISignal signal, RailwayState railwayState)
            : base(signal, railwayState)
        {
        }

        /// <summary>
        /// Update the output of this signal
        /// </summary>
        public abstract void Update();
    }

    /// <summary>
    /// State of a single signal.
    /// </summary>
    public abstract class SignalState<T> : SignalState
        where T : class, ISignal
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        protected SignalState(T signal, RailwayState railwayState)
            : base(signal, railwayState)
        {
        }

        /// <summary>
        /// Gets the entity model object
        /// </summary>
        [Browsable(false)]
        public new T Entity
        {
            get { return (T)base.Entity; }
        }
    }
}
