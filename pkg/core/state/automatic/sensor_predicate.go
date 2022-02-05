// Copyright 2022 Ewout Prangsma
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

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// Predicate for a route
type SensorPredicate func(context.Context, state.Sensor) bool

type sensors interface {
	// Get total possible number of sensors
	GetSensorCount(context.Context) int
	// Iterate all sensors
	ForEachSensor(context.Context, func(state.Sensor))
}

// Construct a predicate that requires both predicates to be true
func (p SensorPredicate) And(other SensorPredicate) SensorPredicate {
	return func(ctx context.Context, r state.Sensor) bool {
		return p(ctx, r) && other(ctx, r)
	}
}

// Get all sensors that match the given predicate
func (p SensorPredicate) GetSensors(ctx context.Context, allSensors sensors) []state.Sensor {
	result := make([]state.Sensor, 0, allSensors.GetSensorCount(ctx))
	allSensors.ForEachSensor(ctx, func(r state.Sensor) {
		if p(ctx, r) {
			result = append(result, r)
		}
	})
	return result
}

// Is there at least 1 sensor that matches the predicate?
func (p SensorPredicate) Any(ctx context.Context, allSensors sensors) bool {
	result := false
	allSensors.ForEachSensor(ctx, func(r state.Sensor) {
		if p(ctx, r) {
			result = true
		}
	})
	return result
}
