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
	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
)

// autoRunLocState implements auto-run behavior for a specific loc.
type autoRunLocState struct {
	exclusive  util.Exclusive
	loc        state.Loc
	lastSensor state.Sensor
	state      autoRunLocStates
}

type autoRunLocStates uint8

const (
	Initial autoRunLocStates = 0
	Enter   autoRunLocStates = 1
)

// newAutoRunLocState constructs an auto run state for the given loc
func newAutoRunLocState(loc state.Loc, exclusive util.Exclusive) *autoRunLocState {
	return &autoRunLocState{
		exclusive: exclusive,
		loc:       loc,
	}
}

// Tick performs a single step in the auto run behavior of the loc
func (s *autoRunLocState) Tick(ctx context.Context) {
	s.exclusive.Exclusive(ctx, func(ctx context.Context) error {
		if !s.loc.GetControlledAutomatically().GetActual(ctx) {
			return nil
		}
		route := s.loc.GetCurrentRoute().GetActual(ctx)
		if route == nil {
			return nil
		}
		switch s.loc.GetAutomaticState().GetActual(ctx) {
		case state.Running, state.EnterSensorActivated, state.EnteringDestination:
			s.selectNextState(ctx, route.GetRoute())
		}
		return nil
	})
}

func (s *autoRunLocState) selectNextState(ctx context.Context, route state.Route) {
	if s.lastSensor != nil {
		s.lastSensor.GetActive().SetActual(ctx, false)
	}
	if s.state == Initial {
		var sensor state.Sensor
		route.ForEachSensor(func(sx state.Sensor) {
			if sensor == nil {
				if route.IsEnteringDestinationSensor(sx, s.loc) {
					sensor = sx
				}
			}
		})
		if sensor != nil {
			sensor.GetActive().SetActual(ctx, true)
			s.lastSensor = sensor
			s.state = Enter
			return
		}
	}
	// Activate reached sensor
	var sensor state.Sensor
	route.ForEachSensor(func(sx state.Sensor) {
		if sensor == nil {
			if route.IsReachedDestinationSensor(sx, s.loc) {
				sensor = sx
			}
		}
	})
	if sensor != nil {
		sensor.GetActive().SetActual(ctx, true)
		s.lastSensor = sensor
		s.state = Initial
	}
}
