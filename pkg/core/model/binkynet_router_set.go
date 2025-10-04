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

// BinkyNetRouterSet is a set of router configurations.
type BinkyNetRouterSet interface {
	// Get number of entries
	GetCount() int

	// Get an entry by ID.
	Get(id string) (BinkyNetRouter, bool)
	// Get an entry by index.
	GetAt(index int) (BinkyNetRouter, bool)

	// Invoke the callback for each entry.
	ForEach(cb func(BinkyNetRouter))

	// Remove the given entry.
	// Returns true if it was removed, false otherwise
	Remove(BinkyNetRouter) bool

	// Is the given entry contained in this set?
	Contains(BinkyNetRouter) bool

	// Add a new entry.
	AddNew() (BinkyNetRouter, error)
}
