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

// BinkyNetLocalWorkerSet is a set of local worker configurations.
type BinkyNetLocalWorkerSet interface {
	// Get number of entries
	GetCount() int

	// Get an entry by ID or alias.
	Get(id string) (BinkyNetLocalWorker, bool)

	// Invoke the callback for each entry.
	ForEach(cb func(BinkyNetLocalWorker))

	// Remove the given entry.
	// Returns true if it was removed, false otherwise
	Remove(BinkyNetLocalWorker) bool

	// Is the given entry contained in this set?
	Contains(BinkyNetLocalWorker) bool

	// Add a new entry with given ID.
	AddNew(id string) (BinkyNetLocalWorker, error)
}
