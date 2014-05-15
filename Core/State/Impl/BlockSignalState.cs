using System;
using System.Collections.Generic;
using System.ComponentModel;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a block signal.
    /// </summary>
    public sealed class BlockSignalState  : SignalState<IBlockSignal>, IBlockSignalState, IInitializeAtPowerOn
    {
        private readonly Address address1;
        private readonly Address address2;
        private readonly Address address3;
        private readonly Address address4;
        private IBlockState block;
        private readonly StateProperty<BlockSignalColor> color;
        private bool initializeColorNeeded;
        private CommandStationState commandStation;

        /// <summary>
        /// Default ctor
        /// </summary>
        public BlockSignalState(IBlockSignal entity, RailwayState railwayState)
            : base(entity, railwayState)
        {
            address1 = entity.Address1;
            address2 = (address1 != null) ? entity.Address2: null;
            address3 = (address2 != null) ? entity.Address3 : null;
            address4 = (address3 != null) ? entity.Address4 : null;
            color = new StateProperty<BlockSignalColor>(this, BlockSignalColor.Red, ValidateColor, OnRequestedColorChanged, null);
        }

        /// <summary>
        /// Addresses used by the signal (0..4)
        /// Lowest bit comes first.
        /// This is an output signal.
        /// </summary>
        [DisplayName(@"Signal addresses")]
        public IEnumerable<Address> Addresses
        {
            get
            {
                if (address1 == null)
                    yield break;
                yield return address1;
                if (address2 == null)
                    yield break;
                yield return address2;
                if (address3 == null)
                    yield break;
                yield return address3;
                if (address4 == null)
                    yield break;
                yield return address4;
            }
        }

        /// <summary>
        /// The block this signal covers
        /// </summary>
        public IBlockState Block { get { return block; } }

        /// <summary>
        /// Type of signal
        /// </summary>
        public BlockSignalType Type { get { return Entity.Type; } }

        /// <summary>
        /// Which side of the block is the signal facing.
        /// </summary>
        public BlockSide Position { get { return Entity.Position; } }

        /// <summary>
        /// Color of the signal.
        /// </summary>
        public IStateProperty<BlockSignalColor> Color { get { return color; } }

        /// <summary>
        /// Gets the next color that is supported by my entity.
        /// </summary>
        public BlockSignalColor GetNextColor(BlockSignalColor current)
        {
            var entity = Entity;
            switch (current)
            {
                case BlockSignalColor.Red:
                    if (entity.IsYellowAvailable)
                        return BlockSignalColor.Yellow;
                    return GetNextColor(BlockSignalColor.Yellow);
                case BlockSignalColor.Yellow:
                    if (entity.IsGreenAvailable)
                        return BlockSignalColor.Green;
                    return GetNextColor(BlockSignalColor.Green);
                case BlockSignalColor.Green:
                    if (entity.IsWhiteAvailable)
                        return BlockSignalColor.White;
                    return GetNextColor(BlockSignalColor.White);
                case BlockSignalColor.White:
                    if (entity.IsRedAvailable)
                        return BlockSignalColor.Red;
                    return GetNextColor(BlockSignalColor.Red);
                default:
                    return BlockSignalColor.Red;
            }
        }

        /// <summary>
        /// Calculate and set the color according to the block state.
        /// </summary>
        public override void Update()
        {
            Color.Requested = CalculateColor();
        }

        /// <summary>
        /// Calculate the color according to the block state.
        /// </summary>
        private BlockSignalColor CalculateColor()
        {
            // Without a block we cannot do anything
            if (block == null)
                return BlockSignalColor.Red;
            // No lock, then do not use the block
            var loc = block.LockedBy;
            if ((loc == null) || (!loc.ControlledAutomatically.Actual))
                return BlockSignalColor.Red;

            // No route for the loc, do not use the block
            var route = loc.CurrentRoute.Actual;
            if (route == null)
                return BlockSignalColor.Red;

            // Check automatic state of loc
            switch (loc.AutomaticState.Actual)
            {
                case AutoLocState.WaitingForAssignedRouteReady:
                case AutoLocState.AssignRoute:
                case AutoLocState.ReversingWaitingForDirectionChange:
                case AutoLocState.WaitingForDestinationTimeout:
                case AutoLocState.WaitingForDestinationGroupMinimum:
                    // Loc is waiting
                    return BlockSignalColor.Red;
                case AutoLocState.EnteringDestination:
                    // Entering destination of route
                    if (route.Route.From == block)
                        return BlockSignalColor.Red;
                    if ((route.Route.To == block) && (Type == BlockSignalType.Entry))
                        return BlockSignalColor.Red;
                    break;
            }

            // Is block part of current route?
            var blockIsPartOfRoute = (route.Route.To == block) || (route.Route.From == block);
            if (blockIsPartOfRoute)
            {
                // Check position compared to direction of route
                if (!IsPositionMatchingRoute(route.Route))
                    return BlockSignalColor.Red;
            }

            // Is block part of next route?
            var nextRoute = loc.NextRoute.Actual;
            var blockIsPartOfNextRoute = (nextRoute != null) && ((nextRoute.To == block));
            if (!(blockIsPartOfRoute || blockIsPartOfNextRoute))
                return BlockSignalColor.Red;            

            if (Type == BlockSignalType.Entry)
            {
                // Entry signal.
                // We know that there is a loc with a current route in/towards the block.

                // Slow down if there is no next route
                if (!loc.HasNextRoute())
                    return BlockSignalColor.Yellow;

                // Be carefull if there are non-straight switched in the route
                if (blockIsPartOfRoute && route.Route.HasNonStraightSwitches)
                    return BlockSignalColor.White;

                // All ok
                return BlockSignalColor.Green;
            }
            
            // Exit signal
            // We know that there is a loc with a current route in/towards the block.

            // We're not going directly from or to this block.
            if (!blockIsPartOfRoute)
                return BlockSignalColor.Red;            

            // Be carefull if there are non-straight switched in the route
            if (blockIsPartOfRoute && route.Route.HasNonStraightSwitches)
                return BlockSignalColor.White;

            // All ok
            return BlockSignalColor.Green;
        }

        /// <summary>
        /// Is the position of this block matching the direction of the route.
        /// </summary>
        private bool IsPositionMatchingRoute(IRouteState route)
        {
            var block = Block;
            if (block == route.From)
            {
                if (Type == BlockSignalType.Entry)
                    return false;
                return (Position == route.FromBlockSide);
            }
            if (block == route.To)
            {
                if (Type == BlockSignalType.Exit)
                    return (Position == route.ToBlockSide.Invert());
                return (Position == route.ToBlockSide);
            }
            // Unknown
            return false;
        }

        /// <summary>
        /// Forward position request to command station
        /// </summary>
        private void OnRequestedColorChanged(BlockSignalColor value)
        {
            var cs = commandStation;
            if (cs != null)
            {
                var pattern = GetPattern(value);
                foreach (var address in Addresses)
                {
                    cs.SendBinaryOutput(address, (pattern & 0x01) != 0);
                    pattern >>= 1;
                }
                color.Actual = color.Requested;
            }
        }

        /// <summary>
        /// Gets the bit pattern to use for the given color.
        /// </summary>
        private int GetPattern(BlockSignalColor color)
        {
            var entity = Entity;
            switch (color)
            {
                case BlockSignalColor.Red:
                    return entity.RedPattern;
                case BlockSignalColor.Green:
                    return entity.GreenPattern;
                case BlockSignalColor.Yellow:
                    return entity.IsYellowAvailable ? entity.YellowPattern : GetPattern(BlockSignalColor.Green);
                case BlockSignalColor.White:
                    return entity.IsWhiteAvailable ? entity.WhitePattern : GetPattern(BlockSignalColor.Yellow);
                default:
                    throw new ArgumentException("Unknown color: " + color);
            }
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            // Resolve block.
            var entity = Entity;
            block = (entity.Block != null) ? RailwayState.BlockStates[entity.Block] : null;
            if (block == null)
                return false;

            // Resolve command station
            var cs = RailwayState.SelectCommandStation(Entity);
            if (cs == null)
                return false;

            commandStation = cs;
            commandStation.AddSignal(this);
            return true;
        }

        /// <summary>
        /// Used for ordering initialization called.
        /// </summary>
        InitializationPriority IInitializeAtPowerOn.Priority
        {
            get { return InitializationPriority.Signal; }
        }

        /// <summary>
        /// Validate the given color with what is actually supported.
        /// </summary>
        private BlockSignalColor ValidateColor(BlockSignalColor c)
        {
            switch (c)
            {
                case BlockSignalColor.Yellow:
                    if (!Entity.IsYellowAvailable) return BlockSignalColor.Green;
                    break;
                case BlockSignalColor.White:
                    if (!Entity.IsWhiteAvailable) return ValidateColor(BlockSignalColor.Yellow);
                    break;
            }
            return c;
        }

        /// <summary>
        /// Perform initialization actions.
        /// This method is always called on the dispatcher thread.
        /// </summary>
        void IInitializeAtPowerOn.Initialize()
        {
            if (initializeColorNeeded)
            {
                initializeColorNeeded = false;
                Color.Requested = BlockSignalColor.Red;                
            }
        }
    }
}
