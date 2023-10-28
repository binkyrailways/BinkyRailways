using System;
using System.IO.Ports;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State.LocoNet;
using BinkyRailways.Core.Util;
using LocoNetToolBox.Protocol;

namespace BinkyRailways.Core.State.Impl.LocoBuffer
{
    /// <summary>
    /// Command station which drives a LocoBuffer
    /// </summary>
    public sealed partial class LocoBufferCommandStationState : CommandStationState, ILocoNetCommandStationState
    {
        /// <summary>
        /// LocoIO device has been found.
        /// </summary>
        public event EventHandler<PropertyEventArgs<ILocoIO>> LocoIOFound
        {
            add { client.LocoIOFound += value; }
            remove { client.LocoIOFound -= value; }
        }

        private LocoNetToolBox.Protocol.LocoBuffer lb;
        private readonly Client client;
        private bool networkIdle = false;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoBufferCommandStationState(ILocoBufferCommandStation entity, RailwayState railwayState, string[] addressSpaces)
            : base(entity, railwayState, addressSpaces)
        {
            var splb = new SerialPortLocoBuffer();
            splb.PortName = entity.ComPortName;
            lb = splb;
            lb.PreviewMessage += MessageProcessor;
            client = new Client(lb,railwayState, this, railwayState.Dispatcher, Log);
            client.Idle += (s, x) =>
            {
                networkIdle = true;
                RefreshIdle();
            };
        }

        /// <summary>
        /// Search for all LocoIO devices.
        /// </summary>
        public void QueryLocoIOs()
        {
            PostWorkerAction(() => client.QueryLocoIOs());   
        }

        /// <summary>
        /// Request to enable/disable the power on the track.
        /// </summary>
        protected override void OnRequestedPowerChanged(bool value)
        {
            var msg = value ? (Request) new GlobalPowerOn() : new GlobalPowerOff();
            msg.Execute(lb);
            Power.Actual = value;
        }

        /// <summary>
        /// Send the speed and direction of the given loc towards the railway.
        /// </summary>
        protected override void OnSendLocSpeedAndDirection(ILocState loc)
        {
            Log.Trace("OnSendLocSpeedAndDirection: {0}", loc);
            client.SendLocSpeedAndDirection(loc);
            client.SendLocDirectionAndFunctions(loc);
        }

        /// <summary>
        /// Send the direction of the given switch towards the railway.
        /// This method is called on my worker thread.
        /// </summary>
        protected override void OnSendSwitchDirection(ISwitchState @switch)
        {
            SendSwitchRequest(@switch.Address, @switch.Direction.Requested, @switch.Invert);
        }

        /// <summary>
        /// Send the position of the given turntable towards the railway.
        /// This method is called on my worker thread.
        /// </summary>
        protected override void OnSendTurnTablePosition(ITurnTableState turnTable)
        {
            Log.Trace("Send: TurnTable: position={0}", turnTable.Position.Requested);
            // Signal new address
            SendSwitchRequest(turnTable.WriteAddress, SwitchDirection.Off, turnTable.InvertWrite);
            // Calc address
            var position = turnTable.Position.Requested;
            foreach (var positionAddress in turnTable.PositionAddresses)
            {
                // Send 1 address bit
                var bit = ((position & 0x01) != 0) ? SwitchDirection.Straight : SwitchDirection.Off;
                SendSwitchRequest(positionAddress, bit, turnTable.InvertPositions);
                position >>= 1;
            }
            // New address complete
            SendSwitchRequest(turnTable.WriteAddress, SwitchDirection.Straight, turnTable.InvertWrite);

            // Wait for busy flag
            // TODO
        }

        /// <summary>
        /// Send the on/off value of a binary output.
        /// </summary>
        protected override void OnSendBinaryOutput(Address address, bool value)
        {
            Log.Trace("OnSendBinaryOutput: {0}", address);
            SendSwitchRequest(address, value ? SwitchDirection.Straight : SwitchDirection.Off, false);
        }

        /// <summary>
        /// Send the on/off value of a binary output.
        /// </summary>
        protected override void OnSendBinaryOutput(IBinaryOutputState output)
        {
            OnSendBinaryOutput(output.Address, output.Active.Requested);
        }

        /// <summary>
        /// Send the pattern of a 4-stage clock output.
        /// </summary>
        protected override void OnSendClock4StageOutput(IClock4StageOutputState output)
        {
            Log.Trace("OnSendClock4StageOutput: {0}", output.Address);
            var period = output.Period.Requested;
            var pattern = output.Pattern.Requested;
            foreach (var address in output.Addresses)
            {
                // Send 1 address bit
                var bit = ((pattern & 0x01) != 0) ? SwitchDirection.Straight : SwitchDirection.Off;
                SendSwitchRequest(address, bit, false);
                pattern >>= 1;
            }
            output.Pattern.Actual = pattern;
            output.Period.Actual = period;
        }

        /// <summary>
        /// Send the direction of the given switch towards the railway.
        /// This method is called on my worker thread.
        /// </summary>
        private void SendSwitchRequest(Address address, SwitchDirection direction, bool invertDirection)
        {
            Log.Trace("Send: SwitchRequest: address={0}, direction={1}", address.Value, direction);
            var msg = new SwitchRequest();
            msg.Address = address.ValueAsInt - 1;
            msg.Direction = (direction == SwitchDirection.Straight);
            if (invertDirection)
            {
                msg.Direction = !msg.Direction;
            }
            msg.Output = true;
            msg.Execute(lb);
        }

        /// <summary>
        /// Determine if this command station is idle.
        /// </summary>
        protected override bool IsIdle()
        {
            return base.IsIdle() && networkIdle;
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            if (!base.TryPrepareForUse(ui, statePersistence))
                return false;
            var splb = lb as SerialPortLocoBuffer;
            if (splb != null)
            {
                var portNames = SerialPort.GetPortNames();
                if (!portNames.Contains(splb.PortName))
                {
                    var portName = ui.ChooseComPortName(this);
                    if (portNames.Contains(portName))
                    {
                        // Now we have an available port name
                        splb.PortName = portName;
                    }
                    else
                    {
                        // No available port name provided, we cannot be used.
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public override void Dispose()
        {
            client.Dispose();
            ((IDisposable)lb).Dispose();
            base.Dispose();
        }

        /// <summary>
        /// Gets advanced info for the given loc
        /// </summary>
        internal override string GetLocInfo(ILocState loc)
        {
            return client.GetLocInfo(loc);
        }
    }
}
