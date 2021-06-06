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
	"encoding/xml"
	"strconv"
	"strings"
)

// Address of an object in the railway.
type Address struct {
	Network Network
	Value   string
}

// NewAddressFromString parses an address in the given string
func NewAddressFromString(value string) (Address, error) {
	if value == "" {
		return Address{}, nil
	}
	haveType := false
	haveValue := false
	haveAddressSpace := false
	atype := AddressTypeDcc
	var avalue string
	var aspace string
	for value != "" {
		parts := strings.SplitN(value, " ", 2)
		if len(parts) > 1 {
			value = parts[1]
		} else {
			value = ""
		}
		if !haveType {
			var err error
			atype, err = ParseAddressType(parts[0])
			if err != nil {
				return Address{}, err
			}
			haveType = true
			continue
		}
		if !haveValue {
			avalue = parts[0]
			haveValue = true
			continue
		}
		if !haveAddressSpace {
			aspace = parts[0]
			haveAddressSpace = true
			continue
		}
	}
	a := NewAddress(NewNetwork(atype, aspace), avalue)
	if err := a.Validate(); err != nil {
		return a, err
	}
	return a, nil
}

// NewAddress returns a initialized address.
func NewAddress(network Network, value string) Address {
	return Address{
		Network: network,
		Value:   value,
	}
}

// Validate an address.
// Returns an error when something is incorrect or nil when all ok.
func (a Address) Validate() error {
	if err := a.Network.Validate(); err != nil {
		return err
	}
	if a.Network.Type.RequiresNumericValue() {
		if _, err := strconv.Atoi(a.Value); err != nil {
			return err
		}
	}
	return nil
}

// Equals returns true if both addresses are equal, false otherwise.
func (a Address) Equals(other Address) bool {
	return a.Network.Equals(other.Network) && a.Value == other.Value
}

// String returns a human readable representation of an address.
func (a Address) String() string {
	var result string
	if a.IsEmpty() {
		return ""
	}
	result = string(a.Network.Type) + " " + a.Value
	if a.Network.AddressSpace != "" {
		result = result + " " + a.Network.AddressSpace
	}
	return result
}

// IsEmpty return true if the given address is the zero value
func (a Address) IsEmpty() bool {
	return a.Network.IsEmpty() && a.Value == ""
}

func (a *Address) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	var s string
	if err := d.DecodeElement(&s, &start); err != nil {
		return err
	}
	addr, err := NewAddressFromString(s)
	if err != nil {
		return err
	}
	*a = addr
	return nil
}

func (a Address) MarshalXML(e *xml.Encoder, start xml.StartElement) error {
	return e.EncodeElement(a.String(), start)
}
