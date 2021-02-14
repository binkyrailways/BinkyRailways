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

// Sensor adds implementation functions to state.Sensor.
type Sensor interface {
	Entity
	state.Sensor
}

type sensor struct {
	entity
	destinationBlocks []state.Block
	active            actualBoolProperty
}

// Create a new entity
func newSensor(en model.Sensor, railway Railway) sensor {
	s := sensor{
		entity: newEntity(en, railway),
	}
	return s
}

// getSensor returns the entity as Sensor.
func (s *sensor) Configure(railway Railway) {
	s.active.Configure(s, railway)
}

// getSensor returns the entity as Sensor.
func (s *sensor) getSensor() model.Sensor {
	return s.GetEntity().(model.Sensor)
}

// Try to prepare the entity for use.
// Returns nil when the entity is successfully prepared,
// returns an error otherwise.
func (s *sensor) TryPrepareForUse(state.UserInterface, state.Persistence) error {
	// Resolve command station
	cs, err := s.railway.SelectCommandStation(s.getSensor())
	if err != nil {
		return err
	}
	cs.RegisterInput(s)

	// Resolve destination blocks
	s.railway.ForEachRoute(func(r state.Route) {
		if r.ContainsSensor(s) {
			s.destinationBlocks = append(s.destinationBlocks, r.GetTo())
		}
	})
	if eb := s.getSensor().GetBlock(); eb != nil {
		ebs, err := s.railway.ResolveBlock(eb)
		if err != nil {
			return err
		}
		found := false
		for _, x := range s.destinationBlocks {
			if x == ebs {
				found = true
				break
			}
		}
		if !found {
			s.destinationBlocks = append(s.destinationBlocks, ebs)
		}
	}

	return nil
}

// Address of the entity
func (s *sensor) GetAddress() model.Address {
	return s.getSensor().GetAddress()
}

// Is this sensor in the 'active' state?
func (s *sensor) GetActive() state.ActualBoolProperty {
	return &s.active
}

// Gets all blocks for which this sensor is either an "entering" or a "reached"
// sensor or to which this sensor is attached.
func (s *sensor) ForEachDestinationBlock(cb func(state.Block)) {
	for _, x := range s.destinationBlocks {
		cb(x)
	}
}

// Shape used to visualize this sensor
func (s *sensor) GetShape() model.Shape {
	return s.getSensor().GetShape()
}
