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
	"context"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/rs/zerolog"
)

// Entity adds implementation functions to state.Entity.
type Entity interface {
	state.Entity

	// Try to prepare the entity for use.
	// Returns nil when the entity is successfully prepared,
	// returns an error otherwise.
	TryPrepareForUse(context.Context, state.UserInterface, state.Persistence) error
	// Wrap up the preparation fase.
	FinalizePrepare(context.Context)
	// Set this entity's readiness for use in the live railway?
	SetIsReadyForUse(value bool)
}

type entity struct {
	log     zerolog.Logger
	entity  model.Entity
	railway Railway

	isReadyForUse bool
}

// Create a new entity
func newEntity(log zerolog.Logger, en model.Entity, railway Railway) entity {
	return entity{
		log:     log,
		entity:  en,
		railway: railway,
	}
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
func (e *entity) GetRailway() state.Railway {
	return e.railway
}

// Gets the railway state this object is a part of.
func (e *entity) GetRailwayImpl() Railway {
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

// Try to prepare the entity for use.
// Returns nil when the entity is successfully prepared or an error otherwise.
func prepareForUse(ctx context.Context, entity Entity, ui state.UserInterface, persistence state.Persistence) error {
	if !entity.GetIsReadyForUse() {
		if err := entity.TryPrepareForUse(ctx, ui, persistence); err != nil {
			return err
		}
		entity.SetIsReadyForUse(true)
	}
	return nil
}

// Wrap up preparation of the given entity.
func finalizePrepare(ctx context.Context, entity Entity) {
	if entity.GetIsReadyForUse() {
		entity.FinalizePrepare(ctx)
	}
}
