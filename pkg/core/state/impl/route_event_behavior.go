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
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// RouteEventBehavior adds implementation functions to state.RouteEventBehavior.
type RouteEventBehavior interface {
	state.RouteEventBehavior
}

type routeEventBehavior struct {
	entity model.RouteEventBehavior
}

// Create a new entity
func newRouteEventBehavior(entity model.RouteEventBehavior, railway Railway) RouteEventBehavior {
	rb := &routeEventBehavior{
		entity: entity,
	}
	//	appliesTo = new LocPredicateState(behavior.AppliesTo, railwayState); // TODO
	return rb
}

// Does this behavior apply to the given loc?
func (rb *routeEventBehavior) AppliesTo(loc state.Loc) bool {
	//	return appliesTo.Evaluate(loc);
	return true // TODO
}

// How is the state of the route changed.
func (rb *routeEventBehavior) GetStateBehavior() model.RouteStateBehavior {
	return rb.entity.GetStateBehavior()
}

// How is the speed of the occupying loc changed.
func (rb *routeEventBehavior) GetSpeedBehavior() model.LocSpeedBehavior {
	return rb.entity.GetSpeedBehavior()
}
