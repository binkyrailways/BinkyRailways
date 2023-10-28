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

// SwitchDirection indicates a switching direction of a switch.
type SwitchDirection string

const (
	// SwitchDirectionStraight indicates the switch in its straight position
	SwitchDirectionStraight SwitchDirection = "Straight"
	// SwitchDirectionOff indicates the switch in its off position
	SwitchDirectionOff SwitchDirection = "Off"
)

// Invert returns the inverted direction.
func (sd SwitchDirection) Invert() SwitchDirection {
	if sd == SwitchDirectionStraight {
		return SwitchDirectionOff
	}
	return SwitchDirectionStraight
}
