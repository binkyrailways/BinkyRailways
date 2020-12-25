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
	"strings"
)

// Network is a set on addresses of the same type.
type Network struct {
	Type         AddressType
	AddressSpace string
}

const (
	invalidAddressSpaceChars = ",;:/\\\n\t "
)

// NewNetwork returns a initialized network.
func NewNetwork(tp AddressType, addressSpace string) Network {
	return Network{
		Type:         tp,
		AddressSpace: addressSpace,
	}
}

// Validate a network.
// Returns an error when something is incorrect or nil when all ok.
func (n Network) Validate() error {
	if strings.ContainsAny(n.AddressSpace, invalidAddressSpaceChars) {
		return fmt.Errorf("Address space contains invalid characters")
	}
	return nil
}

// Equals returns true if both networks are equal, false otherwise.
func (n Network) Equals(other Network) bool {
	return n.Type == other.Type && n.AddressSpace == other.AddressSpace
}

// String returns a human readable representation of a network.
func (n Network) String() string {
	if n.AddressSpace == "" {
		return string(n.Type)
	}
	return fmt.Sprintf("%s, %s", n.Type, n.AddressSpace)
}

// IsEmpty return true if the given address is the zero value
func (n Network) IsEmpty() bool {
	return n.Type == "" && n.AddressSpace == ""
}
