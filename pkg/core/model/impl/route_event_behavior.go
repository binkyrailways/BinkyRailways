// Copyright 2020 Ewout Prangsma
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
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/refs"
)

// RouteEventBehavior adds implementation methods to model.RouteEventBehavior
type RouteEventBehavior interface {
	ModuleEntity
	model.RouteEventBehavior
}

type routeEventBehavior struct {
	moduleEntity

	AppliesTo     LocPredicateContainer     `xml:"AppliesTo"`
	StateBehavior *model.RouteStateBehavior `xml:"StateBehavior,omitempty"`
	SpeedBehavior *model.LocSpeedBehavior   `xml:"SpeedBehavior,omitempty"`
}

var _ RouteEventBehavior = &routeEventBehavior{}

// newRouteEventBehavior creates a new newRouteEventBehavior instance.
func newRouteEventBehavior(appliesTo LocPredicate) *routeEventBehavior {
	reb := &routeEventBehavior{}
	reb.AppliesTo.LocPredicate = appliesTo
	return reb
}

// Accept a visit by the given visitor
func (s *routeEventBehavior) Accept(v model.EntityVisitor) interface{} {
	return v.VisitRouteEventBehavior(s)
}

// Predicate used to select the locs to which this event applies.
func (s *routeEventBehavior) GetAppliesTo() model.LocPredicate {
	return s.AppliesTo.LocPredicate
}

// How is the state of the route changed.
func (s *routeEventBehavior) GetStateBehavior() model.RouteStateBehavior {
	return refs.RouteStateBehaviorValue(s.StateBehavior, model.RouteStateBehaviorNoChange)
}
func (s *routeEventBehavior) SetStateBehavior(value model.RouteStateBehavior) error {
	if s.GetStateBehavior() != value {
		s.StateBehavior = refs.NewRouteStateBehavior(value)
		s.OnModified()
	}
	return nil
}

// How is the speed of the occupying loc changed.
func (s *routeEventBehavior) GetSpeedBehavior() model.LocSpeedBehavior {
	return refs.LocSpeedBehaviorValue(s.SpeedBehavior, model.LocSpeedBehaviorDefault)
}
func (s *routeEventBehavior) SetSpeedBehavior(value model.LocSpeedBehavior) error {
	if s.GetSpeedBehavior() != value {
		s.SpeedBehavior = refs.NewLocSpeedBehavior(value)
		s.OnModified()
	}
	return nil
}
