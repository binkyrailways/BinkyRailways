// Copyright 2021 Ewout Prangsma
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
	modelimpl "github.com/binkyrailways/BinkyRailways/pkg/core/model/impl"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// Entity adds implementation functions to state.Entity.
type Entity interface {
	state.Entity

	// Set this entity's readiness for use in the live railway?
	SetIsReadyForUse(value bool)
}

type entity struct {
	entity  model.Entity
	railway state.Railway

	requestedStateChanged model.EventHandler
	actualStateChanged    model.EventHandler
	isReadyForUse         bool
}

// Initialize the state
func (e *entity) Initialize(en model.Entity, railway state.Railway) {
	e.entity = en
	e.railway = railway
	e.requestedStateChanged = modelimpl.NewEventHandler()
	e.actualStateChanged = modelimpl.NewEventHandler()
}

// A requested value of a property of this state object has changed.
func (e *entity) RequestedStateChanged() model.EventHandler {
	return e.requestedStateChanged
}

// An actual value of a property of this state object has changed.
func (e *entity) ActualStateChanged() model.EventHandler {
	return e.actualStateChanged
}

// Gets the underlying entity
func (e *entity) GetEntity() model.Entity {
	return e.entity
}

// Unique ID of the underlying entity
func (e *entity) GetID() string {
	return e.entity.GetID()
}

// Description of the underlying entity
func (e *entity) GetDescription() string {
	return e.entity.GetDescription()
}

// Gets the railway state this object is a part of.
func (e *entity) GetRailwayState() state.Railway {
	return e.railway
}

// Is this entity fully resolved such that is can be used in the live railway?
func (e *entity) GetIsReadyForUse() bool {
	return e.isReadyForUse
}

// Set this entity's readiness for use in the live railway?
func (e *entity) SetIsReadyForUse(value bool) {
	e.isReadyForUse = value
}
