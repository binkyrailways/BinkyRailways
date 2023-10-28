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

package state

import (
	"fmt"
	"strings"
)

// LocDirection describes the direction of a loc.
type LocDirection uint8

const (
	// LocDirectionForward means the loc must go forward
	LocDirectionForward LocDirection = 0
	// LocDirectionReverse means the loc must go in reverse
	LocDirectionReverse LocDirection = 1
)

func (d LocDirection) String() string {
	if d == LocDirectionForward {
		return "Forward"
	}
	return "Reverse"
}

func (d LocDirection) Invert() LocDirection {
	if d == LocDirectionForward {
		return LocDirectionReverse
	}
	return LocDirectionForward
}

// ParseLocDirection converts a string into a loc direction.
func ParseLocDirection(input string) (LocDirection, error) {
	switch strings.ToLower(input) {
	case "forward":
		return LocDirectionForward, nil
	case "reverse":
		return LocDirectionReverse, nil
	default:
		return 0, fmt.Errorf("Unknown LocDirection '%s'", input)
	}
}
