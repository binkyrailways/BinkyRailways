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

// BinkyNetConnectionPinList is a list of device pins.
type BinkyNetConnectionPinList interface {
	// Gets the containing local worker
	GetLocalWorker() BinkyNetLocalWorker

	// Get number of entries
	GetCount() int

	// Get a pin by index
	Get(index int) (BinkyNetDevicePin, bool)

	// Invoke the callback for each pin
	ForEach(cb func(BinkyNetDevicePin))

	// Remove the item at given index.
	// Returns true if it was removed, false otherwise
	Remove(index int) bool

	// Add a new pin
	AddNew() BinkyNetDevicePin
}
