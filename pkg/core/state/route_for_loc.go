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

package state

// RouteForLoc specifies the state of a single route for a specific loc.
type RouteForLoc interface {
	// Gets the loc for which this route state is
	GetLoc() Loc

	// Gets the underlying route state
	GetRoute() Route

	// Try to get the behavior for the given sensor.
	TryGetBehavior(sensor Sensor) (RouteEventBehavior, bool)

	// Does this route contain an event for the given sensor for my loc?
	Contains(sensor Sensor) bool
}
