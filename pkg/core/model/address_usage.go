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

import (
	"fmt"
)

// AddressUsage is an Address with direction.
type AddressUsage struct {
	Address   Address
	Direction AddressDirection
}

// NewAddressUsage returns a initialized address + direction.
func NewAddressUsage(a Address, d AddressDirection) AddressUsage {
	return AddressUsage{
		Address:   a,
		Direction: d,
	}
}

// Equals returns true if both address + directions are equal, false otherwise.
func (au AddressUsage) Equals(other AddressUsage) bool {
	return au.Address.Equals(other.Address) && au.Direction == other.Direction
}

// String returns a human readable representation of an address usage.
func (au AddressUsage) String() string {
	return fmt.Sprintf("%s, %s", au.Address, au.Direction)
}
