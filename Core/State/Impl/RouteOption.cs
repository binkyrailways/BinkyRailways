using System;

namespace BinkyRailways.Core.State.Impl
{
    internal sealed class RouteOption : IRouteOption
    {
        private readonly IRouteState route;
        private readonly bool isPossible;
        private readonly RouteImpossibleReason reason;
        private readonly string extra;

        /// <summary>
        /// Default ctor
        /// </summary>
        public RouteOption(IRouteState route, bool isPossible, RouteImpossibleReason reason)
            : this(route, isPossible, reason, null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public RouteOption(IRouteState route, RouteImpossibleReason reason)
            : this(route, false, reason, null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public RouteOption(IRouteState route, RouteImpossibleReason reason, string extra)
            : this(route, false, reason, extra)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public RouteOption(IRouteState route, bool isPossible, RouteImpossibleReason reason, string extra)
        {
            this.route = route;
            this.isPossible = isPossible;
            this.reason = reason;
            this.extra = extra;
        }

        public IRouteState Route { get { return route; } }
        public bool IsPossible { get { return isPossible; } }

        public RouteImpossibleReason Reason
        {
            get { return reason; }
        }

        public override string ToString()
        {
            if (isPossible) return string.Format("{0} - {1}", route.Description, Strings.RoutePossible);
            string txt;
            switch (reason)
            {
                case RouteImpossibleReason.Locked:
                    txt = string.Format(Strings.RouteLockedX, extra);
                    break;
                case RouteImpossibleReason.SensorActive:
                    txt = Strings.RouteSensorActive;
                    break;
                case RouteImpossibleReason.Closed:
                    txt = Strings.RouteClosed;
                    break;
                case RouteImpossibleReason.DestinationClosed:
                    txt = Strings.RouteDestinationClosed;
                    break;
                case RouteImpossibleReason.OpposingTraffic:
                    txt = string.Format(Strings.RouteOpposingTrafficInX, extra);
                    break;
                case RouteImpossibleReason.None:
                    txt = Strings.RoutePossible;
                    break;
                case RouteImpossibleReason.DirectionChangeNeeded:
                    txt = Strings.RouteDirectionChangeNeeded;
                    break;
                case RouteImpossibleReason.NoPermission:
                    txt = Strings.RouteNoPermission;
                    break;
                case RouteImpossibleReason.CriticalSectionOccupied:
                    txt = Strings.RouteCriticalSectionOccupied;
                    break;
                case RouteImpossibleReason.DeadLock:
                    txt = Strings.RouteDeadLock;
                    break;
                default:
                    throw new ArgumentException("Unknown reason " + (int)reason);
            }
            return string.Format("{0} - {1}", route.Description, txt);
        }
    }
}
