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

// AddressEntity is an Entity with address
type AddressEntity interface {
	Entity

	// Get the Address of the entity
	GetAddress() Address
	// Set the Address of the entity
	SetAddress(value Address) error

	// Call the given callback for all (non-empty) addresses configured in this
	// entity with the direction their being used.
	ForEachAddressUsage(func(AddressUsage))
}
