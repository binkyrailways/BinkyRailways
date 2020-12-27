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

package impl

import (
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/refs"
)

// SwitchWithState adds implementation methods to model.SwitchWithState
type SwitchWithState interface {
	ModuleEntity
	model.SwitchWithState
}

type switchWithState struct {
	junctionWithState

	Direction *model.SwitchDirection `xml:"Direction,omitempty"`
}

var _ SwitchWithState = &switchWithState{}

func newSwitchWithState(r *route) *switchWithState {
	ssw := &switchWithState{}
	ssw.Initialize(r)
	return ssw
}

// Desired direction
func (j *switchWithState) GetDirection() model.SwitchDirection {
	return refs.SwitchDirectionValue(j.Direction, model.SwitchDirectionStraight)
}

// Create a clone of this entity.
// Do not clone the junction.
func (j *switchWithState) Clone() model.JunctionWithState {
	return &switchWithState{
		junctionWithState: junctionWithState{
			route:      j.route,
			JunctionID: j.JunctionID,
		},
		Direction: refs.NewSwitchDirection(j.GetDirection()),
	}
}
