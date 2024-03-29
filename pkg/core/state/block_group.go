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

package state

import (
	"context"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// BlockGroup specifies the state of a single block group.
type BlockGroup interface {
	// Gets the underlying model
	GetModel() model.BlockGroup

	// Gets all blocks in this group.
	ForEachBlock(func(Block))

	/// The minimum number of locs that must be present in this group.
	/// Locs cannot leave if that results in a lower number of locs in this group.
	GetMinimumLocsInGroup(context.Context) int

	/// Is the condition met to require the minimum number of locs in this group?
	IsMinimumLocsInGroupEnabled(context.Context) bool

	/// Are there enough locs in this group so that one lock can leave?
	GetFirstLocCanLeave(context.Context) bool
}
