using System;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State.Impl;
using BinkyRailways.Core.Util;
using LocoNetToolBox.Protocol;
using NLog;

namespace BinkyRailways.Core.State.LocoNet
{
    /// <summary>
    /// Implementation of client behavior
    /// </summary>
    public partial class Client : IDisposable
    {
        /// <summary>
        /// LocoIO device has been found.
        /// </summary>
        public event EventHandler<PropertyEventArgs<ILocoIO>> LocoIOFound;

        private const int RequestSlotTimeout = 2500;

        private readonly Logger log;
        private readonly RailwayState railwayState;
        private readonly IStateDispatcher stateDispatcher;
        private readonly ICommandStationState cs;
        private readonly LocoBuffer lb;
        private readonly LocSlotMap locSlotMap = new LocSlotMap();
        private Visitor visitor;
        private readonly object transactionLock = new object();
        private DateTime lastPreviewMessageTime;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Client(LocoBuffer lb, RailwayState railwayState, ICommandStationState cs, IStateDispatcher stateDispatcher, Logger log)
        {
            if (lb == null)
                throw new ArgumentNullException("lb");
            if (cs == null)
                throw new ArgumentNullException("cs");
            if (stateDispatcher == null)
                throw new ArgumentNullException("stateDispatcher");
            this.railwayState = railwayState;
            this.lb = lb;
            this.log = log;
            this.stateDispatcher = stateDispatcher;
            this.cs = cs;
            StartIdleDetection();
        }

        /// <summary>
        /// Gets advanced info for the given loc
        /// </summary>
        internal string GetLocInfo(ILocState loc)
        {
            var slot = locSlotMap[loc];
            return (slot != null) ? slot.SlotNumber.ToString() : "-";
        }

        /// <summary>
        /// Gets the locobuffer we're using
        /// </summary>
        protected LocoBuffer LocoBuffer { get { return lb; } }

        /// <summary>
        /// Search for all LocoIO devices.
        /// </summary>
        public void QueryLocoIOs()
        {
            var msg = new PeerXferRequest1
            {
                Command = PeerXferRequest.Commands.Read,
                DestinationLow = 0,
                // DestinationHigh = 0,
                SvAddress = 0
            };
            msg.Execute(lb);
        }

        /// <summary>
        /// Send the speed and direction of the given loc towards the railway.
        /// </summary>
        /// <return>True on success</return>
        public bool SendLocSpeedAndDirection(ILocState loc)
        {
            log.Trace("OnSendLocSpeedAndDirection: {0}", loc);
            var slot = RequestSlot(loc);
            if (slot == null)
            {
                log.Error("No slot available for {0}", loc);
                return false;
            }

            // Send loc speed
            log.Trace("Send: LocoSpeedRequest: slot={0}, speed={1}", slot.SlotNumber, loc.Speed.Requested);
            var spdMsg = new LocoSpeedRequest(slot.SlotNumber, loc.SpeedInSteps.Requested);
            spdMsg.Execute(lb);
            slot.Touch();
            return true;
        }

        /// <summary>
        /// Send the loc direction and F1-F4 of the given loc towards the railway.
        /// </summary>
        /// <return>True on success</return>
        public bool SendLocDirectionAndFunctions(ILocState loc)
        {
            log.Trace("SendLocDirectionAndFunctions: {0}", loc);
            var slot = RequestSlot(loc);
            if (slot == null)
            {
                log.Error("No slot available for {0}", loc);
                return false;
            }

            // Send loc direction
            log.Trace("Send: LocoDirFuncRequest: slot={0}, direction={1}", slot.SlotNumber, loc.Direction.Requested);
            var forward = (loc.Direction.Requested == LocDirection.Forward);
            var f1 = loc.GetFunctionRequestedState(LocFunction.F1);
            var f2 = loc.GetFunctionRequestedState(LocFunction.F2);
            var f3 = loc.GetFunctionRequestedState(LocFunction.F3);
            var f4 = loc.GetFunctionRequestedState(LocFunction.F4);
            var dirMsg = new LocoDirFuncRequest(slot.SlotNumber,
            forward, loc.F0.Requested, f1, f2, f3, f4);
            dirMsg.Execute(lb);
            slot.Touch();
            return true;
        }

        /// <summary>
        /// Initiate the standard address selection procedure to get/create a slot for the given loc.
        /// </summary>
        protected Slot RequestSlot(ILocState loc)
        {
            var address = loc.Address.ValueAsInt;
            log.Trace("RequestSlot(addr={0})", address);

            lock (transactionLock)
            {
                var slot = locSlotMap[loc];
                if ((slot != null) && (slot.IsUpdate2Date))
                {
                    // Slot already available
                    return slot;
                }

                if (slot != null)
                {
                    // Slot may be out of date
                    log.Trace("Updating slot {0} for {1}", slot.SlotNumber, address);
                    try
                    {
                        var msg = new SlotDataRequest(slot.SlotNumber);
                        var response = msg.ExecuteAndWaitForResponse<SlotDataResponse>(
                            LocoBuffer,
                            x => (x.Address == address), RequestSlotTimeout);
                        if (response != null)
                        {
                            log.Trace("Updated slot {0} for {1}", slot.SlotNumber, address);
                            slot.Touch();
                            return slot;
                        }
                        log.Trace("Updating slot {0} for {1} failed. Slot is different.", slot.SlotNumber, address);
                        locSlotMap.Remove(slot);
                    }
                    catch (TimeoutException ex)
                    {
                        // Ignore for now, just claim a new slot
                        locSlotMap.Remove(slot);
                    }
                }

                log.Trace("Requesting slot for {0}", address);
                var req = new LocoAddressRequest(address);
                try
                {
                    // Perform the loco-address request
                    var response = req.ExecuteAndWaitForResponse<SlotDataResponse>(
                        LocoBuffer,
                        x => (x.Address == address), RequestSlotTimeout);
                    if (response == null)
                    {
                        log.Trace("Requesting slot for {0} failed: Timeout", address);
                        throw new TimeoutException();
                    }

                    // We now got a valid slot data response
                    slot = new Slot(response);
                    log.Trace("Requesting slot for {0} succeeded: got slot {1}", address, slot.SlotNumber);

                    // Get status
                    var usageStatus = response.Status1 & SlotStatus1.BusyActiveMask;
                    if ((usageStatus == SlotStatus1.InUse) || (response.Status1.IsSet(SlotStatus1.ConsistUp)))
                    {
                        // We're not allowed to use this slot
                        log.Trace("Slot status does not allow to use the slot: {0}", response.Status1);
                        return null;
                    }

                    // We can use the slot, perform a NULL move to set it in use.
                    log.Trace("Request NULL move for slot {0}, address {1}", slot.SlotNumber, address);
                    var msg = new MoveSlotsRequest(slot.SlotNumber, slot.SlotNumber);
                    var moveResponse = msg.ExecuteAndWaitForResponse<SlotDataResponse>(
                        LocoBuffer,
                        x => (x.Address == address), RequestSlotTimeout);
                    if (moveResponse == null)
                    {
                        log.Trace("NULL move for slot {0}, address {1} failed: timeout", slot.SlotNumber, address);
                        return null;
                    }

                    // We can now use the slot
                    log.Trace("Request NULL move for slot {0}, address {1} succeeded", slot.SlotNumber, address);
                    locSlotMap[loc] = slot;

                    // Set decoder type
                    var status = UpdateDecoderType(response.Status1, loc);
                    log.Trace("Write slot {0} stat1 {1} for address {2}", slot.SlotNumber, status, address);
                    var slotStat1Msg = new SlotStat1Request(response.Slot, status);
                    slotStat1Msg.Execute(lb);

                    return slot;
                }
                catch (TimeoutException)
                {
                    // No proper response
                    log.Trace("Timeout on slot request for {0}", address);
                    return null;
                }
            }
        }

        /// <summary>
        /// Process the given message.
        /// </summary>
        /// <returns>True if handled</returns>
        public virtual bool Process(Message msg)
        {
            if (msg == null)
                return false;
            if (visitor == null)
            {
                visitor = CreateVisitor();
            }
            lastPreviewMessageTime = DateTime.Now;
            return msg.Accept(visitor, this);
        }

        /// <summary>
        /// Create a message visitor.
        /// </summary>
        protected virtual Visitor CreateVisitor()
        {
            return new Visitor(stateDispatcher);
        }

        /// <summary>
        /// Update the status to reflect the correct decoder type and
        /// speed steps.
        /// </summary>
        private static SlotStatus1 UpdateDecoderType(SlotStatus1 status, ILocState loc)
        {
            var addressType = loc.Address.Type;
            status = status & ~SlotStatus1.DecoderTypeMask;
            // Motorola?
            if (addressType == AddressType.Motorola)
            {
                return status | SlotStatus1.DecoderType28Tri;
            }

            // DCC
            switch (loc.SpeedSteps)
            {
                case 14:
                    return status | SlotStatus1.DecoderType14;
                case 28:
                    return status | SlotStatus1.DecoderType28;
                default:
                    return status | SlotStatus1.DecoderType128;
            }
        }

        /// <summary>
        /// Handle the given message
        /// </summary>
        /// <returns>True if handled</returns>
        protected virtual bool ReceiveWriteSlotData(WriteSlotData msg)
        {
            log.Trace("Received: SetSlotData: slot={0}, address={1}", msg.Slot, msg.Address);
            var slot = locSlotMap[msg.Slot];
            if (slot == null)
            {
                // We do not know this slot, so we don't care about it.
                return true;
            }

            if (slot.Address != msg.Address)
            {
                // Slot now has a different address, do not use this slot any longer
                locSlotMap.Remove(slot);
                return true;
            }

            log.Trace("Updating slot: {0}, address={1}", msg.Slot, msg.Address);
            slot.Address = msg.Address;
            slot.DirF = msg.DirF;
            slot.Sound = msg.Sound;
            slot.Speed = msg.Speed;
            slot.Touch();
            UpdateLocFromSlot(slot);
            return true;
        }

        /// <summary>
        /// Handle the given message
        /// </summary>
        /// <returns>True if handled</returns>
        protected virtual bool ReceiveMoveSlotRequest(MoveSlotsRequest msg)
        {
            log.Trace("Received: MoveSlotRequest: src={0}, dst={1}", msg.SourceSlot, msg.DestinationSlot);

            // Special cases
            if (msg.SourceSlot == 0)
            {
                // Dispatch get
            }
            else if (msg.SourceSlot == msg.DestinationSlot)
            {
                // In use
            }
            else if (msg.DestinationSlot == 0)
            {
                // Dispatch put
            }
            else
            {
                // Normal move
            }

            return true;
        }

        /// <summary>
        /// Handle the given message
        /// </summary>
        /// <returns>True if handled</returns>
        protected virtual bool ReceiveLocoSpeed(LocoSpeedRequest msg)
        {
            log.Trace("Received: SetLocoSpeed: slot={0}, speed={1}", msg.Slot, msg.Speed);
            var slot = locSlotMap[msg.Slot];
            if (slot == null)
            {
                // We do not know this slot, so we don't care about it.
                return true;
            }

            // Update the slot
            slot.Speed = msg.Speed;
            slot.Touch();
            UpdateLocFromSlot(slot);
            return true;
        }

        /// <summary>
        /// Handle the given message
        /// </summary>
        /// <returns>True if handled</returns>
        protected virtual bool ReceiveLocoDirFunc(LocoDirFuncRequest msg)
        {
            log.Trace("Received: SetLocoDirFunc: slot={0}, dir={1}", msg.Slot, msg.Forward);
            var slot = locSlotMap[msg.Slot];
            if (slot == null)
            {
                // We do not know this slot, so we don't care about it.
                return true;
            }
            slot.DirF = msg.DirF;
            slot.Touch();
            UpdateLocFromSlot(slot);
            return true;
        }

        /// <summary>
        /// Handle the given message
        /// </summary>
        /// <returns>True if handled</returns>
        protected virtual bool ReceiveInputReport(InputReport msg)
        {
            // Sensor input
            var actual = msg.SensorLevel;
            var actualDir = msg.SensorLevel ? SwitchDirection.Straight : SwitchDirection.Off;
            var invertedActualDir = msg.SensorLevel ? SwitchDirection.Off : SwitchDirection.Straight;
            var address = msg.Address + 1; // Fixup
            log.Trace("Received: InputReport: address={0}, level={1}", address, actual);
            stateDispatcher.PostAction(() =>
            {
                var foundInput = false;
                foreach (var input in cs.Inputs.Where(x => x.Address.ValueAsInt == address))
                {
                    input.Active.Actual = actual;
                    foundInput = true;
                }
                var foundSwitch = false;
                if (!foundInput)
                {
                    // The address does not match any of the sensors, perhaps it matches some of the switches?
                    foreach (var @switch in cs.Junctions.OfType<ISwitchState>().Where(x => x.HasFeedback && (x.FeedbackAddress.ValueAsInt == address)))
                    {
                        @switch.Direction.Actual = @switch.InvertFeedback ? invertedActualDir : actualDir;
                        foundSwitch = true;
                    }
                }
                if (!(foundInput || foundSwitch))
                {
                    railwayState.OnUnknownSensor(new Address(AddressType.LocoNet, string.Empty, address));
                    log.Info(Strings.InputReportForUnknownAddress, actual.OnOff(),
                             address);
                }
            });
            return true;
        }

        /// <summary>
        /// Handle the given message
        /// </summary>
        /// <returns>True if handled</returns>
        protected virtual bool ReceiveSwitchReport(SwitchReport msg)
        {
            var actual = msg.SensorLevel ? SwitchDirection.Straight : SwitchDirection.Off;
            var invertedActual = msg.SensorLevel ? SwitchDirection.Off : SwitchDirection.Straight;
            var address = msg.Address;
            log.Trace("Received: SwitchReport: address={0}, level={1}", address, actual);
            stateDispatcher.PostAction(() =>
            {
                var foundSwitch = false;
                foreach (var @switch in cs.Junctions.OfType<ISwitchState>().Where(x => x.HasFeedback && (x.FeedbackAddress.ValueAsInt == address)))
                {
                    @switch.Direction.Actual = @switch.InvertFeedback ? invertedActual : actual;
                    foundSwitch = true;
                }
                if (!foundSwitch)
                {
                    railwayState.OnUnknownSwitch(new Address(AddressType.LocoNet, string.Empty, address));
                    log.Info(Strings.SwitchReportForUnknownAddress, actual, address);
                }
            });
            return true;
        }

        /// <summary>
        /// Handle the given message
        /// </summary>
        /// <returns>True if handled</returns>
        protected virtual bool ReceivePeerXferResponse(PeerXferResponse msg)
        {
            if (msg.SvAddress == 0)
            {
                if (!msg.IsSourcePC)
                {
                    var entry = new LocoIO(msg, lb);
                    LocoIOFound.Fire(this, new PropertyEventArgs<ILocoIO>(entry));
                }
            }
            return true;
        }

        /// <summary>
        /// Put the data from this slot into the corresponding loc state.
        /// </summary>
        protected void UpdateLocFromSlot(Slot slot)
        {
            var loc = locSlotMap.GetLoc(slot.SlotNumber);
            if (loc != null)
            {
                stateDispatcher.PostAction(() => UpdateLocFromSlot(slot, loc));
            }
        }

        /// <summary>
        /// Put the data from this slot into the loc state.
        /// </summary>
        public void UpdateLocFromSlot(Slot slot, ILocState l)
        {
            var spd = slot.Speed;
            l.SpeedInSteps.Actual = (spd == 1) ? 0 : spd;
            var dirf = slot.DirF;
            l.Direction.Actual = dirf.IsSet(DirFunc.Direction) ? LocDirection.Reverse : LocDirection.Forward;
            l.F0.Actual = dirf.IsSet(DirFunc.F0);
            l.SetFunctionActualState(LocFunction.F1, dirf.IsSet(DirFunc.F1));
            l.SetFunctionActualState(LocFunction.F2, dirf.IsSet(DirFunc.F2));
            l.SetFunctionActualState(LocFunction.F3, dirf.IsSet(DirFunc.F3));
            l.SetFunctionActualState(LocFunction.F4, dirf.IsSet(DirFunc.F4));
            var snd = slot.Sound;
            l.SetFunctionActualState(LocFunction.F5, snd.IsSet(Sound.Snd1));
            l.SetFunctionActualState(LocFunction.F6, snd.IsSet(Sound.Snd2));
            l.SetFunctionActualState(LocFunction.F7, snd.IsSet(Sound.Snd3));
            l.SetFunctionActualState(LocFunction.F8, snd.IsSet(Sound.Snd4));
        }

        /// <summary>
        /// Put the data from the loc state into this slot.
        /// </summary>
        protected void UpdateSlotFromLoc(Slot slot, ILocState l)
        {
            slot.Address = l.Address.ValueAsInt;
            var spd = l.SpeedInSteps.Actual;
            slot.Speed = (spd == 1) ? 2 : spd;
            var dirf = DirFunc.None;
            if (l.Direction.Actual == LocDirection.Reverse) dirf |= DirFunc.Direction;
            if (l.F0.Actual) dirf |= DirFunc.F0;
            if (l.GetFunctionActualState(LocFunction.F1)) dirf |= DirFunc.F1;
            if (l.GetFunctionActualState(LocFunction.F2)) dirf |= DirFunc.F2;
            if (l.GetFunctionActualState(LocFunction.F3)) dirf |= DirFunc.F3;
            if (l.GetFunctionActualState(LocFunction.F4)) dirf |= DirFunc.F4;
            slot.DirF = dirf;
            var snd = Sound.None;
            if (l.GetFunctionActualState(LocFunction.F5)) snd |= Sound.Snd1;
            if (l.GetFunctionActualState(LocFunction.F6)) snd |= Sound.Snd2;
            if (l.GetFunctionActualState(LocFunction.F7)) snd |= Sound.Snd3;
            if (l.GetFunctionActualState(LocFunction.F8)) snd |= Sound.Snd4;
            slot.Sound = snd;
        }

        /// <summary>
        /// Reset all loc-slot mappings
        /// </summary>
        protected void Reset()
        {
            //locSlotMap.Clear();
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            StopIdleDetection();
            LocoIOFound = null;
            Idle = null;
        }

        /// <summary>
        /// Message visitor
        /// </summary>
        protected class Visitor : MessageVisitor<bool, Client>
        {
            private readonly IStateDispatcher stateDispatcher;

            /// <summary>
            /// Default ctor
            /// </summary>
            public Visitor(IStateDispatcher stateDispatcher)
            {
                this.stateDispatcher = stateDispatcher;
            }

            /// <summary>
            /// Detect power off
            /// </summary>
            public override bool Visit(GlobalPowerOff msg, Client data)
            {
                data.Reset();
                stateDispatcher.PostAction(() => data.cs.Power.Actual = false);
                return true;
            }

            /// <summary>
            /// Detect power on
            /// </summary>
            public override bool Visit(GlobalPowerOn msg, Client data)
            {
                data.Reset();
                stateDispatcher.PostAction(() => data.cs.Power.Actual = true);
                return true;
            }

            /// <summary>
            /// Move slots
            /// </summary>
            public override bool Visit(MoveSlotsRequest msg, Client data)
            {
                return data.ReceiveMoveSlotRequest(msg);
            }

            /// <summary>
            /// Write slot data
            /// </summary>
            public override bool Visit(WriteSlotData msg, Client data)
            {
                return data.ReceiveWriteSlotData(msg);
            }

            /// <summary>
            /// Loc set speed
            /// </summary>
            public override bool Visit(LocoSpeedRequest msg, Client data)
            {
                return data.ReceiveLocoSpeed(msg);
            }

            /// <summary>
            /// Loc set direction and functions
            /// </summary>
            public override bool Visit(LocoDirFuncRequest msg, Client data)
            {
                return data.ReceiveLocoDirFunc(msg);
            }

            /// <summary>
            /// Feedback report
            /// </summary>
            public override bool Visit(InputReport msg, Client data)
            {
                return data.ReceiveInputReport(msg);
            }

            /// <summary>
            /// Switch feedback
            /// </summary>
            public override bool Visit(SwitchReport msg, Client data)
            {
                return data.ReceiveSwitchReport(msg);
            }

            /// <summary>
            /// Peer transfer.
            /// </summary>
            public override bool Visit(PeerXferResponse msg, Client data)
            {
                return data.ReceivePeerXferResponse(msg);
            }
        }
    }
}
