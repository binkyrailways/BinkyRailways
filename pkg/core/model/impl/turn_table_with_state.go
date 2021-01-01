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

// TurnTableWithState adds implementation methods to model.TurnTableWithState
type TurnTableWithState interface {
	ModuleEntity
	model.TurnTableWithState
}

type turnTableWithState struct {
	junctionWithState

	Position *int `xml:"Position,omitempty"`
}

var _ TurnTableWithState = &turnTableWithState{}

func newTurnTableWithState() *turnTableWithState {
	ssw := &turnTableWithState{}
	return ssw
}

// Desired position
func (j *turnTableWithState) GetPosition() int {
	return refs.IntValue(j.Position, model.DefaultTurnTableInitialPosition)
}

// Create a clone of this entity.
// Do not clone the junction.
func (j *turnTableWithState) Clone() model.JunctionWithState {
	result := &turnTableWithState{
		junctionWithState: junctionWithState{
			JunctionID: j.JunctionID,
		},
		Position: refs.NewInt(j.GetPosition()),
	}
	result.SetContainer(j.container)
	return result
}
