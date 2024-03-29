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

// LocPredicateSet is a set of loc predicates.
type LocPredicateSet interface {
	EntitySet

	// Get an item by ID
	Get(id string) (LocPredicate, bool)

	// Gets the number of predicates
	GetCount() int

	// Invoke the callback for each item
	ForEach(cb func(LocPredicate))

	// Remove the given item from this set.
	// Returns true if it was removed, false otherwise
	Remove(item LocPredicate) bool

	// Remove all items from this set.
	// Returns true if at least 1 item was removed, false otherwise
	Clear() bool

	// Does this set contain the given item?
	Contains(item LocPredicate) bool

	// Add a new item to this set
	Add(value LocPredicate)
}
