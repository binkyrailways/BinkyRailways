using System;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State.Impl.DccOverRs232;
using BinkyRailways.Core.State.Impl.Ecos;
using BinkyRailways.Core.State.Impl.LocoBuffer;
using BinkyRailways.Core.State.Impl.BinkyNet;
using BinkyRailways.Core.Util;
using BinkyRailways.Core.State.Impl.P50x;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// Builder class used to create a state object.
    /// </summary>
    internal sealed class StateBuilder : EntityVisitor<IEntityState, RailwayState>
    {
        public override IEntityState Visit(IBinaryOutput entity, RailwayState data)
        {
            return new BinaryOutputState(entity, data);
        }

        public override IEntityState Visit(IBinarySensor entity, RailwayState data)
        {
            return new BinarySensorState(entity, data);
        }

        public override IEntityState Visit(IBlock entity, RailwayState data)
        {
            return new BlockState(entity, data);
        }

        public override IEntityState Visit(IBlockGroup entity, RailwayState data)
        {
            return new BlockGroupState(entity, data);
        }

        public override IEntityState Visit(IBlockSignal entity, RailwayState data)
        {
            return new BlockSignalState(entity, data);
        }

        public override IEntityState Visit(IClock4StageOutput entity, RailwayState data)
        {
            return new Clock4StageOutputState(entity, data);
        }

        public override IEntityState Visit(IDccOverRs232CommandStation entity, RailwayState data)
        {
            return new DccOverRs232CommandStationState(entity, data, Empty<string>.Array);
        }

        public override IEntityState Visit(IEcosCommandStation entity, RailwayState data)
        {
            return new EcosCommandStationState(entity, data, Empty<string>.Array);
        }

        public override IEntityState Visit(IInitializeJunctionAction entity, RailwayState data)
        {
            return new InitializeJunctionActionState(entity, data);
        }

        public override IEntityState Visit(ILoc entity, RailwayState data)
        {
            return new LocState(entity, data);
        }

        public override IEntityState Visit(ILocFunctionAction entity, RailwayState data)
        {
            return new LocFunctionActionState(entity, data);
        }

        public override IEntityState Visit(ILocoBufferCommandStation entity, RailwayState data)
        {
            return new LocoBufferCommandStationState(entity, data, Empty<string>.Array);
        }

        public override IEntityState Visit(IBinkyNetCommandStation entity, RailwayState data)
        {
            return new BinkyNetCommandStationState(entity, data, Empty<string>.Array);
        }

        public override IEntityState Visit(IP50xCommandStation entity, RailwayState data)
        {
            return new P50xCommandStationState(entity, data, Empty<string>.Array);
        }

        public override IEntityState Visit(IPlaySoundAction entity, RailwayState data)
        {
            return new PlaySoundActionState(entity, data);
        }

        public override IEntityState Visit(IRailway entity, RailwayState data)
        {
            throw new NotImplementedException();
        }

        public override IEntityState Visit(IRoute entity, RailwayState data)
        {
            return new RouteState(entity, data);
        }

        public override IEntityState Visit(IRouteEvent entity, RailwayState data)
        {
            return new RouteEventState(entity, data);
        }

        public override IEntityState Visit(IRouteEventBehavior entity, RailwayState data)
        {
            return new RouteEventBehaviorState(entity, data);
        }

        public override IEntityState Visit(IPassiveJunction entity, RailwayState data)
        {
            return new PassiveJunctionState(entity, data);
        }

        public override IEntityState Visit(ISwitch entity, RailwayState data)
        {
            return new SwitchState(entity, data);
        }

        public override IEntityState Visit(ITurnTable entity, RailwayState data)
        {
            return new TurnTableState(entity, data);
        }

        public override IEntityState Visit(IVirtualCommandStation entity, RailwayState data)
        {
            return new Virtual.VirtualCommandStationState(entity, data);
        }
    }
}
