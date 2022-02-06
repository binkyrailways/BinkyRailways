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
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// OutputWithState adds implementation methods to model.OutputWithState
type OutputWithState interface {
	ModuleEntity
	model.OutputWithState
}

type outputWithState struct {
	propertyChanged eventHandler
	moduleEntityContainer

	OutputID OutputRef `xml:"Output,omitempty"`
}

var _ OutputWithState = &outputWithState{}

// Accept a visit by the given visitor
func (ows *outputWithState) Accept(v model.EntityVisitor) interface{} {
	panic("Override me")
}

// OnModified fires the propertychanged callbacks
func (ows *outputWithState) OnModified() {
	ows.propertyChanged.Invoke(ows)
	ows.moduleEntityContainer.OnModified()
}

// A property of this entity has changed.
func (ows *outputWithState) PropertyChanged() model.EventHandler {
	return &ows.propertyChanged
}

// GetID returns the ID of the output
func (ows *outputWithState) GetID() string {
	return string(ows.OutputID)
}
func (ows *outputWithState) SetID(value string) error {
	return fmt.Errorf("Not implemented")
}

// GetDescription returns the description of the output
func (ows *outputWithState) GetDescription() string {
	if x := ows.GetOutput(); x != nil {
		return x.GetDescription()
	}
	return ""
}
func (ows *outputWithState) SetDescription(value string) error {
	return fmt.Errorf("Not implemented")
}

// GetTypeName returns the name of the type
func (ows *outputWithState) GetTypeName() string {
	return "" // TODO
}

// Does this entity generate it's own description?
func (ows *outputWithState) HasAutomaticDescription() bool {
	return true // TODO
}

// The output involved
func (ows *outputWithState) GetOutput() model.Output {
	if m := ows.moduleEntityContainer.GetModule(); m != nil {
		x, _ := ows.OutputID.Get(m)
		return x
	}
	return nil
}

// Create a clone of this entity.
// Do not clone the output.
func (ows *outputWithState) Clone() model.OutputWithState {
	panic("Must be overwritten")
}
