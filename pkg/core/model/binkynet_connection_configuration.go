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

// BinkyNetConnectionConfiguration represents configuration for a connection.
type BinkyNetConnectionConfiguration interface {
	// Get number of entries
	GetCount() int

	// Get value by key
	Get(key string) (string, bool)

	// Invoke the callback for configuration key/value pair.
	ForEach(cb func(key, value string))

	// Remove the value for the given key.
	// Returns true if it was removed, false otherwise
	Remove(key string) bool

	// Set a given key/value pair
	Set(key, value string)
}
