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

import api "github.com/binkynet/BinkyNet/apis/v1"

// BinkyNetConnectionSet is a set of connections.
type BinkyNetConnectionSet interface {
	// Gets the containing local worker
	GetLocalWorker() BinkyNetLocalWorker

	// Get number of entries
	GetCount() int

	// Get an entry by name.
	Get(key api.ConnectionName) (BinkyNetConnection, bool)

	// Get an entry by name, add one if not found.
	GetOrAdd(key api.ConnectionName) (BinkyNetConnection, error)

	// Get an entry by index.
	GetAt(index int) (BinkyNetConnection, bool)

	// Invoke the callback for each entry.
	ForEach(cb func(BinkyNetConnection))

	// Remove the given entry.
	// Returns true if it was removed, false otherwise
	Remove(BinkyNetConnection) bool

	// Is the given entry contained in this set?
	Contains(BinkyNetConnection) bool

	// Is the given name contained in this set?
	ContainsName(name api.ConnectionName) bool

	// Add a new entry
	AddNew(key api.ConnectionName) (BinkyNetConnection, error)
}
