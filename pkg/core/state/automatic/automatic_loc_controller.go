// Copyright 2021 Ewout Prangsma
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// Author Ewout Prangsma
//

package automatic

import (
	"context"
	"math/rand"
	"time"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
	"github.com/rs/zerolog"
)

type railway interface {
	state.Railway
	state.EventDispatcher
}

// NewAutomaticLocController creates a new automatic loc controller
func NewAutomaticLocController(railway railway, log zerolog.Logger) (state.AutomaticLocController, error) {
	alc := &automaticLocController{
		railway:                 railway,
		log:                     log,
		autoLocs:                make(map[string]state.Loc),
		routeAvailabilityTester: NewLiveRouteAvailabilityTester(railway),
	}
	alc.enabled.alc = alc

	// Register on all state change events.
	railway.ForEachLoc(func(loc state.Loc) {
		loc.GetControlledAutomatically().SubscribeRequestChanges(func(ctx context.Context, value bool) {
			alc.onLocControlledAutomaticallyChanged(ctx, loc, false, false)
		})
		/*
			loc.BeforeReset += (s, x) => dispatcher.PostAction(() => OnLocControlledAutomaticallyChanged(loc, false, true));
			loc.AfterReset += (s, x) => dispatcher.PostAction(() => OnAfterResetLoc(loc));
		*/
		// TODO
	})

	railway.ForEachSensor(func(sensor state.Sensor) {
		sensor.GetActive().SubscribeActualChanges(func(ctx context.Context, value bool) {
			alc.onSensorActiveChanged(ctx, sensor)
		})
	})

	railway.ForEachJunction(func(junction state.Junction) {
		if sw, ok := junction.(state.Switch); ok {
			sw.GetDirection().SubscribeActualChanges(func(ctx context.Context, dir model.SwitchDirection) {

				//alc.requestUpdate()
				// TODOs
				alc.trigger.Trigger()
			})
		}
	})

	/*
	   // Register on all state change events.
	   signals.AddRange(railwayState.SignalStates);
	*/
	//TODO

	return alc, nil
}

const (
	// Maximum interval until checking state again
	maxDuration = time.Hour * 24
)

type automaticLocController struct {
	railway                 railway
	log                     zerolog.Logger
	enabled                 enabledProperty
	autoLocs                map[string]state.Loc // ID -> loc
	trigger                 util.Trigger
	closing                 bool
	routeAvailabilityTester *liveRouteAvailabilityTester
}

// Is automatic loc control enabled?
func (alc *automaticLocController) GetEnabled() state.BoolProperty {
	return &alc.enabled
}

// Close the automatic loc controller
func (alc *automaticLocController) Close(ctx context.Context) {
	alc.closing = true
	alc.GetEnabled().SetRequested(ctx, false)
}

// Run the automatic controller loop until automatic control is disabled.
func (alc *automaticLocController) run() {
	log := alc.log
	defer func() {
		log.Debug().Msg("alc run ended")
	}()
	sensorActiveChanged := make(chan state.Sensor, 32)
	defer close(sensorActiveChanged)
	/*cancel := alc.railway.Subscribe(context.Background(), func(evt state.Event) {
		switch evt := evt.(type) {
		case state.ActualStateChangedEvent:
			log.Debug().Interface("event", evt).Msg("ActualStateChangedEvent")
			if sensor, ok := evt.Subject.(state.Sensor); ok && sensor.GetActive() == evt.Property {
				sensorActiveChanged <- sensor
			}
		}
	})
	defer cancel()*/
	delay := time.Second
	for {
		// Wait for trigger or heartbeat
		var sensor state.Sensor
		select {
		case <-alc.trigger.Done():
			// Update triggered
			log.Trace().Msg("alc triggered")
		case <-time.After(delay):
			// Heartbeat
		case sensor = <-sensorActiveChanged:
			// Sensor active property change
			log.Trace().Msg("sensor active changed")
		}

		// Update state of locs
		var canClose bool
		alc.railway.Exclusive(context.Background(), func(ctx context.Context) error {
			if sensor != nil {
				alc.onSensorActiveChanged(ctx, sensor)
			}
			delay, canClose = alc.updateLocStates(ctx)
			return nil
		})

		// Close?
		if alc.closing || canClose {
			return
		}
	}
}

// Startup starts automatic loc control
// This function must be called while holding the exclusive.
func (alc *automaticLocController) Startup(ctx context.Context) {
	// Handle "activation" for all activated sensors
	alc.railway.ForEachSensor(func(sx state.Sensor) {
		if !sx.GetActive().GetActual(ctx) {
			return
		}
		hasLocks := false
		sx.ForEachDestinationBlock(func(b state.Block) {
			if state.IsLocked(ctx, b) {
				hasLocks = true
			}
		})
		if hasLocks {
			alc.handleGhost(sx)
		}
	})

	// Record locs
	alc.railway.ForEachLoc(func(l state.Loc) {
		if l.GetControlledAutomatically().GetActual(ctx) {
			alc.onLocControlledAutomaticallyChanged(ctx, l, true, false)
		}
	})

	// Start run loop
	alc.trigger.Trigger()
	go alc.run()
}

// handleGhost handles a ghost sensor event
func (alc *automaticLocController) handleGhost(sensor state.Sensor) {
	alc.railway.Send(&state.UnexpectedSensorActivatedEvent{
		Subject: sensor,
	})
}

// onLocControlledAutomaticallyChanged is called when the given loc is set in automatic / manual control mode
// This function must be called while holding the exclusive.
func (alc *automaticLocController) onLocControlledAutomaticallyChanged(ctx context.Context, loc state.Loc, atStartup, forceRemove bool) {
	if loc.GetControlledAutomatically().IsConsistent(ctx) && !atStartup {
		// Nothing has changed
		return
	}
	if loc.GetControlledAutomatically().GetRequested(ctx) {
		// Loc wants to be controlled automatically
		alc.autoLocs[loc.GetID()] = loc
		loc.GetAutomaticState().SetActual(ctx, state.AssignRoute)
		loc.GetControlledAutomatically().SetActual(ctx, true)
		alc.trigger.Trigger()
	} else if forceRemove {
		// Remove at any state
		alc.removeLocFromAutomaticControl(ctx, loc)
	} else {
		// Loc wants to be removed from automatic control
		switch loc.GetAutomaticState().GetActual(ctx) {
		case state.AssignRoute,
			state.WaitingForAssignedRouteReady,
			state.ReachedDestination,
			state.WaitingForDestinationTimeout,
			state.WaitingForDestinationGroupMinimum:
			// We can remove now
			alc.removeLocFromAutomaticControl(ctx, loc)
		}
	}
}

// removeLocFromAutomaticControl removes the loc from automatic control
// This function must be called while holding the exclusive.
func (alc *automaticLocController) removeLocFromAutomaticControl(ctx context.Context, loc state.Loc) {
	delete(alc.autoLocs, loc.GetID())
	loc.GetAutomaticState().SetActual(ctx, state.AssignRoute)
	loc.GetControlledAutomatically().SetActual(ctx, false)
}

// Active state of given sensor has changed.
func (alc *automaticLocController) onSensorActiveChanged(ctx context.Context, sensor state.Sensor) {
	log := alc.log.With().Str("sensor", sensor.GetDescription()).Logger()
	if !sensor.GetActive().GetActual(ctx) {
		// Sensor became inactive
		return
	}

	log.Trace().Msg("OnSensorActive")
	var locsWithRoutes, locsWithSensor []state.Loc
	alc.railway.ForEachLoc(func(x state.Loc) {
		if x.GetCurrentRoute().GetActual(ctx) != nil {
			locsWithRoutes = append(locsWithRoutes, x)
		}
	})
	for _, x := range locsWithRoutes {
		if x.GetCurrentRoute().GetActual(ctx).Contains(sensor) {
			locsWithSensor = append(locsWithSensor, x)
		}
	}
	ghost := true

	// Update state of all locs
	for _, loc := range locsWithSensor {
		route := loc.GetCurrentRoute().GetActual(ctx)
		behavior, found := route.TryGetBehavior(sensor)
		if !found {
			continue
		}
		// Save in loc
		loc.SetLastEventBehavior(ctx, behavior)

		// Update state
		autoLocState := loc.GetAutomaticState().GetActual(ctx)
		ghost = false
		switch behavior.GetStateBehavior() {
		case model.RouteStateBehaviorNoChange:
			// No change
		case model.RouteStateBehaviorEnter:
			if autoLocState == state.Running {
				loc.GetAutomaticState().SetActual(ctx, state.EnterSensorActivated)
				alc.trigger.Trigger()
			}
		case model.RouteStateBehaviorReached:
			if (autoLocState == state.Running) ||
				(autoLocState == state.EnterSensorActivated) ||
				(autoLocState == state.EnteringDestination) {
				loc.GetAutomaticState().SetActual(ctx, state.ReachedSensorActivated)
				alc.trigger.Trigger()
			}
		}

		// Update speed
		switch behavior.GetSpeedBehavior() {
		case model.LocSpeedBehaviorNoChange:
			// No change
		case model.LocSpeedBehaviorDefault:
			// This is handled in the applicable states
		case model.LocSpeedBehaviorMaximum:
			loc.GetSpeed().SetRequested(ctx, state.GetMaximumSpeedForRoute(ctx, loc, route.GetRoute()))
		case model.LocSpeedBehaviorMedium:
			loc.GetSpeed().SetRequested(ctx, state.GetMediumSpeedForRoute(ctx, loc, route.GetRoute()))
		case model.LocSpeedBehaviorMinimum:
			loc.GetSpeed().SetRequested(ctx, loc.GetSlowSpeed(ctx))
		}
	}

	// Handle ghost events
	if ghost {
		// Is the sensor connected to any route, other then the routes that are active?
		mustHandleGhost := false
		sensor.ForEachDestinationBlock(func(b state.Block) {
			anyRouteContainsBlock := false
			for _, x := range locsWithRoutes {
				rt := x.GetCurrentRoute().GetActual(ctx)
				if rt.GetRoute().ContainsBlock(ctx, b) {
					anyRouteContainsBlock = true
					break
				}
			}
			if !anyRouteContainsBlock {
				mustHandleGhost = true
			}
		})
		if mustHandleGhost {
			// Yes, now consider it a ghost event
			alc.handleGhost(sensor)
		}
	}
}

// Update the state of all automatically controlled locs.
// Returns: delayUntilNextInvocation, canStop
// This function must be called while holding the exclusive.
func (alc *automaticLocController) updateLocStates(ctx context.Context) (time.Duration, bool) {
	if !alc.GetEnabled().GetRequested(ctx) {
		// TODO stop all locs??
		return 0, true
	}

	nextDelay := time.Second * 10
	for _, loc := range alc.autoLocs {
		st := loc.GetAutomaticState().GetActual(ctx)
		log := alc.log.With().
			Str("loc", loc.GetDescription()).
			Str("state", st.String()).
			Logger()
		nextLocDelay := nextDelay
		switch st {
		case state.AssignRoute:
			nextLocDelay = alc.onAssignRoute(ctx, loc)
		case state.ReversingWaitingForDirectionChange:
			nextLocDelay = alc.onReversingWaitingForDirectionChange(ctx, loc)
		case state.WaitingForAssignedRouteReady:
			nextLocDelay = alc.onWaitingForRouteReady(ctx, loc)
		case state.Running:
			// Do nothing
		case state.EnterSensorActivated:
			nextLocDelay = alc.onEnterSensorActivated(ctx, loc)
		case state.EnteringDestination:
			nextLocDelay = alc.onEnteringDestination(ctx, loc)
		case state.ReachedSensorActivated:
			nextLocDelay = alc.onReachSensorActivated(ctx, loc)
		case state.ReachedDestination:
			nextLocDelay = alc.onReachedDestination(ctx, loc)
		case state.WaitingForDestinationTimeout:
			nextLocDelay = alc.onWaitingForDestinationTimeout(ctx, loc)
		case state.WaitingForDestinationGroupMinimum:
			nextLocDelay = alc.onWaitingForDestinationGroupMinimum(ctx, loc)
		default:
			log.Warn().Msg("Invalid state in loc.")
		}
		if nextLocDelay < nextDelay {
			nextDelay = nextLocDelay
		}
	}

	// Update the output signals
	/*
	   TODO				foreach (var signal in signals)
	   				{
	   					signal.Update();
	   				}
	*/
	return nextDelay, false
}

/// Choose an available route from the given block.
/// <param name="loc">The loc a route should be choosen for</param>
/// <param name="fromBlock">The starting block of the route</param>
/// <param name="locDirection">The direction the loc is facing in the <see cref="fromBlock"/>.</param>
/// <param name="avoidDirectionChanges">If true, routes requiring a direction change will not be considered, unless there is no alternative.</param>
/// <returns>Null if not route is available.</returns>
func (alc *automaticLocController) chooseRoute(ctx context.Context, loc state.Loc, currentRoute state.Route, fromBlock state.Block, locDirection model.BlockSide, avoidDirectionChanges bool) state.Route {
	// Gather all non-closed routes from given from-block
	routeFromFromBlock := getAllPossibleNonClosedRoutesFromBlock(fromBlock)
	// Filter out routes that are currently not available (for any reason)
	possibleRouteOptions := routeFromFromBlock.And(func(ctx context.Context, r state.Route) bool {
		ro := alc.routeAvailabilityTester.IsAvailableFor(ctx, r, loc, locDirection, avoidDirectionChanges)
		alc.log.Debug().
			Str("loc", loc.GetDescription()).
			Str("route", r.GetDescription()).
			Bool("available", ro.IsPossible).
			Str("reason", ro.GetReasonDescription()).
			Msg("Route availability result")
		return ro.IsPossible
	})
	// Filter out routes that have a conflicting output against the current route
	locState := loc.GetAutomaticState().GetActual(ctx)
	routeOptionsWithoutConflictingOutputs := possibleRouteOptions.And(func(ctx context.Context, r state.Route) bool {
		if currentRoute == nil {
			// No conflict because there is no current route
			return true
		}
		if locState >= state.EnterSensorActivated {
			// Loc is already reached destination (or further).
			// We no longer care about conflicting outputs
			return true
		}
		hasConflictingOutput := false
		r.GetModel().GetOutputs().ForEach(func(ows model.OutputWithState) {
			if !hasConflictingOutput {
				if currentRoute.HasConflictingOutput(ctx, ows) {
					hasConflictingOutput = true
				}
			}
		})
		return !hasConflictingOutput
	})
	possibleRoutes := routeOptionsWithoutConflictingOutputs.GetRoutes(ctx, alc.railway)
	//loc.LastRouteOptions.Actual = routeOptions.ToArray()
	// TODO ^^

	if len(possibleRoutes) > 0 {
		// Use the route selector to choose a next route
		selector := loc.GetRouteSelector(ctx)
		selected := selector.SelectRoute(ctx, possibleRoutes, loc, fromBlock, locDirection)
		if selected == nil {
			alc.log.Info().
				Str("loc", loc.GetDescription()).
				Str("from", fromBlock.GetDescription()).
				Str("fromSide", locDirection.String()).
				Interface("availableRoutes", possibleRoutes).
				Msg("No route selected for loc")
		} else {
			alc.log.Info().
				Str("loc", loc.GetDescription()).
				Str("selected", selected.GetDescription()).
				Str("from", fromBlock.GetDescription()).
				Str("fromSide", locDirection.String()).
				Interface("availableRoutes", possibleRoutes).
				Msg("Selected route for loc")
		}
		return selected
	}

	alc.log.Info().
		Str("loc", loc.GetDescription()).
		Str("from", fromBlock.GetDescription()).
		Str("fromSide", locDirection.String()).
		Msg("No possible routes for loc")

	// No available routes
	return nil
}

/// <summary>
/// Try to choose a next route, unless the target block of the current route
/// is set to wait.
/// </summary>
/// <returns>True if a next route has been chosen.</returns>
func (alc *automaticLocController) chooseNextRoute(ctx context.Context, loc state.Loc) bool {
	// Should we wait in the destination block?
	route := loc.GetCurrentRoute().GetActual(ctx)
	if (route == nil) ||
		(loc.GetWaitAfterCurrentRoute().GetActual(ctx)) ||
		(state.HasNextRoute(ctx, loc)) ||
		!alc.shouldControlAutomatically(ctx, loc) {
		// No need to choose a next route
		return false
	}

	// The loc can continue (if a free route is found)
	nextRoute := alc.chooseRoute(ctx, loc, route.GetRoute(), route.GetRoute().GetTo(ctx), route.GetRoute().GetToBlockSide(ctx).Invert(), true)
	if nextRoute == nil {
		// No route available
		return false
	}

	// We have a next route
	nextRoute.Lock(ctx, loc)
	loc.GetNextRoute().SetActual(ctx, nextRoute)

	// Set all junctions correct
	nextRoute.Prepare(ctx)
	return true
}

// Try to assign a route to the given loc.
// Returns: The timespan before this method wants to be called again.
func (alc *automaticLocController) onAssignRoute(ctx context.Context, loc state.Loc) time.Duration {
	log := alc.log.With().Str("loc", loc.GetID()).Logger()
	// Check lock state
	if st := loc.GetAutomaticState().GetActual(ctx); st != state.AssignRoute {
		log.Warn().
			Str("state", st.String()).
			Msg("Cannot assign route to loc in this state")
		return maxDuration
	}

	// Log state
	log.Trace().Msg("onAssignRoute")

	// Does the locs still wants to be controlled?
	if !alc.shouldControlAutomatically(ctx, loc) {
		alc.removeLocFromAutomaticControl(ctx, loc)
		return maxDuration
	}

	// Look for a free route from the current destination
	block := loc.GetCurrentBlock().GetActual(ctx)
	if block == nil {
		// Oops, no current block, we cannot control this loc
		alc.removeLocFromAutomaticControl(ctx, loc)
		return maxDuration
	}

	// Is a reversing block on a block where it must un-reverse?
	if loc.GetReversing().GetActual(ctx) && block.GetChangeDirectionReversingLocs(ctx) {
		// Change direction and clear the reversing flag
		loc.GetCurrentBlockEnterSide().SetActual(ctx, loc.GetCurrentBlockEnterSide().GetActual(ctx).Invert())
		loc.GetReversing().SetActual(ctx, false)
		loc.GetDirection().SetRequested(ctx, loc.GetDirection().GetActual(ctx).Invert())
		loc.GetAutomaticState().SetActual(ctx, state.ReversingWaitingForDirectionChange)
		return 0
	}

	// Select next route
	route := loc.GetNextRoute().GetActual(ctx)
	if route == nil {
		// No next route was set
		if (loc.GetSpeed().GetRequested(ctx) == 0) && (!canLeaveCurrentBlock(ctx, loc)) {
			// We're not running and we're not allowed to leave the current block
			loc.GetAutomaticState().SetActual(ctx, state.WaitingForDestinationGroupMinimum)
			return time.Minute
		}

		// Get current route
		currentRouteForLoc := loc.GetCurrentRoute().GetActual(ctx)
		var currentRoute state.Route
		if currentRouteForLoc != nil {
			currentRoute = currentRouteForLoc.GetRoute()
		}

		// Choose a next route now
		route = alc.chooseRoute(ctx, loc, currentRoute, block, loc.GetCurrentBlockEnterSide().GetActual(ctx).Invert(), false)
	}
	if route == nil {
		// No possible routes right now, try again
		return time.Minute
	}

	// Lock the route
	if err := route.Lock(ctx, loc); err != nil {
		log.Error().Err(err).Msg("Failed to lock route")
		return maxDuration
	}
	// Setup waiting after block (do this before assigning the route)
	if route.GetTo(ctx).GetWaitPermissions().Evaluate(ctx, loc) {
		// Waiting allowed, gamble for it.
		waitProbability := route.GetTo(ctx).GetWaitProbability(ctx)
		loc.GetWaitAfterCurrentRoute().SetActual(ctx, gamble(waitProbability))
	} else {
		// Waiting not allowed.
		loc.GetWaitAfterCurrentRoute().SetActual(ctx, false)
	}
	// Assign the route
	loc.GetCurrentRoute().SetActual(ctx, route.CreateStateForLoc(ctx, loc))

	// Clear next route
	loc.GetNextRoute().SetActual(ctx, nil)

	// Should we change direction?
	enteredSide := loc.GetCurrentBlockEnterSide().GetActual(ctx)
	leavingSide := route.GetFromBlockSide(ctx)
	if enteredSide == leavingSide {
		// Reverse direction
		loc.GetDirection().SetRequested(ctx, loc.GetDirection().GetActual(ctx).Invert())

		// Are we reversing now?
		if loc.GetChangeDirection(ctx) == model.ChangeDirectionAvoid {
			loc.GetReversing().SetActual(ctx, !loc.GetReversing().GetActual(ctx))
		}

		// When reversing, check the state of the target block
		if loc.GetReversing().GetActual(ctx) && route.GetTo(ctx).GetChangeDirectionReversingLocs(ctx) {
			// We must stop at the target block
			loc.GetWaitAfterCurrentRoute().SetActual(ctx, true)
		}
	}

	// Change state
	loc.GetAutomaticState().SetActual(ctx, state.WaitingForAssignedRouteReady)

	// Prepare the route?
	route.Prepare(ctx)

	// We're done
	return 0
}

// Continue to AssignRoute state once the direction is consistent.
func (alc *automaticLocController) onReversingWaitingForDirectionChange(ctx context.Context, loc state.Loc) time.Duration {
	// Check state
	log := alc.log.With().Str("loc", loc.GetID()).Logger()
	st := loc.GetAutomaticState().GetActual(ctx)
	if st != state.ReversingWaitingForDirectionChange {
		log.Warn().Str("state", st.String()).Msg("Expected ReversingWaitingForDirectionChange state for loc")
		return maxDuration
	}

	// Log state
	log.Trace().Msg("OnReversingWaitingForDirectionChange")

	if loc.GetDirection().IsConsistent(ctx) {
		// Ok, we can continue
		loc.GetAutomaticState().SetActual(ctx, state.AssignRoute)
		return 0
	}

	// Wait a bit more
	return time.Second
}

// Check that the designated route is ready and if so, start the loc.
func (alc *automaticLocController) onWaitingForRouteReady(ctx context.Context, loc state.Loc) time.Duration {
	// Check state
	log := alc.log.With().
		Str("loc", loc.GetDescription()).
		Logger()
	st := loc.GetAutomaticState().GetActual(ctx)
	if st != state.WaitingForAssignedRouteReady {
		log.Warn().Str("state", st.String()).Msg("Cannot start running loc in this state.")
		return maxDuration
	}

	// Collect state
	route := loc.GetCurrentRoute().GetActual(ctx)
	log = log.With().Str("route", route.GetRoute().GetDescription()).Logger()

	// Log state
	log.Trace().Msg("OnWaitingForRouteReady")

	// Is the route ready?
	if !route.GetRoute().GetIsPrepared(ctx) {
		// Make sure we stop
		loc.GetSpeed().SetRequested(ctx, 0)
		// We're not ready yet
		return time.Minute
	}

	// Set to max speed
	loc.GetSpeed().SetRequested(ctx, state.GetMaximumSpeedForRoute(ctx, loc, route.GetRoute()))

	// Update state
	loc.GetAutomaticState().SetActual(ctx, state.Running)

	// Already look for a next route
	alc.chooseNextRoute(ctx, loc)

	return maxDuration
}

// Loc has triggered an enter sensor.
func (alc *automaticLocController) onEnterSensorActivated(ctx context.Context, loc state.Loc) time.Duration {
	// Check state
	log := alc.log.With().Str("loc", loc.GetID()).Logger()
	st := loc.GetAutomaticState().GetActual(ctx)
	if st != state.EnterSensorActivated {
		// Wrong state, no need to do anything
		return maxDuration
	}

	// Log state
	log.Trace().Msg("OnEnterSensorActivated")

	// Notify route selector
	loc.GetRouteSelector(ctx).BlockEntered(ctx, loc, loc.GetCurrentRoute().GetActual(ctx).GetRoute().GetTo(ctx))

	// Should we wait in the destination block?
	if loc.GetWaitAfterCurrentRoute().GetActual(ctx) {
		// The loc should wait in the target block,
		// so slow down the loc.
		if loc.GetIsLastEventBehaviorSpeedDefault(ctx) {
			loc.GetSpeed().SetRequested(ctx, state.GetMediumSpeedForRoute(ctx, loc, loc.GetCurrentRoute().GetActual(ctx).GetRoute()))
		}
	} else {
		// The loc can continue (if a free route is found)
		alc.chooseNextRoute(ctx, loc)
		nextRoute := loc.GetNextRoute().GetActual(ctx)
		if (nextRoute != nil) && nextRoute.GetIsPrepared(ctx) {
			// We have a next route, so we can continue our speed
			if loc.GetIsLastEventBehaviorSpeedDefault(ctx) {
				loc.GetSpeed().SetRequested(ctx, state.GetMaximumSpeedForRoute(ctx, loc, nextRoute))
			}
		} else if nextRoute != nil {
			// No next route not yet ready, slow down
			if loc.GetIsLastEventBehaviorSpeedDefault(ctx) {
				loc.GetSpeed().SetRequested(ctx, state.GetMediumSpeedForRoute(ctx, loc, nextRoute))
			}
		} else {
			// No route available at this time, or next route not yet ready, slow down
			if loc.GetIsLastEventBehaviorSpeedDefault(ctx) {
				loc.GetSpeed().SetRequested(ctx, state.GetMediumSpeedForRoute(ctx, loc, loc.GetCurrentRoute().GetActual(ctx).GetRoute()))
			}
		}
	}

	// Update state
	loc.GetAutomaticState().SetActual(ctx, state.EnteringDestination)

	return time.Minute
}

// Loc is entering its destination.
func (alc *automaticLocController) onEnteringDestination(ctx context.Context, loc state.Loc) time.Duration {
	// Check state
	log := alc.log.With().Str("loc", loc.GetID()).Logger()
	st := loc.GetAutomaticState().GetActual(ctx)
	if st != state.EnteringDestination {
		// Wrong state, no need to do anything
		return maxDuration
	}

	// Log state
	log.Trace().Msg("OnEnteringDestination")

	// The loc can continue (if a free route is found)
	alc.chooseNextRoute(ctx, loc)
	if state.HasNextRoute(ctx, loc) && loc.GetNextRoute().GetActual(ctx).GetIsPrepared(ctx) {
		// We have a next route, so we can continue our speed
		loc.GetSpeed().SetRequested(ctx, state.GetMaximumSpeedForRoute(ctx, loc, loc.GetNextRoute().GetActual(ctx)))
	}

	return maxDuration
}

// Change the state of the loc to reached destination.
func (alc *automaticLocController) onReachSensorActivated(ctx context.Context, loc state.Loc) time.Duration {
	// Check state
	log := alc.log.With().Str("loc", loc.GetID()).Logger()
	st := loc.GetAutomaticState().GetActual(ctx)
	if st != state.ReachedSensorActivated {
		// Invalid state, no need to do anything
		return maxDuration
	}

	// Log state
	log.Trace().Msg("OnReachSensorActivated")

	route := loc.GetCurrentRoute().GetActual(ctx)
	currentBlock := route.GetRoute().GetTo(ctx)

	// Notify route selector
	loc.GetRouteSelector(ctx).BlockReached(ctx, loc, currentBlock)

	// Should we wait in the destination block?
	if loc.GetWaitAfterCurrentRoute().GetActual(ctx) ||
		(loc.GetNextRoute().GetActual(ctx) == nil) ||
		(!alc.shouldControlAutomatically(ctx, loc)) {
		// Stop the loc now
		loc.GetSpeed().SetRequested(ctx, 0)
	}

	// Release the current route, except for the current block.
	loc.GetCurrentBlock().SetActual(ctx, currentBlock)
	loc.GetCurrentBlockEnterSide().SetActual(ctx, route.GetRoute().GetToBlockSide(ctx))
	route.GetRoute().Unlock(ctx, currentBlock)
	// Make sure the current block is still locked
	state.AssertLockedBy(ctx, currentBlock, loc)

	// Update state
	loc.GetAutomaticState().SetActual(ctx, state.ReachedDestination)
	return 0
}

// Change the state of the loc to reached destination.
func (alc *automaticLocController) onReachedDestination(ctx context.Context, loc state.Loc) time.Duration {
	// Check state
	log := alc.log.With().Str("loc", loc.GetID()).Logger()
	st := loc.GetAutomaticState().GetActual(ctx)
	if st != state.ReachedDestination {
		// Invalid state, no need to do anything
		return maxDuration
	}

	// Log state
	log.Trace().Msg("OnReachedDestination")

	// Do we still control this loc?
	if !alc.shouldControlAutomatically(ctx, loc) {
		alc.removeLocFromAutomaticControl(ctx, loc)
		return maxDuration
	}

	// Delay if we should wait
	route := loc.GetCurrentRoute().GetActual(ctx)
	currentBlock := route.GetRoute().GetTo(ctx)
	if loc.GetWaitAfterCurrentRoute().GetActual(ctx) {
		delta := maxInt(0, currentBlock.GetMaximumWaitTime(ctx)-currentBlock.GetMinimumWaitTime(ctx))
		secondsToWait := currentBlock.GetMinimumWaitTime(ctx)
		if delta > 0 {
			secondsToWait += rand.Intn(delta)
		}
		restartTime := time.Now().Add(time.Duration(secondsToWait) * time.Second)
		loc.GetStartNextRouteTime().SetActual(ctx, restartTime)

		// Post an action to continue after the wait time
		loc.GetAutomaticState().SetActual(ctx, state.WaitingForDestinationTimeout)

		return time.Second * time.Duration(secondsToWait)
	}

	// We can continue, let's assign a new route
	loc.GetAutomaticState().SetActual(ctx, state.AssignRoute)
	return 0
}

// The given loc has waited at it's destination.
// Let's assign a new route.
func (alc *automaticLocController) onWaitingForDestinationTimeout(ctx context.Context, loc state.Loc) time.Duration {
	// Check state
	log := alc.log.With().Str("loc", loc.GetID()).Logger()
	st := loc.GetAutomaticState().GetActual(ctx)
	if st != state.WaitingForDestinationTimeout {
		log.Warn().Str("state", st.String()).Msg("Loc in valid state at destination timeout.")
		return maxDuration
	}

	// Log state
	log.Trace().Msg("OnWaitingForDestinationTimeout")

	// Do we still control this loc?
	if !alc.shouldControlAutomatically(ctx, loc) {
		alc.removeLocFromAutomaticControl(ctx, loc)
		return maxDuration
	}

	// Timeout reached?
	startNextRouteTime := loc.GetStartNextRouteTime().GetActual(ctx)
	now := time.Now()
	if !now.Before(startNextRouteTime) {
		// Check the block group, see if we can leave
		if canLeaveCurrentBlock(ctx, loc) {
			// Yes, let's assign a new route
			loc.GetAutomaticState().SetActual(ctx, state.AssignRoute)
			return 0
		} else {
			// No we cannot leave yet
			loc.GetAutomaticState().SetActual(ctx, state.WaitingForDestinationGroupMinimum)
			return maxDuration
		}
	}

	// Nothing changed
	return startNextRouteTime.Sub(now)
}

// The given loc has waited at it's destination.
// Let's assign a new route.
func (alc *automaticLocController) onWaitingForDestinationGroupMinimum(ctx context.Context, loc state.Loc) time.Duration {
	// Check state
	log := alc.log.With().Str("loc", loc.GetID()).Logger()
	st := loc.GetAutomaticState().GetActual(ctx)
	if st != state.WaitingForDestinationGroupMinimum {
		log.Warn().Str("state", st.String()).Msg("Loc in valid state at destination timeout.")
		return maxDuration
	}

	// Log state
	log.Trace().Msg("OnWaitingForDestinationGroupMinimum")

	// Do we still control this loc?
	if !alc.shouldControlAutomatically(ctx, loc) {
		alc.removeLocFromAutomaticControl(ctx, loc)
		return maxDuration
	}

	// Timeout reached?
	if canLeaveCurrentBlock(ctx, loc) {
		// Yes, let's assign a new route
		loc.GetAutomaticState().SetActual(ctx, state.AssignRoute)
		return 0
	}

	// Nothing changed
	return maxDuration
}

// Is automatic loc control requested to de-activate?
func (alc *automaticLocController) isStopping(ctx context.Context) bool {
	return !alc.enabled.GetRequested(ctx)
}

/// Should the given loc be controlled automatically?
func (alc *automaticLocController) shouldControlAutomatically(ctx context.Context, loc state.Loc) bool {
	return loc.GetControlledAutomatically().GetRequested(ctx) &&
		!alc.isStopping(ctx)
	// TODO && !disposing
}
