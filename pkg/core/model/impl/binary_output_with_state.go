// Copyright 2022 Ewout Prangsma
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

// BinaryOutputWithState adds implementation methods to model.BinaryOutputWithState
type BinaryOutputWithState interface {
	ModuleEntity
	model.BinaryOutputWithState
}

type binaryOutputWithState struct {
	outputWithState

	Active *bool `xml:"Active,omitempty"`
}

var _ BinaryOutputWithState = &binaryOutputWithState{}

func newBinaryOutputWithState() *binaryOutputWithState {
	ssw := &binaryOutputWithState{}
	return ssw
}

// GetEntityType returns the type of this entity
func (j *binaryOutputWithState) GetEntityType() string {
	return TypeBinaryOutputWithState
}

// Accept a visit by the given visitor
func (j *binaryOutputWithState) Accept(v model.EntityVisitor) interface{} {
	return v.VisitBinaryOutputWithState(j)
}

// Desired active
func (j *binaryOutputWithState) GetActive() bool {
	return refs.BoolValue(j.Active, false)
}

// Desired active
func (j *binaryOutputWithState) SetActive(value bool) error {
	j.Active = refs.NewBool(value)
	return nil
}

// Create a clone of this entity.
// Do not clone the junction.
func (j *binaryOutputWithState) Clone() model.OutputWithState {
	result := &binaryOutputWithState{
		outputWithState: outputWithState{
			OutputID: j.OutputID,
		},
		Active: refs.NewBool(j.GetActive()),
	}
	result.SetContainer(j.container)
	return result
}
