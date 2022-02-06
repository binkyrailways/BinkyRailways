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

// BlockGroup adds implementation functions to state.BlockGroup.
type BlockGroup interface {
	Entity
	state.BlockGroup
}

type blockGroup struct {
	entity

	blocks []Block
}

// Create a new entity
func newBlockGroup(en model.BlockGroup, railway Railway) BlockGroup {
	bg := &blockGroup{
		entity: newEntity(en, railway),
	}
	return bg
}

// getBlock returns the entity as BlockGroup.
func (bg *blockGroup) getBlockGroup() model.BlockGroup {
	return bg.GetEntity().(model.BlockGroup)
}

// GetModel returns the entity as BlockGroup.
func (bg *blockGroup) GetModel() model.BlockGroup {
	return bg.getBlockGroup()
}

// Try to prepare the entity for use.
// Returns nil when the entity is successfully prepared,
// returns an error otherwise.
func (bg *blockGroup) TryPrepareForUse(ctx context.Context, ui state.UserInterface, _ state.Persistence) error {
	// TODO
	return nil
}

// Wrap up the preparation fase.
func (bg *blockGroup) FinalizePrepare(ctx context.Context) {
}

// Gets all blocks in this group.
func (bg *blockGroup) ForEachBlock(cb func(state.Block)) {
	for _, b := range bg.blocks {
		cb(b)
	}
}

/// The minimum number of locs that must be present in this group.
/// Locs cannot leave if that results in a lower number of locs in this group.
func (bg *blockGroup) GetMinimumLocsInGroup(context.Context) int {
	return bg.getBlockGroup().GetMinimumLocsInGroup()
}

/// Is the condition met to require the minimum number of locs in this group?
func (bg *blockGroup) IsMinimumLocsInGroupEnabled(ctx context.Context) bool {
	// TODO
	return false
}

/// Are there enough locs in this group so that one lock can leave?
func (bg *blockGroup) GetFirstLocCanLeave(ctx context.Context) bool {
	// TODO
	return true
}
