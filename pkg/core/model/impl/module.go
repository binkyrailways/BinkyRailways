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

const (
	moduleBackgroundImageID = "BackgroundImage"
)

// Module extends implementation methods to model.Module
type Module interface {
	Entity
	model.Module
	PersistentEntity

	GetEndPoint(id string) (model.EndPoint, bool)
}

type module struct {
	entity
	persistentEntity

	Blocks      blockSet      `xml:"Blocks"`
	BlockGroups blockGroupSet `xml:"BlockGroups"`
	Edges       edgeSet       `xml:"Edges"`
	Outputs     outputSet     `xml:"Outputs"`
	Junctions   junctionSet   `xml:"Junctions"`
	Sensors     sensorSet     `xml:"Sensors"`
	Signals     signalSet     `xml:"Signals"`
	Routes      routeSet      `xml:"Routes"`
}

var (
	_ Module = &module{}
)

// NewModule initialize a new module
func NewModule(p model.Package) Module {
	m := &module{}
	m.EnsureID()
	m.SetPackage(p)
	m.SetDescription("New module")
	m.persistentEntity.Initialize(m.entity.OnModified)
	m.Blocks.SetContainer(m)
	m.BlockGroups.SetContainer(m)
	m.Edges.SetContainer(m)
	m.Outputs.SetContainer(m)
	m.Junctions.SetContainer(m)
	m.Sensors.SetContainer(m)
	m.Signals.SetContainer(m)
	m.Routes.SetContainer(m)
	return m
}

// GetEntityType returns the type of this entity
func (m *module) GetEntityType() string {
	return TypeModule
}

// Accept a visit by the given visitor
func (m *module) Accept(v model.EntityVisitor) interface{} {
	return v.VisitModule(m)
}

// ForEachAddress iterates all addresses in this entity and any child entities.
func (m *module) ForEachAddress(cb func(addr model.Address, onUpdate func(context.Context, model.Address) error)) {
	m.Blocks.ForEachAddress(cb)
	m.BlockGroups.ForEachAddress(cb)
	m.Edges.ForEachAddress(cb)
	m.Outputs.ForEachAddress(cb)
	m.Junctions.ForEachAddress(cb)
	m.Sensors.ForEachAddress(cb)
	m.Signals.ForEachAddress(cb)
	m.Routes.ForEachAddress(cb)
}

// Return the containing module
func (m *module) GetModule() model.Module {
	return m
}

// Gets all blocks contained in this module.
func (m *module) GetBlocks() model.BlockSet {
	return &m.Blocks
}

// Gets all block groups contained in this module.
func (m *module) GetBlockGroups() model.BlockGroupSet {
	return &m.BlockGroups
}

// / Gets all edges of this module.
func (m *module) GetEdges() model.EdgeSet {
	return &m.Edges
}

// Gets all junctions contained in this module.
func (m *module) GetJunctions() model.JunctionSet {
	return &m.Junctions
}

// Gets all sensors contained in this module.
func (m *module) GetSensors() model.SensorSet {
	return &m.Sensors
}

// Gets all signals contained in this module.
func (m *module) GetSignals() model.SignalSet {
	return &m.Signals
}

// Gets all outputs contained in this module.
func (m *module) GetOutputs() model.OutputSet {
	return &m.Outputs
}

// Gets all routes contained in this module.
func (m *module) GetRoutes() model.RouteSet {
	return &m.Routes
}

// Gets/sets the background image of the this module.
// Null if there is no image.</value>
// Image must be png, bmp, gif, jpg, wmf or emf.
func (m *module) GetBackgroundImage() []byte {
	if pkg := m.GetPackage(); pkg != nil {
		img, _ := pkg.GetGenericPart(m, moduleBackgroundImageID)
		return img
	}
	return nil
}
func (m *module) SetBackgroundImage(value []byte) error {
	return fmt.Errorf("Not implemented")
}

// Gets the horizontal size (in pixels) of this entity.
func (m *module) GetWidth() int {
	maxw := 1
	m.ForEachPositionedEntity(func(item model.PositionedEntity) {
		w := item.GetX() + item.GetWidth()
		if w > maxw {
			maxw = w
		}
	})
	return maxw
}

// Gets the vertical size (in pixels) of this entity.
func (m *module) GetHeight() int {
	maxh := 1
	m.ForEachPositionedEntity(func(item model.PositionedEntity) {
		h := item.GetY() + item.GetHeight()
		if h > maxh {
			maxh = h
		}
	})
	return maxh
}

// Call the callback for each positioned item in the module
func (m *module) ForEachPositionedEntity(cb func(model.PositionedEntity)) {
	m.Blocks.ForEach(func(item model.Block) { cb(item) })
	m.Edges.ForEach(func(item model.Edge) { cb(item) })
	m.Junctions.ForEach(func(item model.Junction) { cb(item) })
	m.Outputs.ForEach(func(item model.Output) { cb(item) })
	m.Sensors.ForEach(func(item model.Sensor) { cb(item) })
	m.Signals.ForEach(func(item model.Signal) { cb(item) })
}

// Call the given callback for all (non-empty) addresses configured in this
// module with the direction their being used.
// If addresses are used by multiple entities, they are enumerated multiple times.
func (m *module) ForEachAddressUsage(cb func(model.AddressUsage)) {
	cbHelper := func(item interface{}) {
		if x, ok := item.(model.AddressEntity); ok {
			x.ForEachAddressUsage(cb)
		}
	}
	m.Junctions.ForEach(func(item model.Junction) { cbHelper(item) })
	m.Outputs.ForEach(func(item model.Output) { cbHelper(item) })
	m.Sensors.ForEach(func(item model.Sensor) { cbHelper(item) })
	m.Signals.ForEach(func(item model.Signal) { cbHelper(item) })
}

// Upgrade to latest version
func (m *module) Upgrade() {
	// Empty on purpose
}

func (m *module) GetEndPoint(id string) (model.EndPoint, bool) {
	// Try block
	if b, ok := m.Blocks.Get(id); ok {
		return b, true
	}
	// Try edge
	if e, ok := m.Edges.Get(id); ok {
		return e, true
	}
	return nil, false
}
