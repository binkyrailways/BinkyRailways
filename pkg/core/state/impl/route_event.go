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

// RouteEvent adds implementation functions to state.RouteEvent.
type RouteEvent interface {
	state.RouteEvent
}

type routeEvent struct {
	sensor    Sensor
	behaviors []RouteEventBehavior
}

// Create a new entity
func newRouteEvent(entity model.RouteEvent, railway Railway) (RouteEvent, error) {
	rb := &routeEvent{}
	var err error
	rb.sensor, err = railway.ResolveSensor(entity.GetSensor())
	if err != nil {
		return nil, err
	}
	entity.GetBehaviors().ForEach(func(b model.RouteEventBehavior) {
		rb.behaviors = append(rb.behaviors, newRouteEventBehavior(b, railway))
	})
	return rb, nil
}

// Gets the source block.
func (re *routeEvent) GetSensor() state.Sensor {
	return re.sensor
}

// Gets all sensors that are listed as entering/reached sensor of this route.
func (re *routeEvent) ForEachBehavior(cb func(state.RouteEventBehavior)) {
	for _, b := range re.behaviors {
		cb(b)
	}
}
