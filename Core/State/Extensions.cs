using System;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State specific extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Is the given state object a state for the given entity?
        /// </summary>
        public static bool IsStateOf(this IEntityState state, IEntity entity)
        {
            return (state != null) && (entity != null) && (state.EntityId == entity.Id);
        }

        /// <summary>
        /// Is the given state object a state for the given entity?
        /// </summary>
        public static bool IsStateOf<T>(this IEntityState<T> state, T entity)
            where T : class, IEntity
        {
            return (state != null) && (entity != null) && (state.EntityId == entity.Id);
        }

        /// <summary>
        /// Does the given command station support the given network (exact or not exact)?
        /// </summary>
        public static bool Supports(this ICommandStationState cs, IAddressEntity entity, Network network)
        {
            bool exact;
            return cs.Supports(entity, network, out exact);
        }
        /// <summary>
        /// Does the given command station support the given network exact?
        /// </summary>
        public static bool SupportsExact(this ICommandStationState cs, IAddressEntity entity, Network network)
        {
            bool exact;
            return cs.Supports(entity, network, out exact) && exact;
        }

        /// <summary>
        /// Is the given state locked?
        /// </summary>
        public static bool IsLocked(this ILockableState state)
        {
            return (state.LockedBy != null);
        }

        /// <summary>
        /// Is the given state locked by the given loc?
        /// </summary>
        public static bool IsLockedBy(this ILockableState state, ILocState loc)
        {
            return (state.LockedBy == loc);
        }

        /// <summary>
        /// Validate that the given state is locked by the given loc.
        /// If not, throw an error.
        /// </summary>
        public static void AssertLockedBy(this ILockableState state, ILocState loc)
        {
            if (state.LockedBy != loc)
            {
                var current = state.LockedBy;
                if (current == null)
                {
                    throw new InvalidOperationException(
                        string.Format(Strings.ObjectXNotLockedByYNotLocked,
                                      state, loc));
                }
                else
                {
                    throw new InvalidOperationException(
                        string.Format(Strings.ObjectXNotLockedByYButByZ,
                                      state, loc, current));
                }
            }
        }

        /// <summary>
        /// Gets the state object that represents the state of the given entity.
        /// </summary>
        public static IEntityState GetState(this IRailwayState railwayState, IEntity entity)
        {
            if (entity == null)
                return null;
            return entity.Accept(Default<GetStateVisitor>.Instance, railwayState);
        }

        /// <summary>
        /// Helper class for GetState
        /// </summary>
        private sealed class GetStateVisitor : EntityVisitor<IEntityState, IRailwayState>
        {
            public override IEntityState Visit(IBlock entity, IRailwayState data)
            {
                return data.BlockStates[entity];
            }
            public override IEntityState Visit(ICommandStation entity, IRailwayState data)
            {
                return data.CommandStationStates[entity];
            }
            public override IEntityState Visit(IJunction entity, IRailwayState data)
            {
                return data.JunctionStates[entity];
            }
            public override IEntityState Visit(ILoc entity, IRailwayState data)
            {
                return data.LocStates[entity];
            }
            public override IEntityState Visit(IOutput entity, IRailwayState data)
            {
                return data.OutputStates[entity];
            }
            public override IEntityState Visit(IRoute entity, IRailwayState data)
            {
                return data.RouteStates.FirstOrDefault(x => x.IsStateOf(entity));
            }
            public override IEntityState Visit(ISensor entity, IRailwayState data)
            {
                return data.SensorStates[entity];
            }
            public override IEntityState Visit(ISignal entity, IRailwayState data)
            {
                return data.SignalStates[entity];
            }
        }

        /// <summary>
        /// Gets the inverted direction.
        /// </summary>
        public static LocDirection Invert(this LocDirection value)
        {
            return (value == LocDirection.Forward) ? LocDirection.Reverse : LocDirection.Forward;
        }

        /// <summary>
        /// Has the given loc a next route configured?
        /// </summary>
        public static bool HasNextRoute(this ILocState loc)
        {
            return (loc.NextRoute.Actual != null);
        }

        /// <summary>
        /// Does the given loc have an actual speed other then 0?
        /// </summary>
        public static bool IsRunning(this ILocState loc)
        {
            return (loc.Speed.Actual > 0);
        }

        /// <summary>
        /// Gets the maximum speed of the given loc on the given route.
        /// </summary>
        public static int GetMaximumSpeed(this ILocState loc, IRouteState route)
        {
            var speed = loc.Reversing.Actual ? loc.MediumSpeed : loc.MaximumSpeed;
            var routeSpeed = route.Speed / 100.0;
            return Math.Max((int)(speed * routeSpeed), loc.SlowSpeed);
        }

        /// <summary>
        /// Gets the medium speed of the given loc on the given route.
        /// </summary>
        public static int GetMediumSpeed(this ILocState loc, IRouteState route)
        {
            var speed = loc.MediumSpeed;
            var routeSpeed = route.Speed / 100.0;
            return Math.Max((int)(speed * routeSpeed), loc.SlowSpeed);
        }

        /// <summary>
        /// Find the route that is the reverse of the given route.
        /// </summary>
        /// <returns>Null if not found</returns>
        public static IRouteState GetReverse(this IRouteState route)
        {
            var from = route.To;
            var to = route.From;
            var railway = route.RailwayState;

            if ((from == null) || (to == null) || (railway == null))
                return null;

            var reverseRoutes = railway.RouteStates.Where(x => (x.From == from) && (x.To == to));
            foreach (var iterator in reverseRoutes)
            {
                // From side should match
                if (route.FromBlockSide != iterator.ToBlockSide)
                {
                    // No match
                    continue;
                }
                // To side should match
                if (route.ToBlockSide != iterator.FromBlockSide)
                {
                    // No match
                    continue;
                }
                // We found it
                return iterator;
            }
            // Not found
            return null;
        }
    }
}
