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

// JunctionSet is a set of junctions.
type JunctionSet interface {
	EntitySet

	// Gets the containing module
	GetModule() Module

	// Get an item by ID
	Get(id string) (Junction, bool)

	// Invoke the callback for each item
	ForEach(cb func(Junction))

	// Remove the given item from this set.
	// Returns true if it was removed, false otherwise
	Remove(item Junction) bool

	// Does this set contain the given item?
	Contains(item Junction) bool

	// Add a new passive junction
	AddPassiveJunction() PassiveJunction

	// Add a new standard switch
	AddSwitch() Switch

	// Add a new turn table
	AddTurnTable() TurnTable
}
