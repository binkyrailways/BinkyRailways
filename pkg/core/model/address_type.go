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

// AddressType is a strongly typed type of address
type AddressType string

const (
	AddressTypeBinkyNet AddressType = "BinkyNet"
	AddressTypeDcc      AddressType = "Dcc"
	AddressTypeLocoNet  AddressType = "LocoNet"
	AddressTypeMotorola AddressType = "Motorola"
	AddressTypeMfx      AddressType = "Mfx"
	AddressTypeMqtt     AddressType = "Mqtt"
)

var (
	allAddressTypes = []AddressType{
		AddressTypeBinkyNet,
		AddressTypeDcc,
		AddressTypeLocoNet,
		AddressTypeMotorola,
		AddressTypeMfx,
		AddressTypeMqtt,
	}
)

// ParseAddressType parses the given input into an AddressType.
func ParseAddressType(input string) (AddressType, error) {
	upperInput := strings.ToUpper(input)
	for _, x := range allAddressTypes {
		if strings.ToUpper(string(x)) == upperInput {
			return x, nil
		}
	}
	return "", fmt.Errorf("Unknown address type '%s", input)
}

// Validate checks if the given address type is valid
func (tp AddressType) Validate() error {
	switch tp {
	case AddressTypeBinkyNet, AddressTypeDcc, AddressTypeLocoNet, AddressTypeMotorola, AddressTypeMfx, AddressTypeMqtt:
		return nil // Ok
	default:
		return fmt.Errorf("Unknown address type '%s", string(tp))
	}
}

// RequiresNumericValue returns true if the given type require a numeric value,
// false otherwise.
func (tp AddressType) RequiresNumericValue() bool {
	switch tp {
	case AddressTypeDcc, AddressTypeLocoNet, AddressTypeMotorola, AddressTypeMfx:
		return true
	case AddressTypeBinkyNet, AddressTypeMqtt:
		return false
	default:
		panic("Unknown address type " + tp)
	}
}

// MaxValue returns the maximum possible value for an address of the given type.
func (tp AddressType) MaxValue() int {
	switch tp {
	case AddressTypeDcc:
		return 9999 // 0x27FF;
	case AddressTypeLocoNet:
		return 4096
	case AddressTypeMotorola:
		return 80
	case AddressTypeMfx:
		return 16000
	case AddressTypeBinkyNet, AddressTypeMqtt:
		return 0
	default:
		panic("Unknown address type " + tp)
	}
}

// MinValue returns the minimum possible value for an address of the given type.
func (tp AddressType) MinValue() int {
	switch tp {
	case AddressTypeDcc:
		return 0
	case AddressTypeLocoNet:
		return 1
	case AddressTypeMotorola:
		return 1
	case AddressTypeMfx:
		return 1
	case AddressTypeBinkyNet, AddressTypeMqtt:
		return 0
	default:
		panic("Unknown address type " + tp)
	}
}
