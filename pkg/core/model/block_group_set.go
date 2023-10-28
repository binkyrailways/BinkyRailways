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

// BlockGroupSet is a set of block groups.
type BlockGroupSet interface {
	EntitySet

	// Gets the containing module
	GetModule() Module

	// Get a block group by ID
	Get(id string) (BlockGroup, bool)

	// Invoke the callback for each item
	ForEach(cb func(BlockGroup))

	// Remove the given item from this set.
	// Returns true if it was removed, false otherwise
	Remove(item BlockGroup) bool

	// Does this set contain the given item?
	Contains(item BlockGroup) bool

	// Add a new item to this set
	AddNew() BlockGroup
}
