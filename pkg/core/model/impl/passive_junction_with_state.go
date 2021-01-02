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
)

// PassiveJunctionWithState adds implementation methods to model.PassiveJunctionWithState
type PassiveJunctionWithState interface {
	ModuleEntity
	model.PassiveJunctionWithState
}

type passiveJunctionWithState struct {
	junctionWithState
}

var _ PassiveJunctionWithState = &passiveJunctionWithState{}

func newPassiveJunctionWithState() *passiveJunctionWithState {
	ssw := &passiveJunctionWithState{}
	return ssw
}

// Accept a visit by the given visitor
func (j *passiveJunctionWithState) Accept(v model.EntityVisitor) interface{} {
	return v.VisitPassiveJunctionWithState(j)
}

// Create a clone of this entity.
// Do not clone the junction.
func (j *passiveJunctionWithState) Clone() model.JunctionWithState {
	result := &passiveJunctionWithState{
		junctionWithState: junctionWithState{
			JunctionID: j.JunctionID,
		},
	}
	result.SetContainer(j.container)
	return result
}
