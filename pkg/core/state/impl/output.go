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

// Output adds implementation functions to state.Output.
type Output interface {
	Entity
	state.Output
}

type output struct {
	entity
	lockable
}

// Create a new entity
func newOutput(en model.Output, railway Railway) output {
	s := output{
		entity: newEntity(railway.Logger().With().Str("output", en.GetDescription()).Logger(), en, railway),
	}
	s.lockable = newLockable(railway, func(ctx context.Context, f func(state.Lockable) error) error {
		return nil
	})
	return s
}

// getOutput returns the entity as Output.
func (s *output) getOutput() model.Output {
	return s.GetEntity().(model.Output)
}

// Gets the underlying model
func (s *output) GetModel() model.Output {
	return s.getOutput()
}
