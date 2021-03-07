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

// LocRefSet is a set of locomotives references.
type LocRefSet interface {
	EntitySet

	// Get the containing railway
	GetRailway() Railway

	// Get an item by ID
	Get(id string) (LocRef, bool)

	// Invoke the callback for each item
	ForEach(cb func(LocRef))

	// Remove the given item from this set.
	// Returns true if it was removed, false otherwise
	Remove(item LocRef) bool

	// Does this set contain the given item?
	Contains(item LocRef) bool

	// Add a reference to the given entity
	Add(Loc) LocRef

	// Copy all entries into the given destination.
	CopyTo(LocRefSet)
}
