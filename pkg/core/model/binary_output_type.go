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

import (
	"fmt"
	"strings"
)

// BinaryOutputType is a strongly typed type of binary output.
type BinaryOutputType string

const (
	// BinaryOutputTypeDefault indicates a standard on/off switch
	BinaryOutputTypeDefault BinaryOutputType = "Default"
	// BinaryOutputTypeTrackInverter indicates a digital track inverter
	BinaryOutputTypeTrackInverter BinaryOutputType = "TrackInverter"
)

var (
	// AllBinaryOutputTypes contains all possible binary output types
	AllBinaryOutputTypes = []BinaryOutputType{
		BinaryOutputTypeDefault,
		BinaryOutputTypeTrackInverter,
	}
)

// ParseBinaryOutputType parses the given input into an BinaryOutputType.
func ParseBinaryOutputType(input string) (BinaryOutputType, error) {
	if input == "" {
		return BinaryOutputTypeDefault, nil
	}
	upperInput := strings.ToUpper(input)
	for _, x := range AllBinaryOutputTypes {
		if strings.ToUpper(string(x)) == upperInput {
			return x, nil
		}
	}
	return "", fmt.Errorf("Unknown binary output type '%s", input)
}

// Validate checks if the given binary output type is valid
func (tp BinaryOutputType) Validate() error {
	switch tp {
	case BinaryOutputTypeDefault, BinaryOutputTypeTrackInverter:
		return nil // Ok
	default:
		return fmt.Errorf("Unknown binary output type type '%s", string(tp))
	}
}
