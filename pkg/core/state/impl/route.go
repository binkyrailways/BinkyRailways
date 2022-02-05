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

package impl

import (
	"context"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"go.uber.org/multierr"
)

// Route adds implementation functions to state.Route.
type Route interface {
	Entity
	state.Route
}

type route struct {
	entity
	lockable

	from              Block
	to                Block
	events            []RouteEvent
	crossingJunctions []JunctionWithState
	permissions       locPredicate
	csr               *criticalSectionRoutes
}

// Create a new entity
func newRoute(en model.Route, railway Railway) Route {
	r := &route{
		entity:   newEntity(en, railway),
		lockable: newLockable(railway),
		csr:      &criticalSectionRoutes{},
	}
	var err error
	r.permissions, err = newLocPredicate(railway, en.GetPermissions())
	if err != nil {
		panic(err)
	}
	return r
}

// Unique ID of the module containing this entity
func (r *route) GetModuleID() string {
	return r.getRoute().GetModule().GetID()
}

// getRoute returns the entity as Route.
func (r *route) getRoute() model.Route {
	return r.GetEntity().(model.Route)
}

// Gets the underlying model
func (r *route) GetModel() model.Route {
	return r.getRoute()
}

// Try to prepare the entity for use.
// Returns nil when the entity is successfully prepared,
// returns an error otherwise.
func (r *route) TryPrepareForUse(ctx context.Context, ui state.UserInterface, ps state.Persistence) error {
	// Resolve from, to
	var err error
	rw := r.GetRailwayImpl()
	r.from, err = rw.ResolveEndPoint(ctx, r.getRoute().GetFrom())
	if err != nil {
		return err
	}
	r.to, err = rw.ResolveEndPoint(ctx, r.getRoute().GetTo())
	if err != nil {
		return err
	}

	// Construct crossing junctions
	var merr error
	r.getRoute().GetCrossingJunctions().ForEach(func(jws model.JunctionWithState) {
		jwsImpl, err := newJunctionWithState(ctx, jws, rw)
		if err != nil {
			multierr.AppendInto(&merr, err)
		} else {
			r.crossingJunctions = append(r.crossingJunctions, jwsImpl)
		}
	})
	if merr != nil {
		return merr
	}

	// Construct event states
	r.getRoute().GetEvents().ForEach(func(evt model.RouteEvent) {
		if evt, err := newRouteEvent(ctx, evt, r.railway); err != nil {
			multierr.AppendInto(&merr, err)
		} else {
			r.events = append(r.events, evt)
		}
	})
	if merr != nil {
		return merr
	}

	// Construct critical section
	if err := r.csr.TryPrepareForUse(ctx, ui, ps); err != nil {
		return err
	}

	return nil
}

// Speed of locs when going this route.
// This value is a percentage of the maximum / medium speed of the loc.
// <value>0..100</value>
func (r *route) GetSpeed(context.Context) int {
	return r.getRoute().GetSpeed()
}

// Probability (in percentage) that a loc will take this route.
// When multiple routes are available to choose from the route with the highest probability will have the highest
// chance or being chosen.
// <value>0..100</value>
func (r *route) GetChooseProbability(context.Context) int {
	return r.getRoute().GetChooseProbability()
}

// Gets the source block.
func (r *route) GetFrom(context.Context) state.Block {
	return r.from
}

// Side of the <see cref="From"/> block at which this route will leave that block.
func (r *route) GetFromBlockSide(context.Context) model.BlockSide {
	return r.getRoute().GetFromBlockSide()
}

// Gets the destination block.
func (r *route) GetTo(context.Context) state.Block {
	return r.to
}

// Side of the <see cref="To"/> block at which this route will enter that block.
func (r *route) GetToBlockSide(context.Context) model.BlockSide {
	return r.getRoute().GetToBlockSide()
}

// Does this route require any switches to be in the non-straight state?
func (r *route) GetHasNonStraightSwitches(ctx context.Context) bool {
	for _, jws := range r.crossingJunctions {
		if jws.GetIsNonStraight(ctx) {
			return true
		}
	}
	return false
}

// Is the given sensor listed as one of the "entering destination" sensors of this route?
func (r *route) IsEnteringDestinationSensor(ctx context.Context, sensor state.Sensor, loc state.Loc) bool {
	for _, evt := range r.events {
		if evt.GetSensor() != sensor {
			continue
		}
		found := false
		evt.ForEachBehavior(func(b state.RouteEventBehavior) {
			if !found && b.GetStateBehavior() == model.RouteStateBehaviorEnter {
				if b.AppliesTo(loc) {
					found = true
				}
			}
		})
		if found {
			return true
		}
	}
	return false
}

// Is the given sensor listed as one of the "entering destination" sensors of this route?
func (r *route) IsReachedDestinationSensor(ctx context.Context, sensor state.Sensor, loc state.Loc) bool {
	for _, evt := range r.events {
		if evt.GetSensor() != sensor {
			continue
		}
		found := false
		evt.ForEachBehavior(func(b state.RouteEventBehavior) {
			if !found && b.GetStateBehavior() == model.RouteStateBehaviorReached {
				if b.AppliesTo(loc) {
					found = true
				}
			}
		})
		if found {
			return true
		}
	}
	return false
}

// Does this route contains the given block (either as from, to or crossing)
func (r *route) ContainsBlock(ctx context.Context, b state.Block) bool {
	return r.from == b || r.to == b
}

// Does this route contains the given sensor (either as entering or reached)
func (r *route) ContainsSensor(ctx context.Context, s state.Sensor) bool {
	for _, x := range r.events {
		if x.GetSensor() == s {
			return true
		}
	}
	return false
}

// Does this route contains the given junction
func (r *route) ContainsJunction(ctx context.Context, j state.Junction) bool {
	jImpl, _ := j.(Junction)
	for _, jws := range r.crossingJunctions {
		if jws.Contains(ctx, jImpl) {
			return true
		}
	}
	return false
}

// Gets all sensors that are listed as entering/reached sensor of this route.
func (r *route) ForEachSensor(ctx context.Context, cb func(state.Sensor)) {
	for _, x := range r.events {
		cb(x.GetSensor())
	}
}
func (r *route) GetSensorCount(ctx context.Context) int {
	return len(r.events)
}

// All routes that must be free before this route can be taken.
func (r *route) GetCriticalSection(ctx context.Context) state.CriticalSectionRoutes {
	return r.csr
}

// Gets all events configured for this route.
func (r *route) ForEachEvent(ctx context.Context, cb func(state.RouteEvent)) {
	for _, x := range r.events {
		cb(x)
	}
}

// Gets the predicate used to decide which locs are allowed to use this route.
func (r *route) GetPermissions(ctx context.Context) state.LocPredicate {
	return r.permissions
}

// Is this route open for traffic or not?
// Setting to true, allows for maintance etc. on this route.
func (r *route) GetClosed(ctx context.Context) bool {
	return r.getRoute().GetClosed()
}

// Maximum time in seconds that this route should take.
// If a loc takes this route and exceeds this duration, a warning is given.
func (r *route) GetMaxDuration(ctx context.Context) int {
	return r.getRoute().GetMaxDuration()
}

// Prepare all junctions in this route, such that it can be taken.
func (r *route) Prepare(ctx context.Context) {
	for _, jws := range r.crossingJunctions {
		jws.Prepare(ctx)
	}
}

// Are all junctions set in the state required by this route?
func (r *route) GetIsPrepared(ctx context.Context) bool {
	for _, jws := range r.crossingJunctions {
		if !jws.GetIsPrepared(ctx) {
			return false
		}
	}
	return true
}

// Create a specific route state for the given loc.
func (r *route) CreateStateForLoc(ctx context.Context, loc state.Loc) state.RouteForLoc {
	return newRouteForLoc(ctx, loc, r)
}

// Fire the actions attached to the entering destination trigger.
func (r *route) FireEnteringDestinationTrigger(ctx context.Context, loc state.Loc) {
	// TODO
}

// Fire the actions attached to the destination reached trigger.
func (r *route) FireDestinationReachedTrigger(ctx context.Context, loc state.Loc) {
	// TODO
}
