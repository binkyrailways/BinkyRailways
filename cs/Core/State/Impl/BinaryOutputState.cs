using System.ComponentModel;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single output.
    /// </summary>
    public sealed class BinaryOutputState  : OutputState<IBinaryOutput>, IBinaryOutputState
    {
        private readonly StateProperty<bool> active;

        /// <summary>
        /// Default ctor
        /// </summary>
        public BinaryOutputState(IBinaryOutput output, RailwayState railwayState)
            : base(output, railwayState)
        {
            active = new StateProperty<bool>(this, false, null, OnActiveRequestedChanged, OnActiveActualChanged);
        }


        /// <summary>
        /// Is this output in the 'active' state?
        /// </summary>
        [DisplayName(@"Active")]
        public IStateProperty<bool> Active
        {
            get { return active; }
        }

        /// <summary>
        /// Requested state of active property has changed.
        /// </summary>
        private void OnActiveRequestedChanged(bool value)
        {
            CommandStation.SendBinaryOutput(this);
        }

        /// <summary>
        /// Active property has changed.
        /// </summary>
        private void OnActiveActualChanged(bool value)
        {
            Log.Info(Strings.OutputStateXY, Entity, value.OnOff());
        }
    }
}
