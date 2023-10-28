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

// JunctionWithStateSet is a set of junction with state elements.
// Each junction may only occur once (if it occurs)
type JunctionWithStateSet interface {
	EntitySet

	// Invoke the callback for each item
	ForEach(cb func(JunctionWithState))

	// Get by junction ID.
	// Returns false if not found
	Get(junctionID string) (JunctionWithState, bool)

	// Remove the given item from this set.
	// Returns true if it was removed, false otherwise
	Remove(item Junction) bool

	// Remove all items.
	// Returns true if one or more items were removed, false otherwise
	Clear() bool

	// Does this set contain the given item with some state?
	Contains(item Junction) bool

	// Copy all my entries to the given destination
	CopyTo(JunctionWithStateSet) error

	/// <summary>
	/// Add the given item to this set
	/// </summary>
	//void Add(IPassiveJunction item);

	/// <summary>
	/// Add the given item to this set
	/// </summary>
	AddSwitch(item Switch, direction SwitchDirection) error

	/// <summary>
	/// Add the given item to this set
	/// </summary>
	//void Add(ITurnTable item, int position);
}
