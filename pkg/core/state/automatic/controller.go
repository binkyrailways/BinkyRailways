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
	alcEnabledGauge.Set(0)
	alc.enabled.alc = alc
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
	alc.trigger.Trigger()
}

// Run the automatic controller loop until automatic control is disabled.
func (alc *automaticLocController) run() {
	log := alc.log
	alc.closing = false
	alc.enabled.actual = true
	alcEnabledGauge.Set(1)
	defer func() {
		log.Debug().Msg("alc run ended")
		alcEnabledGauge.Set(0)
		alc.enabled.actual = false
	}()

	// Subscribe to relevant events
	var subscriptions []context.CancelFunc
	actionsQueue := make(chan func(context.Context), 32)
	// Unsubscription on end of run
	defer func() {
		for _, cancel := range subscriptions {
			cancel()
		}
		q := actionsQueue
		actionsQueue = nil
		close(q)
	}()

	// Subscribe to loc events
	alc.railway.ForEachLoc(func(loc state.Loc) {
		// Subscribe to loc-controlled-automatically events
		cancel := loc.GetControlledAutomatically().SubscribeRequestChanges(func(ctx context.Context, value bool) {
			if q := actionsQueue; q != nil {
				q <- func(ctx context.Context) {
					log.Trace().Msg("loc controlled automatically state changed")
					alc.onLocControlledAutomaticallyChanged(ctx, loc, false, false)
				}
			}
		})
		subscriptions = append(subscriptions, cancel)

		// Subscribe to loc-before-reset events
		cancel = loc.SubscribeBeforeReset(func(ctx context.Context) {
			if q := actionsQueue; q != nil {
				q <- func(ctx context.Context) {
					log.Trace().Msg("loc before reset")
					alc.onLocControlledAutomaticallyChanged(ctx, loc, false, true)
				}
			}
		})
		subscriptions = append(subscriptions, cancel)

		// Subscribe to loc-after-reset events
		cancel = loc.SubscribeAfterReset(func(ctx context.Context) {
			if q := actionsQueue; q != nil {
				q <- func(ctx context.Context) {
					log.Trace().Msg("loc after reset")
					alc.onAfterResetLoc(ctx, loc)
				}
			}
		})
		subscriptions = append(subscriptions, cancel)
	})

	// Subscribe to sensor events
	alc.railway.ForEachSensor(func(sensor state.Sensor) {
		cancel := sensor.GetActive().SubscribeActualChanges(func(ctx context.Context, value bool) {
			if sensor.GetActive().GetActual(ctx) {
				// Sensor became active
				if q := actionsQueue; q != nil {
					q <- func(ctx context.Context) {
						log.Trace().Msg("sensor active changed")
						alc.onSensorActivated(ctx, sensor)
					}
				}
			}
		})
		subscriptions = append(subscriptions, cancel)
	})

	// Subscribe to signal actual changed events
	alc.railway.ForEachJunction(func(junction state.Junction) {
		if sw, ok := junction.(state.Switch); ok {
			cancel := sw.GetDirection().SubscribeActualChanges(func(ctx context.Context, dir model.SwitchDirection) {
				// Upon actual switch change, trigger update
				log.Trace().Msg("switch actual changed")
				alc.trigger.Trigger()
			})
			subscriptions = append(subscriptions, cancel)
		}
	})

	/*
	   // Register on all state change events.
	   signals.AddRange(railwayState.SignalStates);
	*/
	//TODO

	delay := time.Second
	for {
		// Wait for trigger or heartbeat
		var action func(ctx context.Context)
		select {
		case <-alc.trigger.Done():
			// Update triggered
		case action = <-actionsQueue:
			// Execute given action in our loop
		case <-time.After(delay):
			// Heartbeat
		}

		// Update state of locs
		var canClose bool
		alc.railway.Exclusive(context.Background(), time.Millisecond*5, "automaticLocController.run.updateLocStates", func(ctx context.Context) error {
			// Process action (if any)
			if action != nil {
				action(ctx)
			}
			// Update automatic state of all locs
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

// Loc has been reset.
func (alc *automaticLocController) onAfterResetLoc(ctx context.Context, loc state.Loc) {
	// Loc could have "occupied" sensors which have now been released.
	// Update sensors.
	alc.railway.ForEachSensor(func(s state.Sensor) {
		if s.GetActive().GetActual(ctx) {
			alc.onSensorActivated(ctx, s)
		}
	})
}

// removeLocFromAutomaticControl removes the loc from automatic control
// This function must be called while holding the exclusive.
func (alc *automaticLocController) removeLocFromAutomaticControl(ctx context.Context, loc state.Loc) {
	delete(alc.autoLocs, loc.GetID())
	loc.GetAutomaticState().SetActual(ctx, state.AssignRoute)
	loc.GetControlledAutomatically().SetActual(ctx, false)
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
	if route.GetTo().GetWaitPermissions().Evaluate(ctx, loc) {
		// Waiting allowed, gamble for it.
		waitProbability := route.GetTo().GetWaitProbability(ctx)
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
	leavingSide := route.GetFromBlockSide()
	if enteredSide == leavingSide {
		// Reverse direction
		loc.GetDirection().SetRequested(ctx, loc.GetDirection().GetActual(ctx).Invert())

		// Are we reversing now?
		if loc.GetChangeDirection(ctx) == model.ChangeDirectionAvoid {
			loc.GetReversing().SetActual(ctx, !loc.GetReversing().GetActual(ctx))
		}

		// When reversing, check the state of the target block
		if loc.GetReversing().GetActual(ctx) && route.GetTo().GetChangeDirectionReversingLocs(ctx) {
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

// / Should the given loc be controlled automatically?
func (alc *automaticLocController) shouldControlAutomatically(ctx context.Context, loc state.Loc) bool {
	return loc.GetControlledAutomatically().GetRequested(ctx) &&
		!alc.isStopping(ctx)
	// TODO && !disposing
}
