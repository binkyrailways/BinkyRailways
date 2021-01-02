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

package model

// ActionTrigger is an event source to which actions can be added.
type ActionTrigger interface {
	EntitySet

	// Gets human readable (localizable) name of this trigger.
	GetName() string

	// Get an item by index
	Get(index int) (Action, bool)

	// Invoke the callback for each item
	ForEach(cb func(Action))

	// Remove the given item from this list.
	// Returns true if it was removed, false otherwise
	Remove(item Action) bool

	// Remove the item at the given index from this list.
	// Returns true if it was removed, false otherwise
	RemoveAt(index int) bool

	// Does this list contain the given item?
	Contains(item Action) bool

	// Add a new item to this list
	Add(item Action)
}
