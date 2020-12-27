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

// Module extends implementation methods to model.Module
type Module interface {
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
func NewModule() Module {
	m := &module{}
	m.EnsureID()
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

/// Gets all edges of this module.
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

/// <summary>
/// Gets/sets the background image of the this module.
/// </summary>
/// <value>Null if there is no image.</value>
/// <remarks>Image must be png, bmp, gif, jpg, wmf or emf</remarks>
//Stream BackgroundImage { get; set; }

// Gets the horizontal size (in pixels) of this entity.
func (m *module) GetWidth() int {
	maxw := 0
	m.ForEachPositionedEntity(func(item model.PositionedEntity) {
		w := item.GetWidth()
		if w > maxw {
			maxw = w
		}
	})
	return maxw
}

// Gets the vertical size (in pixels) of this entity.
func (m *module) GetHeight() int {
	maxh := 0
	m.ForEachPositionedEntity(func(item model.PositionedEntity) {
		h := item.GetHeight()
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
