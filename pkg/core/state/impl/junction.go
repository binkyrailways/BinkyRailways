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
)

// Junction adds implementation functions to state.Junction.
type Junction interface {
	Entity
	state.Junction
}

type junction struct {
	entity
	lockable
}

// Create a new entity
func newJunction(en model.Junction, this state.Lockable, railway Railway) junction {
	s := junction{
		entity: newEntity(railway.Logger().With().Str("junction", en.GetDescription()).Logger(), en, railway),
	}
	s.lockable = newLockable(railway, this, func(c context.Context, f func(state.Lockable) error) error {
		return nil
	})
	return s
}

// Unique ID of the module containing this entity
func (s *junction) GetModuleID() string {
	return s.getJunction().GetModule().GetID()
}

// getJunction returns the entity as Junction.
func (s *junction) getJunction() model.Junction {
	return s.GetEntity().(model.Junction)
}

// Gets the underlying model
func (s *junction) GetModel() model.Junction {
	return s.getJunction()
}
