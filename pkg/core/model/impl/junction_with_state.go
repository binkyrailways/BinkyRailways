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
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// JunctionWithState adds implementation methods to model.JunctionWithState
type JunctionWithState interface {
	ModuleEntity
	model.JunctionWithState
}

type junctionWithState struct {
	propertyChanged eventHandler
	moduleEntityContainer

	JunctionID JunctionRef `xml:"Junction,omitempty"`
}

var _ JunctionWithState = &junctionWithState{}

// OnModified fires the propertychanged callbacks
func (j *junctionWithState) OnModified() {
	j.propertyChanged.Invoke(j)
	j.moduleEntityContainer.OnModified()
}

// A property of this entity has changed.
func (j *junctionWithState) PropertyChanged() model.EventHandler {
	return &j.propertyChanged
}

// GetID returns the ID of the junction
func (j *junctionWithState) GetID() string {
	return string(j.JunctionID)
}
func (j *junctionWithState) SetID(value string) error {
	return fmt.Errorf("Not implemented")
}

// GetDescription returns the description of the junction
func (j *junctionWithState) GetDescription() string {
	if x := j.GetJunction(); x != nil {
		return x.GetDescription()
	}
	return ""
}
func (j *junctionWithState) SetDescription(value string) error {
	return fmt.Errorf("Not implemented")
}

// GetTypeName returns the name of the type
func (j *junctionWithState) GetTypeName() string {
	return "" // TODO
}

// Does this entity generate it's own description?
func (j *junctionWithState) HasAutomaticDescription() bool {
	return true // TODO
}

// The junction involved
func (j *junctionWithState) GetJunction() model.Junction {
	if m := j.moduleEntityContainer.GetModule(); m != nil {
		x, _ := j.JunctionID.Get(m)
		return x
	}
	return nil
}

// Create a clone of this entity.
// Do not clone the junction.
func (j *junctionWithState) Clone() model.JunctionWithState {
	panic("Must be overwritten")
}
