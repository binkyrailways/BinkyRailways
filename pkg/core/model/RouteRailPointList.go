// Copyright 2024 Ewout Prangsma
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

// RouteRailPointList is an ordered list of rail points in a route.
type RouteRailPointList interface {
	// Get number of entries
	GetCount() int

	// Get a rail point by index
	Get(index int) (RailPoint, bool)

	// Invoke the callback for each item
	ForEach(cb func(RailPoint))

	// Add a rail point to the end of this list.
	// The rail point must be part of the same module.
	Add(item RailPoint) error

	// Remove the given rail point from this list.
	// Returns true if it was removed, false otherwise
	Remove(item RailPoint) bool

	// Remove the rail point at the given index from this list.
	RemoveAt(index int) bool

	// Does this list contain the given item?
	Contains(item RailPoint) bool
}
