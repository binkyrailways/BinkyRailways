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

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
)

type railway interface {
	state.Railway
	state.EventDispatcher
}

// NewAutomaticLocController creates a new automatic loc controller
func NewAutomaticLocController(railway railway) (state.AutomaticLocController, error) {
	alc := &automaticLocController{
		railway:  railway,
		autoLocs: make(map[string]state.Loc),
	}
	alc.enabled.alc = alc
	return alc, nil
}

type automaticLocController struct {
	railway  railway
	enabled  enabledProperty
	autoLocs map[string]state.Loc // ID -> loc
	trigger  util.Trigger
	closing  bool
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
	delay := time.Second
	for {
		// Wait for trigger or heartbeat
		select {
		case <-alc.trigger.Done():
			// Update triggered
		case <-time.After(delay):
			// Heartbeat
		}

		// Update state of locs
		var canClose bool
		alc.railway.Exclusive(context.Background(), func(ctx context.Context) error {
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
			//			log.Warn("Invalid state ({0}) in loc {1}.", state, loc)
			// TODO
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

func (alc *automaticLocController) onAssignRoute(ctx context.Context, loc state.Loc) time.Duration {
	// TODO
	return 0
}

func (alc *automaticLocController) onReversingWaitingForDirectionChange(ctx context.Context, loc state.Loc) time.Duration {
	// TODO
	return 0

}

func (alc *automaticLocController) onWaitingForRouteReady(ctx context.Context, loc state.Loc) time.Duration {
	// TODO
	return 0

}

func (alc *automaticLocController) onEnterSensorActivated(ctx context.Context, loc state.Loc) time.Duration {
	// TODO
	return 0

}

func (alc *automaticLocController) onEnteringDestination(ctx context.Context, loc state.Loc) time.Duration {
	// TODO
	return 0

}

func (alc *automaticLocController) onReachSensorActivated(ctx context.Context, loc state.Loc) time.Duration {
	// TODO
	return 0

}

func (alc *automaticLocController) onReachedDestination(ctx context.Context, loc state.Loc) time.Duration {
	// TODO
	return 0

}

func (alc *automaticLocController) onWaitingForDestinationTimeout(ctx context.Context, loc state.Loc) time.Duration {
	// TODO
	return 0

}

func (alc *automaticLocController) onWaitingForDestinationGroupMinimum(ctx context.Context, loc state.Loc) time.Duration {
	// TODO
	return 0

}
