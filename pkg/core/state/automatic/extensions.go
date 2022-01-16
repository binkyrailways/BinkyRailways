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

package automatic

import (
	"context"
	"math/rand"
	"time"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

func init() {
	rand.Seed(time.Now().Unix())
}

// Is is needed for a loc facing in the given direction (in the From block of the given route)
// to chance direction when taking this route?
// route: The route being investigated
// locDirection: The direction the loc is facing in the From block of the given <see cref="route"/>.
func isDirectionChangeNeeded(ctx context.Context, route state.Route, locDirection model.BlockSide) bool {
	fromSide := route.GetFromBlockSide()
	return (fromSide != locDirection)
}

// Is the given loc allowed to leave its current block?
func canLeaveCurrentBlock(ctx context.Context, loc state.Loc) bool {
	currentBlock := loc.GetCurrentBlock().GetActual(ctx)
	if currentBlock == nil {
		return true
	}
	currentBlockGroup := currentBlock.GetBlockGroup(ctx)
	return ((currentBlockGroup == nil) || (currentBlockGroup.GetFirstLocCanLeave(ctx)))
}

// Can the given loc change direction in the given block?
func canChangeDirectionIn(ctx context.Context, loc state.Loc, block state.Block) bool {
	return (loc.GetChangeDirection(ctx) == model.ChangeDirectionAllow) &&
		(block.GetChangeDirection(ctx) == model.ChangeDirectionAllow)
}

// Is the given block consider a station for the given loc?
// Note: It is considered a station if the loc is allowed to stop in the block and the wait possibility is greater then 50%.
func isStationFor(ctx context.Context, block state.Block, loc state.Loc) bool {
	return (block.GetIsStation(ctx)) && block.GetWaitPermissions().Evaluate(ctx, loc)
}

// Take a gamble.
// The result is true or false, where the given chance is for "true".
func gamble(percent int) bool {
	//if ((chance < 0) || (chance > 100))
	//    throw new ArgumentOutOfRangeException("chance", chance, "Must be between 0..100");
	if percent == 0 {
		return false
	}
	if percent == 100 {
		return true
	}
	value := rand.Intn(100)
	return (value <= percent)
}
