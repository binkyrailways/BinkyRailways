using System;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl.Virtual
{
    /// <summary>
    /// Command station which is used in virtual mode
    /// </summary>
    public sealed class VirtualCommandStationState : CommandStationState
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public VirtualCommandStationState(IVirtualCommandStation entity, RailwayState railwayState)
            : base(entity, railwayState, null)
        {
        }

        /// <summary>
        /// Request to enable/disable the power on the track.
        /// </summary>
        protected override void OnRequestedPowerChanged(bool value)
        {
            PostWorkerDelayedAction(TimeSpan.FromMilliseconds(500), () =>
            {
                Power.Actual = value;
                RefreshIdle();
            });
        }

        /// <summary>
        /// Send the speed and direction of the given loc towards the railway.
        /// </summary>
        protected override void OnSendLocSpeedAndDirection(ILocState loc)
        {
            Log.Trace("OnSendLocSpeedAndDirection: {0}", loc);
            loc.Direction.Actual = loc.Direction.Requested;
            loc.Speed.Actual = loc.Speed.Requested;
        }

        /// <summary>
        /// Send the direction of the given switch towards the railway.
        /// This method is called on my worker thread.
        /// </summary>
        protected override void OnSendSwitchDirection(ISwitchState @switch)
        {
            // Do nothing here, the base implementation sets actual after a delay
            Log.Trace("OnSendSwitchDirection: {0}", @switch);
        }

        /// <summary>
        /// Send the position of the given turntable towards the railway.
        /// This method is called on my worker thread.
        /// </summary>
        protected override void OnSendTurnTablePosition(ITurnTableState turnTable)
        {
            Log.Trace("OnSendTurnTablePosition: {0}", turnTable);
            turnTable.Position.Actual = turnTable.Position.Requested;
        }

        /// <summary>
        /// Send the on/off value of a binary output.
        /// </summary>
        protected override void OnSendBinaryOutput(IBinaryOutputState output)
        {
            Log.Trace("OnSendBinaryOutput: {0}", output);
            output.Active.Actual = output.Active.Requested;
        }

        /// <summary>
        /// Send the pattern of a 4-stage clock output.
        /// </summary>
        protected override void OnSendClock4StageOutput(IClock4StageOutputState output)
        {
            Log.Trace("OnSendClock4StageOutput: {0}", output);
            output.Pattern.Actual = output.Pattern.Requested;
            output.Period.Actual = output.Period.Requested;
        }

        /// <summary>
        /// Send the on/off value of a binary output.
        /// </summary>
        protected override void OnSendBinaryOutput(Address address, bool value)
        {
            Log.Trace("OnSendBinaryOutput: {0}", address);
            // Do nothing
        }

        /// <summary>
        /// Gets advanced info for the given loc
        /// </summary>
        internal override string GetLocInfo(ILocState loc)
        {
            return string.Empty;
        }
    }
}
