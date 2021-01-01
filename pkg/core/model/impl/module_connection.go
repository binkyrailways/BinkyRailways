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
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/refs"
)

// ModuleConnection extends implementation methods to model.ModuleConnection
type ModuleConnection interface {
	RailwayEntity
	model.ModuleConnection
}

type moduleConnection struct {
	railwayEntity

	ModuleIDA *string `xml:"ModuleIdA,omitempty"`
	EdgeIDA   *string `xml:"EdgeIdA,omitempty"`
	ModuleIDB *string `xml:"ModuleIdB,omitempty"`
	EdgeIDB   *string `xml:"EdgeIdB,omitempty"`
}

var (
	_ ModuleConnection = &moduleConnection{}
)

// newLocGroup initialize a new module connection
func newModuleConnection() *moduleConnection {
	m := &moduleConnection{}
	m.EnsureID()
	return m
}

// The first module in the connection
func (mc *moduleConnection) GetModuleA() model.Module {
	id := refs.StringValue(mc.ModuleIDA, "")
	return mc.tryResolveModule(id)
}

// Edge of module A
func (mc *moduleConnection) GetEdgeA() model.Edge {
	id := refs.StringValue(mc.EdgeIDA, "")
	return mc.tryResolveEdge(id, mc.GetModuleA())
}
func (mc *moduleConnection) SetEdgeA(value model.Edge) error {
	id := ""
	modID := ""
	if value != nil {
		id = value.GetID()
		if value.GetModule() == nil {
			return fmt.Errorf("Edge has no valid module")
		}
		modID = value.GetModule().GetID()
	}
	if refs.StringValue(mc.EdgeIDA, "") != id {
		mc.ModuleIDA = refs.NewString(modID)
		mc.EdgeIDA = refs.NewString(id)
		mc.OnModified()
	}
	return nil
}

// The second module in the connection
func (mc *moduleConnection) GetModuleB() model.Module {
	id := refs.StringValue(mc.ModuleIDB, "")
	return mc.tryResolveModule(id)
}

// Edge of module B
func (mc *moduleConnection) GetEdgeB() model.Edge {
	id := refs.StringValue(mc.EdgeIDB, "")
	return mc.tryResolveEdge(id, mc.GetModuleB())
}
func (mc *moduleConnection) SetEdgeB(value model.Edge) error {
	id := ""
	modID := ""
	if value != nil {
		id = value.GetID()
		if value.GetModule() == nil {
			return fmt.Errorf("Edge has no valid module")
		}
		modID = value.GetModule().GetID()
	}
	if refs.StringValue(mc.EdgeIDB, "") != id {
		mc.ModuleIDB = refs.NewString(modID)
		mc.EdgeIDB = refs.NewString(id)
		mc.OnModified()
	}
	return nil
}

// Try to resolve a module ID into a module
func (mc *moduleConnection) tryResolveModule(id string) model.Module {
	if id == "" {
		return nil
	}
	rw := mc.GetRailway()
	if rw == nil {
		return nil
	}
	if mr, ok := rw.GetModules().Get(id); ok {
		return mr.TryResolve()
	}
	return nil
}

// Try to resolve an edge ID in the context of a module into an edge
func (mc *moduleConnection) tryResolveEdge(id string, module model.Module) model.Edge {
	if id == "" || module == nil {
		return nil
	}
	if e, ok := module.GetEdges().Get(id); ok {
		return e
	}
	return nil
}
