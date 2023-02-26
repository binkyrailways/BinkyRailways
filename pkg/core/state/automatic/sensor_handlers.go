// Copyright 2023 Ewout Prangsma
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
)

// Pair of loc with current route
type locWithRoute struct {
	Loc   state.Loc
	Route state.RouteForLoc
}

// handleGhost handles a ghost sensor event
func (alc *automaticLocController) handleGhost(sensor state.Sensor) {
	alc.railway.Send(&state.UnexpectedSensorActivatedEvent{
		Subject: sensor,
	})
}

// onSensorActivated is called when a given sensor became active.
func (alc *automaticLocController) onSensorActivated(ctx context.Context, sensor state.Sensor) {
	log := alc.log.With().Str("sensor", sensor.GetDescription()).Logger()

	log.Trace().Msg("OnSensorActive")
	var locsWithRoutes []locWithRoute
	alc.railway.ForEachLoc(func(x state.Loc) {
		if rt := x.GetCurrentRoute().GetActual(ctx); rt != nil {
			locsWithRoutes = append(locsWithRoutes, locWithRoute{
				Loc:   x,
				Route: rt,
			})
		}
	})
	var locsWithSensor []locWithRoute
	for _, x := range locsWithRoutes {
		if x.Route.Contains(sensor) {
			locsWithSensor = append(locsWithSensor, x)
		}
	}
	ghost := true

	// Update state of all locs
	for _, entry := range locsWithSensor {
		loc := entry.Loc
		route := entry.Route
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

		// Update speed (if allowed)
		if loc.GetAutomaticState().GetActual(ctx).AcceptSensorSpeedBehavior() {
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
	}

	// Handle ghost events
	if ghost {
		// Is the sensor connected to any route, other then the routes that are active?
		mustHandleGhost := false
		sensor.ForEachDestinationBlock(func(b state.Block) {
			anyRouteContainsBlock := false
			for _, x := range locsWithRoutes {
				rt := x.Route
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
	// Test that current block is locked
	state.AssertLockedBy(ctx, currentBlock, loc)
	// Unlock the route
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
