using System;
using System.Collections.Generic;
using System.Linq;
using BinkyRailways.Core.Logging;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State.Impl;
using BinkyRailways.Core.Util;
using NLog;

namespace BinkyRailways.Core.State.Automatic
{
    /// <summary>
    /// Implementation of automatic loc control.
    /// </summary>
    internal sealed class AutomaticLocController : IAutomaticLocController, IDisposable
    {
        /// <summary>
        /// A sensor was activated which was not expected.
        /// </summary>
        public event EventHandler<UnexpectedSensorActivatedEventArgs> UnexpectedSensorActivated;

        private static readonly Logger log = LogManager.GetLogger(LogNames.AutoLocController);
        private readonly HeartBeat heartBeat;
        private readonly StateProperty<bool> enabled;
        private readonly List<ILocState> autoLocs = new List<ILocState>();
        private readonly List<ISignalState> signals = new List<ISignalState>();
        private readonly IRailwayState railwayState;
        private bool disposing;

        /// <summary>
        /// Default ctor
        /// </summary>
        public AutomaticLocController(IRailwayState railwayState)
        {
            this.railwayState = railwayState;
            var dispatcher = railwayState.Dispatcher;
            enabled = new StateProperty<bool>(null, false, null, OnRequestedEnabledChanged, null);
            heartBeat = new HeartBeat(this, dispatcher);

            // Register on all state change events.
            foreach (var iterator in railwayState.LocStates)
            {
                var loc = iterator;
                loc.ControlledAutomatically.RequestedChanged += (s, x) => dispatcher.PostAction(() => OnLocControlledAutomaticallyChanged(loc, false, false));
                loc.BeforeReset += (s, x) => dispatcher.PostAction(() => OnLocControlledAutomaticallyChanged(loc, false, true));
                loc.AfterReset += (s, x) => dispatcher.PostAction(() => OnAfterResetLoc(loc));
            }
            foreach (var iterator in railwayState.SensorStates)
            {
                var sensor = iterator;
                sensor.Active.ActualChanged += (s, x) => dispatcher.PostAction(() => OnSensorActiveChanged(sensor));
            }
            foreach (var iterator in railwayState.JunctionStates.OfType<ISwitchState>())
            {
                var @switch = iterator;
                @switch.Direction.ActualChanged += (s, x) => RequestUpdate();
            }
            signals.AddRange(railwayState.SignalStates);
        }

        /// <summary>
        /// Is automatic loc control active?
        /// </summary>
        IStateProperty<bool> IAutomaticLocController.Enabled
        {
            get { return enabled; }
        }

        /// <summary>
        /// Is automatic loc control active?
        /// </summary>
        internal bool Enabled
        {
            get { return enabled.Actual; }
        }

        /// <summary>
        /// Is automatic loc control requested to de-activate?
        /// </summary>
        internal bool Stopping
        {
            get { return !enabled.Requested; }
        }

        /// <summary>
        /// Should the given loc be controlled automatically?
        /// </summary>
        private bool ShouldControlAutomatically(ILocState loc)
        {
            return
                loc.ControlledAutomatically.Requested &&
                !Stopping &&
                !disposing;
        }

        /// <summary>
        /// Notify the heartbeat to invoke an update.
        /// </summary>
        private void RequestUpdate()
        {
            heartBeat.NotifyUpdate();
        }

        /// <summary>
        /// Enabled.Requested has changed.
        /// </summary>
        private void OnRequestedEnabledChanged(bool value)
        {
            if (value)
            {
                enabled.Actual = true;
                Startup();
            }
            else
            {
                // TODO bring to a controlled stop
                enabled.Actual = false;
            }
        }

        /// <summary>
        /// Start automatic loc control
        /// </summary>
        private void Startup()
        {
            var dispatcher = railwayState.Dispatcher;

            // Handle "activation" for all activated sensors
            foreach (var iterator in railwayState.SensorStates.Where(x => x.Active.Actual))
            {
                if (!iterator.DestinationBlocks.Any(x => x.IsLocked()))
                {
                    // Sensor is not related to any locked block.
                    // Raise a ghost train event
                    var sensor = iterator;
                    dispatcher.PostAction(() => HandleGhost(sensor));
                }
            }            

            // Record locs
            foreach (var iterator in railwayState.LocStates.Where(x => x.ControlledAutomatically.Actual))
            {
                var loc = iterator;
                dispatcher.PostAction(() => OnLocControlledAutomaticallyChanged(loc, true, false));
            }

            // Make sure the heartbeat kicks in.
            RequestUpdate();
        }

        /// <summary>
        /// Stop and cleanup
        /// </summary>
        public void Dispose()
        {
            disposing = true;
            heartBeat.Dispose();
        }

        /// <summary>
        /// Update the state of all automatically controlled locs.
        /// This function must be called on the dispatcher thread.
        /// </summary>
        /// <returns>The timespan before this method wants to be called again.</returns>
        internal TimeSpan UpdateLocStates()
        {
            if (!Enabled)
            {
                return TimeSpan.FromDays(1);
            }

            // Go over each loc and act according to its state.
            var result = TimeSpan.FromSeconds(10);
            var locSelection = new List<ILocState>(autoLocs);
            foreach (var loc in locSelection)
            {
                var state = loc.AutomaticState.Actual;
                var nextUpdate = result;
                switch (state)
                {
                    case AutoLocState.AssignRoute:
                        nextUpdate = OnAssignRoute(loc);
                        break;
                    case AutoLocState.ReversingWaitingForDirectionChange:
                        nextUpdate = OnReversingWaitingForDirectionChange(loc);
                        break;
                    case AutoLocState.WaitingForAssignedRouteReady:
                        nextUpdate = OnWaitingForRouteReady(loc);
                        break;
                    case AutoLocState.Running:
                        // Do nothing
                        break;
                    case AutoLocState.EnterSensorActivated:
                        nextUpdate = OnEnterSensorActivated(loc);
                        break;
                    case AutoLocState.EnteringDestination:
                        nextUpdate = OnEnteringDestination(loc);
                        break;
                    case AutoLocState.ReachedSensorActivated:
                        nextUpdate = OnReachSensorActivated(loc);
                        break;
                    case AutoLocState.ReachedDestination:
                        nextUpdate = OnReachedDestination(loc);
                        break;
                    case AutoLocState.WaitingForDestinationTimeout:
                        nextUpdate = OnWaitingForDestinationTimeout(loc);
                        break;
                    default:
                        log.Warn("Invalid state ({0}) in loc {1}.", state, loc);
                        break;
                }
                if (nextUpdate < result)
                {
                    result = nextUpdate;
                }
            }

            // Update the output signals
            foreach (var signal in signals)
            {
                signal.Update();
            }

            return result;
        }

        /// <summary>
        /// Given loc is set in automatic / manual control mode
        /// </summary>
        private void OnLocControlledAutomaticallyChanged(ILocState loc, bool atStartup, bool forceRemove)
        {
            if ((loc.ControlledAutomatically.IsConsistent) && !atStartup)
            {
                // Nothing has changed
                return;
            }
            if (loc.ControlledAutomatically.Requested)
            {
                // Loc wants to be controlled automatically.
                if (!autoLocs.Contains(loc))
                {
                    // Add to list of automatically controlled locs
                    autoLocs.Add(loc);
                }
                loc.AutomaticState.Actual = AutoLocState.AssignRoute;
                loc.ControlledAutomatically.Actual = true;
                RequestUpdate();
            }
            else if (forceRemove)
            {
                // Remove at any state
                RemoveLocFromAutomaticControl(loc);
            }
            else 
            {
                // Loc wants to be removed from automatic control
                switch (loc.AutomaticState.Actual)
                {
                    case AutoLocState.AssignRoute:
                    case AutoLocState.WaitingForAssignedRouteReady:
                    case AutoLocState.ReachedDestination:
                    case AutoLocState.WaitingForDestinationTimeout:
                        // We can remove now
                        RemoveLocFromAutomaticControl(loc);
                        break;
                }
            }
        }

        /// <summary>
        /// Loc has been reset.
        /// </summary>
        private void OnAfterResetLoc(ILocState loc)
        {
            // Loc could have "occupied" sensors which have now been released.
            // Update sensors.
            foreach (var sensor in railwayState.SensorStates)
            {
                OnSensorActiveChanged(sensor);
            }            
        }

        /// <summary>
        /// Remove the given loc from the list of automatically controlled locs.
        /// </summary>
        private void RemoveLocFromAutomaticControl(ILocState loc)
        {
            log.Info("Removing {0} from auto-loc-list", loc);
            autoLocs.Remove(loc);
            loc.AutomaticState.Actual = AutoLocState.AssignRoute;
            loc.ControlledAutomatically.Actual = false;
        }

        /// <summary>
        /// Active state of given sensor has changed.
        /// </summary>
        private void OnSensorActiveChanged(ISensorState sensor)
        {
            if (!sensor.Active.Actual)
            {
                // Sensor became inactive
                return;
            }

            log.Trace("OnSensorActive {0}", sensor);
            var locsWithRoutes = autoLocs.Where(x => x.CurrentRoute.Actual != null).ToList();
            var locsWithSensor = locsWithRoutes.Where(x => x.CurrentRoute.Actual.Contains(sensor));
            var ghost = true;

            // Update state of all locs 
            foreach (var loc in locsWithSensor)
            {
                IRouteEventBehaviorState behavior;
                var route = loc.CurrentRoute.Actual;
                if (!route.TryGetBehavior(sensor, out behavior))
                    continue;
                // Save in loc
                loc.LastEventBehavior = behavior;

                // Update state
                var autoLocState = loc.AutomaticState.Actual;
                ghost = false;
                switch (behavior.StateBehavior)
                {
                    case RouteStateBehavior.NoChange:
                        // No change
                        break;
                    case RouteStateBehavior.Enter:
                        if (autoLocState == AutoLocState.Running)
                        {
                            loc.AutomaticState.Actual = AutoLocState.EnterSensorActivated;
                            RequestUpdate();
                        }
                        break;
                    case RouteStateBehavior.Reached:
                        if ((autoLocState == AutoLocState.Running) ||
                            (autoLocState == AutoLocState.EnterSensorActivated) ||
                            (autoLocState == AutoLocState.EnteringDestination))
                        {
                            loc.AutomaticState.Actual = AutoLocState.ReachedSensorActivated;
                            RequestUpdate();
                        }
                        break;
                }

                // Update speed
                switch (behavior.SpeedBehavior)
                {
                    case LocSpeedBehavior.NoChange:
                        // No change
                        break;
                    case LocSpeedBehavior.Default:
                        // This is handled in the applicable states
                        break;
                    case LocSpeedBehavior.Maximum:
                        loc.Speed.Requested = loc.GetMaximumSpeed(route.Route);
                        break;
                    case LocSpeedBehavior.Medium:
                        loc.Speed.Requested = loc.GetMediumSpeed(route.Route);
                        break;
                    case LocSpeedBehavior.Minimum:
                        loc.Speed.Requested = loc.SlowSpeed;
                        break;
                }
            }

            // Handle ghost events
            if (ghost)
            {
                // Is the sensor connected to any route, other then the routes that are active?
                var destinationBlocks = sensor.DestinationBlocks;
                var activeRoutes = locsWithRoutes.Select(x => x.CurrentRoute.Actual).ToList();
                if (destinationBlocks.Any(x => !activeRoutes.Any(r => r.Route.Contains(x))))                
                {
                    // Yes, now consider it a ghost event
                    HandleGhost(sensor);
                }
            }
        }

        /// <summary>
        /// Handle a ghost sensor event
        /// </summary>
        private void HandleGhost(ISensorState sensor)
        {
            log.Trace("HandleGhost {0}", sensor);
            // Notify about it
            if (UnexpectedSensorActivated != null)
            {
                var args = new UnexpectedSensorActivatedEventArgs(sensor);
                UnexpectedSensorActivated(this, args);
                if (args.Handled)
                    return;
            }
            else
            {
                // Not handled, power off
                log.Warn("Ghost activation in sensor {0}, global power down.", sensor);
                railwayState.Power.Requested = false;
            }
        }

        /// <summary>
        /// Choose an available route from the given block.
        /// </summary>
        /// <param name="loc">The loc a route should be choosen for</param>
        /// <param name="fromBlock">The starting block of the route</param>
        /// <param name="locDirection">The direction the loc is facing in the <see cref="fromBlock"/>.</param>
        /// <param name="avoidDirectionChanges">If true, routes requiring a direction change will not be considered, unless there is no alternative.</param>
        /// <returns>Null if not route is available.</returns>
        private IRouteState ChooseRoute(ILocState loc, IBlockState fromBlock, BlockSide locDirection, bool avoidDirectionChanges)
        {
            // Gather possible routes.
            var routeFromFromBlock = railwayState.GetAllPossibleNonClosedRoutesFromBlock(fromBlock).ToList();
            var possibleRoutes = routeFromFromBlock.Where(x => x.IsAvailableFor(railwayState, loc, locDirection, avoidDirectionChanges)).ToList();

            if (possibleRoutes.Any())
            {
                // Use the route selector to choose a next route
                var selector = loc.RouteSelector;
                var selected = selector.SelectRoute(possibleRoutes, loc, fromBlock, locDirection);
                if (selected == null)
                {
                    log.Info("No route selected for {0} [from {1} {2}]. Available routes: {3}", loc, fromBlock, locDirection, string.Join(", ", possibleRoutes.Select(x => x.ToString()).ToArray()));
                }
                else
                {
                    log.Info("Selected route {0} for {1} [from {2} {3}]", selected, loc, fromBlock, locDirection);
                }
                return selected;
            }

            log.Info("No possible routes for {0} [from {1} {2}]", loc, fromBlock, locDirection);

            // No available routes
            return null;
        }

        /// <summary>
        /// Try to choose a next route, unless the target block of the current route
        /// is set to wait.
        /// </summary>
        /// <returns>True if a next route has been chosen.</returns>
        private bool ChooseNextRoute(ILocState loc)
        {
            // Should we wait in the destination block?
            var route = loc.CurrentRoute.Actual;
            if ((route == null) ||
                (loc.WaitAfterCurrentRoute.Actual) ||
                (loc.HasNextRoute()) ||
                !ShouldControlAutomatically(loc))
            {
                // No need to choose a next route
                return false;
            }

            // The loc can continue (if a free route is found)
            var nextRoute = ChooseRoute(loc, route.Route.To, route.Route.ToBlockSide.Invert(), true);
            if (nextRoute == null)
            {
                // No route available
                return false;
            }

            // We have a next route
            nextRoute.Lock(loc);
            loc.NextRoute.Actual = nextRoute;

            // Set all junctions correct
            nextRoute.Prepare();
            return true;
        }

        /// <summary>
        /// Try to assign a route to the given loc.
        /// </summary>
        /// <returns>The timespan before this method wants to be called again.</returns>
        private TimeSpan OnAssignRoute(ILocState loc)
        {
            // Check state
            var state = loc.AutomaticState.Actual;
            if (state != AutoLocState.AssignRoute) 
            {
                log.Warn("Cannot assign route to loc {0} in {1} state.", loc, loc.AutomaticState);
                return TimeSpan.MaxValue;
            }

            // Log state
            log.Trace("OnAssignRoute {0}", loc);

            // Does the locs still wants to be controlled?
            if (!ShouldControlAutomatically(loc))
            {
                RemoveLocFromAutomaticControl(loc);
                return TimeSpan.MaxValue;
            }

            // Look for a free route from the current destination
            var block = loc.CurrentBlock.Actual;
            if (block == null)
            {
                // Oops, no current block, we cannot control this loc
                RemoveLocFromAutomaticControl(loc);
                return TimeSpan.MaxValue;
            }

            // Is a reversing block on a block where it must un-reverse?
            if (loc.Reversing.Actual && block.ChangeDirectionReversingLocs)
            {
                // Change direction and clear the reversing flag
                loc.CurrentBlockEnterSide.Actual = loc.CurrentBlockEnterSide.Actual.Invert();
                loc.Reversing.Actual = false;
                loc.Direction.Requested = loc.Direction.Actual.Invert();
                loc.AutomaticState.Actual = AutoLocState.ReversingWaitingForDirectionChange;
                return TimeSpan.Zero;
            }

            // Gather possible routes
            var route = loc.NextRoute.Actual ?? ChooseRoute(loc, block, loc.CurrentBlockEnterSide.Actual.Invert(), false);
            if (route == null)
            {
                // No possible routes right now, try again
                return TimeSpan.FromMinutes(1);
            }

            // Lock the route and assign it
            route.Lock(loc);
            loc.CurrentRoute.Actual = route.CreateStateForLoc(loc);
            // Setup waiting after block
            if (route.To.WaitPermissions.Evaluate(loc))
            {
                // Waiting allowed, gamble for it.
                var waitProbability = route.To.WaitProbability;
                loc.WaitAfterCurrentRoute.Actual = ThreadStatics.Random.Gamble(waitProbability);
            }
            else
            {
                // Waiting not allowed.
                loc.WaitAfterCurrentRoute.Actual = false;
            }

            // Clear next route
            loc.NextRoute.Actual = null;

            // Should we change direction?
            var enteredSide = loc.CurrentBlockEnterSide.Actual;
            var leavingSide = route.FromBlockSide;
            if (enteredSide == leavingSide)
            {
                // Reverse direction
                loc.Direction.Requested = loc.Direction.Actual.Invert();

                // Are we reversing now?
                if (loc.ChangeDirection == ChangeDirection.Avoid)
                {
                    loc.Reversing.Actual = !loc.Reversing.Actual;
                }

                // When reversing, check the state of the target block
                if (loc.Reversing.Actual && route.To.ChangeDirectionReversingLocs)
                {
                    // We must stop at the target block
                    loc.WaitAfterCurrentRoute.Actual = true;
                }
            }

            // Change state
            loc.AutomaticState.Actual = AutoLocState.WaitingForAssignedRouteReady;

            // Prepare the route?
            route.Prepare();

            // We're done
            return TimeSpan.Zero;
        }

        /// <summary>
        /// Continue to AssignRoute state once the direction is consistent.
        /// </summary>
        private TimeSpan OnReversingWaitingForDirectionChange(ILocState loc)
        {
            // Check state
            if (loc.AutomaticState.Actual != AutoLocState.ReversingWaitingForDirectionChange)
            {
                log.Warn("Expected ReversingWaitingForDirectionChange state for loc {0}, found {1}.", loc, loc.AutomaticState);
                return TimeSpan.MaxValue;
            }

            // Log state
            log.Trace("OnReversingWaitingForDirectionChange{0}", loc);

            if (loc.Direction.IsConsistent)
            {
                // Ok, we can continue
                loc.AutomaticState.Actual = AutoLocState.AssignRoute;
                return TimeSpan.Zero;
            }
            
            // Wait a bit more
            return TimeSpan.FromSeconds(1);
        }

        /// <summary>
        /// Check that the designated route is ready and if so, start the loc.
        /// </summary>
        /// <returns>The timespan before this method wants to be called again.</returns>
        private TimeSpan OnWaitingForRouteReady(ILocState loc)
        {
            // Check state
            if (loc.AutomaticState.Actual != AutoLocState.WaitingForAssignedRouteReady)
            {
                log.Warn("Cannot start running loc {0} in {1} state.", loc, loc.AutomaticState);
                return TimeSpan.MaxValue;
            }
            
            // Log state
            log.Trace("OnWaitingForRouteReady{0}", loc);

            // If the route ready?
            var route = loc.CurrentRoute.Actual;
            if (!route.Route.IsPrepared)
            {
                // Make sure we stop
                loc.Speed.Requested = 0;
                // We're not ready yet
                return TimeSpan.FromMinutes(1);
            }

            // Set to max speed
            loc.Speed.Requested = loc.GetMaximumSpeed(route.Route);

            // Update state
            loc.AutomaticState.Actual = AutoLocState.Running;

            // Already look for a next route
            ChooseNextRoute(loc);

            return TimeSpan.MaxValue;
        }

        /// <summary>
        /// Loc has triggered an enter sensor.
        /// </summary>
        /// <returns>The timespan before this method wants to be called again.</returns>
        private TimeSpan OnEnterSensorActivated(ILocState loc)
        {
            // Check state
            var state = loc.AutomaticState.Actual;
            if (state != AutoLocState.EnterSensorActivated)
            {
                // Wrong state, no need to do anything
                return TimeSpan.MaxValue;
            }

            // Log state
            log.Trace("OnEnterSensorActivated {0}", loc);

            // Notify route selector
            loc.RouteSelector.BlockEntered(loc, loc.CurrentRoute.Actual.Route.To);

            // Should we wait in the destination block?
            if (loc.WaitAfterCurrentRoute.Actual)
            {
                // The loc should wait in the target block,
                // so slow down the loc.
                if (loc.IsLastEventBehaviorSpeedDefault)
                {
                    loc.Speed.Requested = loc.GetMediumSpeed(loc.CurrentRoute.Actual.Route);
                }
            }
            else
            {
                // The loc can continue (if a free route is found)
                ChooseNextRoute(loc);
                var nextRoute = loc.NextRoute.Actual;
                if ((nextRoute != null) && nextRoute.IsPrepared)
                {
                    // We have a next route, so we can continue our speed
                    if (loc.IsLastEventBehaviorSpeedDefault)
                    {
                        loc.Speed.Requested = loc.GetMaximumSpeed(nextRoute);
                    }
                }
                else if (nextRoute != null)
                {
                    // No next route not yet ready, slow down
                    if (loc.IsLastEventBehaviorSpeedDefault)
                    {
                        loc.Speed.Requested = loc.GetMediumSpeed(nextRoute);
                    }
                }
                else
                {
                    // No route available at this time, or next route not yet ready, slow down
                    if (loc.IsLastEventBehaviorSpeedDefault)
                    {
                        loc.Speed.Requested = loc.GetMediumSpeed(loc.CurrentRoute.Actual.Route);
                    }
                }
            }

            // Update state
            loc.AutomaticState.Actual = AutoLocState.EnteringDestination;

            return TimeSpan.FromMinutes(1);
        }

        /// <summary>
        /// Loc is entering its destination.
        /// </summary>
        /// <returns>The timespan before this method wants to be called again.</returns>
        private TimeSpan OnEnteringDestination(ILocState loc)
        {
            // Check state
            var state = loc.AutomaticState.Actual;
            if (state != AutoLocState.EnteringDestination)
            {
                // Wrong state, no need to do anything
                return TimeSpan.MaxValue;
            }

            // Log state
            log.Trace("OnEnteringDestination {0}", loc);

            // The loc can continue (if a free route is found)
            ChooseNextRoute(loc);
            if (loc.HasNextRoute() && loc.NextRoute.Actual.IsPrepared)
            {
                // We have a next route, so we can continue our speed
                loc.Speed.Requested = loc.GetMaximumSpeed(loc.NextRoute.Actual);
            }

            return TimeSpan.MaxValue;
        }

        /// <summary>
        /// Change the state of the loc to reached destination.
        /// </summary>
        /// <returns>The timespan before this method wants to be called again.</returns>
        private TimeSpan OnReachSensorActivated(ILocState loc)
        {
            // Check state
            var state = loc.AutomaticState.Actual;
            if (state != AutoLocState.ReachedSensorActivated)
            {
                // Invalid state, no need to do anything
                return TimeSpan.MaxValue;
            }

            // Log state
            log.Trace("OnReachSensorActivated {0}", loc);

            var route = loc.CurrentRoute.Actual;
            var currentBlock = route.Route.To;

            // Notify route selector
            loc.RouteSelector.BlockReached(loc, currentBlock);

            // Should we wait in the destination block?
            if (loc.WaitAfterCurrentRoute.Actual || 
                (loc.NextRoute.Actual == null) || 
                (!ShouldControlAutomatically(loc)))
            {
                // Stop the loc now
                loc.Speed.Requested = 0;
            }

            // Release the current route, except for the current block.
            loc.CurrentBlock.Actual = currentBlock;
            loc.CurrentBlockEnterSide.Actual = route.Route.ToBlockSide;
            route.Route.Unlock(x => (x == currentBlock));
            // Make sure the current block is still locked
            currentBlock.AssertLockedBy(loc);

            // Update state
            loc.AutomaticState.Actual = AutoLocState.ReachedDestination;

            return TimeSpan.Zero;
        }

        /// <summary>
        /// Change the state of the loc to reached destination.
        /// </summary>
        /// <returns>The timespan before this method wants to be called again.</returns>
        private TimeSpan OnReachedDestination(ILocState loc)
        {
            // Check state
            var state = loc.AutomaticState.Actual;
            if (state != AutoLocState.ReachedDestination)
            {
                // Invalid state, no need to do anything
                return TimeSpan.MaxValue;
            }

            // Log state
            log.Trace("OnReachedDestination {0}", loc);

            // Do we still control this loc?
            if (!ShouldControlAutomatically(loc))
            {
                RemoveLocFromAutomaticControl(loc);
                return TimeSpan.MaxValue;
            }

            // Delay if we should wait
            var route = loc.CurrentRoute.Actual;
            var currentBlock = route.Route.To;
            if (loc.WaitAfterCurrentRoute.Actual)
            {
                var delta = Math.Max(0, currentBlock.MaximumWaitTime - currentBlock.MinimumWaitTime);
                var secondsToWait = currentBlock.MinimumWaitTime + ((delta > 0) ? ThreadStatics.Random.Next(delta) : 0);
                var restartTime = DateTime.Now.AddSeconds(secondsToWait);
                loc.StartNextRouteTime.Actual = restartTime;

                // Post an action to continue after the wait time
                loc.AutomaticState.Actual = AutoLocState.WaitingForDestinationTimeout;

                return TimeSpan.FromSeconds(secondsToWait);
            }

            // We can continue, let's assign a new route
            loc.AutomaticState.Actual = AutoLocState.AssignRoute;
            return TimeSpan.Zero;
        }

        /// <summary>
        /// The given loc has waited at it's destination.
        /// Let's assign a new route.
        /// </summary>
        /// <returns>The timespan before this method wants to be called again.</returns>
        private TimeSpan OnWaitingForDestinationTimeout(ILocState loc)
        {
            // Check state
            var state = loc.AutomaticState.Actual;
            if (state != AutoLocState.WaitingForDestinationTimeout)
            {
                log.Warn("Loc {0} in valid state ({1}) at destination timeout.", loc, loc.AutomaticState);
                return TimeSpan.MaxValue;
            }

            // Log state
            log.Trace("OnWaitingForDestinationTimeout {0}", loc);

            // Do we still control this loc?
            if (!ShouldControlAutomatically(loc))
            {
                RemoveLocFromAutomaticControl(loc);
                return TimeSpan.MaxValue;
            }

            // Timeout reached?
            var startNextRouteTime = loc.StartNextRouteTime.Actual;
            var now = DateTime.Now;
            if (now >= startNextRouteTime)
            {
                // Yes, let's assign a new route
                loc.AutomaticState.Actual = AutoLocState.AssignRoute;
                return TimeSpan.Zero;
            }

            // Nothing changed
            return startNextRouteTime.Subtract(now);
        }
    }
}
