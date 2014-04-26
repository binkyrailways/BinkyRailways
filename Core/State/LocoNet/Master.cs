using System;
using System.Linq;
using LocoNetToolBox.Protocol;
using MyModelRailway.Core.Model;
using MyModelRailway.Core.State.Impl;
using MyModelRailway.Core.Util;
using NLog;

namespace MyModelRailway.Core.State.LocoNet
{
    /// <summary>
    /// Implementation of Master behavior
    /// </summary>
    public class Master
    {
        private const int RequestSlotTimeout = 2500;

        private readonly Logger log;
        private readonly RailwayState railwayState;
        private readonly IStateDispatcher stateDispatcher;
        private readonly ICommandStationState cs;
        private readonly LocoBuffer lb;
        private readonly SlotTable slotTable;
        private Visitor visitor;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Master(LocoBuffer lb, RailwayState railwayState, ICommandStationState cs, IStateDispatcher stateDispatcher, Logger log)
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
            slotTable = new SlotTable(cs);
        }

        /// <summary>
        /// Gets advanced info for the given loc
        /// </summary>
        internal string GetLocInfo(ILocState loc)
        {
            var slot = slotTable.FindByAddress(loc.Entity.Address.Value, false);
            return (slot != null) ? slot.SlotNumber.ToString() : "-";
        }

        /// <summary>
        /// Gets the slots used
        /// </summary>
        protected SlotTable Slots { get { return slotTable; } }

        /// <summary>
        /// Gets the locobuffer we're using
        /// </summary>
        protected LocoBuffer LocoBuffer { get { return lb; } }

        /// <summary>
        /// Initiate the standard address selection procedure to get/create a slot for the given loc.
        /// </summary>
        public Slot RequestSlot(ILocState loc)
        {
            var slot = Slots.FindByLoc(loc);
            if (slot != null)
            {
                // Slot already available
                return slot;
            }

            var address = loc.Entity.Address.Value;
            log.Trace("Requesting slot for {0}", address);
            var req = new LocoAddressRequest(address);
            try
            {
                req.ExecuteAndWaitForResponse<SlotDataResponse>(
                    LocoBuffer,
                    x => (x.Address == address), RequestSlotTimeout);
            }
            catch (TimeoutException)
            {
                // No proper response
                log.Trace("Timeout on slot request for {0}", address);
                return null;
            }
            var result = Slots.FindByAddress(address, false);
            if (result == null)
            {
                log.Error("Requesting slot succeeded, but no slot found for {0}", address);
            }
            else
            {
                log.Trace("Requested slot for {0}, using slot {1}", address, result.SlotNumber);
            }
            return result;
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
        /// Send a slot data response for the given slot.
        /// </summary>
        protected void SendSlotDataResponse(Slot slot, int slotNumber)
        {
            var msg = new SlotDataResponse(slotNumber);
            if (slot != null)
            {
                log.Trace("Send: SlotDataResponse: slot={0}, address={1}", slotNumber, slot.Address);
                msg.Status1 = SlotStatus1.InUse;
                msg.Address = slot.Address;
                msg.DirF = slot.DirF;
                msg.Speed = slot.Speed;
                msg.Sound = slot.Sound;
                // TODO fill other properties
            }
            else
            {
                // Fill "slot free" properties
                log.Trace("Send: SlotDataResponse: slot={0}, free", slotNumber);
                msg.Status1 = SlotStatus1.None;
            }
            msg.Execute(lb);
        }

        /// <summary>
        /// Handle the given message
        /// </summary>
        /// <returns>True if handled</returns>
        protected virtual bool LocoAddress(LocoAddressRequest msg)
        {
            log.Trace("Received: LocoAddress: address={0}", msg.Address);
            var slot = slotTable.FindByAddress(msg.Address, true);
            if (slot == null)
            {
                // No slots available
                log.Trace("Send: LongAck: no slots available", msg.Address);
                var result = new LongAck(LocoAddressRequest.Opcode, 0);
                result.Execute(lb);
                return true;
            }
            // Normal response
            SendSlotDataResponse(slot, slot.SlotNumber);
            return true;
        }

        /// <summary>
        /// Handle the given message
        /// </summary>
        /// <returns>True if handled</returns>
        protected virtual bool GetSlotData(SlotDataRequest msg)
        {
            log.Trace("Received: GetSlotData: slot={0}", msg.Slot);
            var slot = slotTable.FindBySlotNumber(msg.Slot, false, -1);
            SendSlotDataResponse(slot, msg.Slot);
            return true;
        }

        /// <summary>
        /// Handle the given message
        /// </summary>
        /// <returns>True if handled</returns>
        protected virtual bool SetSlotData(WriteSlotData msg)
        {
            log.Trace("Received: SetSlotData: slot={0}, address={1}", msg.Slot, msg.Address);
            var slot = slotTable.FindBySlotNumber(msg.Slot, true, msg.Address);
            if (slot == null)
            {
                log.Trace("Unknown slot: {0}", msg.Slot);
                return false;
            }
            log.Trace("Updating slot: {0}, address={1}", msg.Slot, msg.Address);
            slot.Address = msg.Address;
            slot.DirF = msg.DirF;
            slot.Sound = msg.Sound;
            slot.Speed = msg.Speed;
            stateDispatcher.PostAction(slot.SlotUpdated);
            return true;
        }

        /// <summary>
        /// Handle the given message
        /// </summary>
        /// <returns>True if handled</returns>
        protected virtual bool MoveSlots(MoveSlotsRequest msg)
        {
            log.Trace("Received: MoveSlots: src={0}, dst={1}", msg.SourceSlot, msg.DestinationSlot);
            return false;
        }

        /// <summary>
        /// Handle the given message
        /// </summary>
        /// <returns>True if handled</returns>
        protected virtual bool SetLocoSpeed(LocoSpeedRequest msg)
        {
            log.Trace("Received: SetLocoSpeed: slot={0}, speed={1}", msg.Slot, msg.Speed);
            var slot = slotTable.FindBySlotNumber(msg.Slot, false, -1);
            if (slot == null)
                return false;
            slot.Speed = msg.Speed;
            stateDispatcher.PostAction(slot.SlotUpdated);
            return true;
        }

        /// <summary>
        /// Handle the given message
        /// </summary>
        /// <returns>True if handled</returns>
        protected virtual bool SetLocoDirFunc(LocoDirFuncRequest msg)
        {
            log.Trace("Received: SetLocoDirFunc: slot={0}, dir={1}", msg.Slot, msg.Direction);
            var slot = slotTable.FindBySlotNumber(msg.Slot, false, -1);
            if (slot == null)
                return false;
            slot.Direction = msg.Direction;
            slot.F0 = msg.F0;
            slot.F1 = msg.F1;
            slot.F2 = msg.F2;
            slot.F3 = msg.F3;
            slot.F4 = msg.F4;
            stateDispatcher.PostAction(slot.SlotUpdated);
            return true;
        }

        /// <summary>
        /// Handle the given message
        /// </summary>
        /// <returns>True if handled</returns>
        protected virtual bool InputReport(InputReport msg)
        {
            // Sensor input
            var actual = msg.SensorLevel;
            var address = msg.Address + 1; // Fixup
            log.Trace("Received: InputReport: address={0}, level={1}", address, actual);
            stateDispatcher.PostAction(() =>
            {
                var foundSensor = false;
                foreach (var sensor in cs.Sensors.Where(x => x.Entity.Address.Value == address))
                {
                    sensor.Active.Actual = actual;
                    foundSensor = true;
                }
                if (!foundSensor)
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
        protected virtual bool SwitchReport(SwitchReport msg)
        {
            var actual = msg.SensorLevel ? SwitchDirection.Straight : SwitchDirection.Off;
            var invertedActual = msg.SensorLevel ? SwitchDirection.Off : SwitchDirection.Straight;
            var address = msg.Address;
            log.Trace("Received: SwitchReport: address={0}, level={1}", address, actual);
            stateDispatcher.PostAction(() =>
            {
                var foundSwitch = false;
                foreach (var @switch in cs.Junctions.OfType<ISwitchState>().Where(x => x.Entity.Address.Value == address))
                {
                    @switch.Direction.Actual = @switch.Entity.Invert ? invertedActual : actual;
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
        /// Message visitor
        /// </summary>
        protected class Visitor : MessageVisitor<bool, Master>
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
            public override bool Visit(GlobalPowerOff msg, Master data)
            {
                stateDispatcher.PostAction(() => data.cs.Power.Actual = false);
                return true;
            }

            /// <summary>
            /// Detect power on
            /// </summary>
            public override bool Visit(GlobalPowerOn msg, Master data)
            {
                stateDispatcher.PostAction(() => data.cs.Power.Actual = true);
                return true;
            }

            /// <summary>
            /// Loc address request
            /// </summary>
            public override bool Visit(LocoAddressRequest msg, Master data)
            {
                return data.LocoAddress(msg);
            }

            /// <summary>
            /// Move slots
            /// </summary>
            public override bool Visit(MoveSlotsRequest msg, Master data)
            {
                return data.MoveSlots(msg);
            }

            /// <summary>
            /// Slot data request
            /// </summary>
            public override bool Visit(SlotDataRequest msg, Master data)
            {
                return data.GetSlotData(msg);
            }

            /// <summary>
            /// Write slot data
            /// </summary>
            public override bool Visit(WriteSlotData msg, Master data)
            {
                return data.SetSlotData(msg);
            }

            /// <summary>
            /// Loc set speed
            /// </summary>
            public override bool Visit(LocoSpeedRequest msg, Master data)
            {
                return data.SetLocoSpeed(msg);
            }

            /// <summary>
            /// Loc set direction and functions
            /// </summary>
            public override bool Visit(LocoDirFuncRequest msg, Master data)
            {
                return data.SetLocoDirFunc(msg);
            }

            /// <summary>
            /// Feedback report
            /// </summary>
            public override bool Visit(InputReport msg, Master data)
            {
                return data.InputReport(msg);
            }

            /// <summary>
            /// Switch feedback
            /// </summary>
            public override bool Visit(SwitchReport msg, Master data)
            {
                return data.SwitchReport(msg);
            }
        }
    }
}
