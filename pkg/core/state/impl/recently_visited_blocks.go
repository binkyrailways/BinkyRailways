// Copyright 2022 Ewout Prangsma
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
	"time"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
)

type recentlyVisitedBlocks struct {
	exclusive util.Exclusive
	list      []state.Block
}

const (
	updateRecentlyVisitedBlocksTimeout  = time.Millisecond
	forEachRecentlyVisitedBlocksTimeout = time.Millisecond * 5
)

// newRecentlyVisitedBlocks constructs a new recently visited blocks list
func newRecentlyVisitedBlocks(exclusive util.Exclusive) *recentlyVisitedBlocks {
	return &recentlyVisitedBlocks{
		exclusive: exclusive,
	}
}

// Insert the given block to the head to the list, removing any other instances.
func (rvb *recentlyVisitedBlocks) Insert(ctx context.Context, block state.Block) {
	rvb.exclusive.Exclusive(ctx, updateRecentlyVisitedBlocksTimeout, "recentlyVisitedBlocks.Insert", func(c context.Context) error {
		// Removing existing instances of given block
		for i := 0; i < len(rvb.list); {
			if rvb.list[i] == block {
				rvb.list = append(rvb.list[:i], rvb.list[i+1:]...)
			} else {
				i++
			}
		}

		// Insert block at head of list
		rvb.list = append([]state.Block{block}, rvb.list...)

		return nil
	})
}

// Make a clone of the list of recently visited blocks in the given source and store that in rvb.
func (rvb *recentlyVisitedBlocks) CloneFrom(ctx context.Context, source *recentlyVisitedBlocks) {
	rvb.exclusive.Exclusive(ctx, updateRecentlyVisitedBlocksTimeout, "recentlyVisitedBlocks.CloneFrom", func(ctx context.Context) error {
		source.exclusive.Exclusive(ctx, updateRecentlyVisitedBlocksTimeout, "recentlyVisitedBlocks.CloneFrom", func(ctx context.Context) error {
			rvb.list = append([]state.Block{}, source.list...)
			return nil
		})
		return nil
	})
}

// Make a clone of the list of recently visited blocks in the given source and store that in rvb.
func (rvb *recentlyVisitedBlocks) ForEach(ctx context.Context, cb func(context.Context, state.Block) error) error {
	return rvb.exclusive.Exclusive(ctx, forEachRecentlyVisitedBlocksTimeout, "recentlyVisitedBlocks.ForEach", func(ctx context.Context) error {
		for _, b := range rvb.list {
			if err := cb(ctx, b); err != nil {
				return err
			}
		}
		return nil
	})
}
