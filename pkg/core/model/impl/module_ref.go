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

// ModuleRef adds implementation methods to model.ModuleRef
type ModuleRef interface {
	model.ModuleRef
}

type moduleRef struct {
	positionedRailwayEntity
	onTryResolve func(id string) model.Module

	ZoomFactor *int `xml:"ZoomFactor,omitempty"`
}

var _ ModuleRef = &moduleRef{}

// newCommandStationRef creates a new cs ref
func newModuleRef(id string, onTryResolve func(id string) model.Module) moduleRef {
	mr := moduleRef{
		onTryResolve: onTryResolve,
	}
	mr.ID = id
	return mr
}

func (lr *moduleRef) SetResolver(onTryResolve func(id string) model.Module) {
	lr.onTryResolve = onTryResolve
}

// Get the Identification value.
func (lr *moduleRef) GetID() string {
	return lr.ID
}

// Get the Identification value.
func (lr *moduleRef) Set(value model.Module, onModified func()) error {
	id := ""
	if value == nil {
		id = value.GetID()
	}
	if lr.ID != id {
		lr.ID = id
		onModified()
	}
	return nil
}

/// Zoomfactor used in displaying the module (in percentage).
/// <value>100 means 100%</value>
func (lr *moduleRef) GetZoomFactor() int {
	return refs.IntValue(lr.ZoomFactor, model.DefaultModuleRefZoomFactor)
}
func (lr *moduleRef) SetZoomFactor(value int) error {
	if lr.GetZoomFactor() != value {
		lr.ZoomFactor = refs.NewInt(value)
		lr.OnModified()
	}
	return nil
}

// Is this module a reference to the given module?
func (lr *moduleRef) IsReferenceTo(module model.Module) bool {
	id := ""
	if module != nil {
		id = module.GetID()
	}
	return lr.GetID() == id
}

// Try to resolve the loc reference.
// Returns non-nil CommandStation or nil if not found.
func (lr *moduleRef) TryResolve() model.Module {
	if lr.onTryResolve == nil {
		return nil
	}
	return lr.onTryResolve(lr.ID)
}
