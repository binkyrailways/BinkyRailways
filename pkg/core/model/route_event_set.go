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

package model

// RouteEventSet is a set of route events.
type RouteEventSet interface {
	// Get number of entries
	GetCount() int

	// Get event in this set by ID of the sensor
	Get(sensorID string) (RouteEvent, bool)

	// Invoke the callback for each item
	ForEach(cb func(RouteEvent))

	// Remove the given item from this set.
	// Returns true if it was removed, false otherwise
	Remove(item RouteEvent) bool

	// Remove all items.
	// Returns true if one or more items were removed, false otherwise
	Clear() bool

	// Add the given item to this set.
	Add(sensor Sensor) RouteEvent
}
