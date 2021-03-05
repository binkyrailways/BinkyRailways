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

// BinkyNetLocalWorker represents the configuration of a Local Worker in the Bink network.
type BinkyNetLocalWorker interface {
	Entity

	// Optional alias for the local worker.
	GetAlias() string
	SetAlias(value string) error

	// Set of devices that must be configured on this local worker.
	GetDevices() BinkyNetDeviceSet
	// Set of real world objects controlled by the local worker
	GetObjects() BinkyNetObjectSet
}