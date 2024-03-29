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

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// RouteForLoc adds implementation functions to state.RouteForLoc.
type RouteForLoc interface {
	state.RouteForLoc
}

type routeForLoc struct {
	loc       state.Loc
	route     Route
	behaviors map[string]state.RouteEventBehavior // sensor.ID -> ...
}

// Create a new entity
func newRouteForLoc(ctx context.Context, loc state.Loc, route Route) RouteForLoc {
	rs := &routeForLoc{
		loc:       loc,
		route:     route,
		behaviors: make(map[string]state.RouteEventBehavior),
	}
	route.ForEachEvent(ctx, func(evt state.RouteEvent) {
		var first state.RouteEventBehavior
		evt.ForEachBehavior(func(b state.RouteEventBehavior) {
			if first != nil {
				return
			}
			if b.AppliesTo(loc) {
				first = b
			}
		})
		if first != nil {
			rs.behaviors[evt.GetSensor().GetID()] = first
		}
	})
	return rs
}

// Is this equal to other?
func (rs *routeForLoc) Equals(other state.RouteForLoc) bool {
	return rs.GetLoc() == other.GetLoc() && rs.GetRoute() == other.GetRoute()
}

// Gets the loc for which this route state is
func (rs *routeForLoc) GetLoc() state.Loc {
	if rs == nil {
		return nil
	}
	return rs.loc
}

// Gets the underlying route state
func (rs *routeForLoc) GetRoute() state.Route {
	if rs == nil {
		return nil
	}
	return rs.route
}

// Try to get the behavior for the given sensor.
func (rs *routeForLoc) TryGetBehavior(sensor state.Sensor) (state.RouteEventBehavior, bool) {
	id := sensor.GetID()
	b, found := rs.behaviors[id]
	return b, found
}

// Does this route contain an event for the given sensor for my loc?
func (rs *routeForLoc) Contains(sensor state.Sensor) bool {
	id := sensor.GetID()
	_, found := rs.behaviors[id]
	return found
}
