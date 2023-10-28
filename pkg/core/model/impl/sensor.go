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
	"context"
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Sensor adds implementation methods to model.Sensor
type Sensor interface {
	Entity
	ModuleEntity
	model.Sensor
}

type sensor struct {
	positionedModuleEntity

	Address model.Address `xml:"Address"`
	BlockID string        `xml:"Block,omitempty"`
	Shape   model.Shape   `xml:"Shape,omitempty"`
}

var _ Sensor = &sensor{}

// Initialize the sensor after construction
func (s *sensor) Initialize(w, h int) {
	s.positionedModuleEntity.Initialize(w, h)
}

// ForEachAddress iterates all addresses in this entity and any child entities.
func (s *sensor) ForEachAddress(cb func(addr model.Address, onUpdate func(context.Context, model.Address) error)) {
	cb(s.Address, s.SetAddress)
}

// Get the Address of the entity
func (s *sensor) GetAddress() model.Address {
	return s.Address
}

// Set the Address of the entity
func (s *sensor) SetAddress(ctx context.Context, value model.Address) error {
	if !s.Address.Equals(value) {
		s.Address = value
		s.OnModified()
	}
	return nil
}

// Call the given callback for all (non-empty) addresses configured in this
// entity with the direction their being used.
func (s *sensor) ForEachAddressUsage(cb func(model.AddressUsage)) {
	if !s.Address.IsEmpty() {
		cb(model.AddressUsage{
			Address:   s.Address,
			Direction: model.AddressDirectionInput,
		})
	}
}

// The block that this sensor belongs to.
// When set, this connection is used in the loc-to-block assignment process.
func (s *sensor) GetBlock() model.Block {
	m := s.GetModule()
	if m == nil || s.BlockID == "" {
		return nil
	}
	b, _ := m.GetBlocks().Get(s.BlockID)
	return b
}
func (s *sensor) SetBlock(value model.Block) error {
	id := ""
	var module model.Module
	if value != nil {
		id = value.GetID()
		module = value.GetModule()
	}
	if s.BlockID != id {
		if module != s.GetModule() {
			return fmt.Errorf("Invalid module")
		}
		s.BlockID = id
		s.OnModified()
	}
	return nil
}

// Shape used to visualize this sensor
func (s *sensor) GetShape() model.Shape {
	return s.Shape
}
func (s *sensor) SetShape(value model.Shape) error {
	if s.Shape != value {
		s.Shape = value
		s.OnModified()
	}
	return nil
}

// Ensure implementation implements Sensor
func (*sensor) ImplementsSensor() {
	// Nothing here
}
