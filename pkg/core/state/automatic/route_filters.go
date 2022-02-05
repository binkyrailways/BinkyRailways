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

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// Gets a selection of all possible routes from the given block.
func getAllPossibleRoutesFromBlock(fromBlock state.Block) RoutePredicate {
	return func(ctx context.Context, r state.Route) bool {
		return r.GetFrom(ctx).GetID() == fromBlock.GetID()
	}
}

// Gets a selection of all possible non-closed routes from the given block.
func getAllPossibleNonClosedRoutesFromBlock(fromBlock state.Block) RoutePredicate {
	return getAllPossibleRoutesFromBlock(fromBlock).And(func(ctx context.Context, r state.Route) bool {
		return !r.GetClosed(ctx)
	})
}

// Gets a selection of all possible routes from the given block + side.
func getAllPossibleRoutesFromBlockWithSide(fromBlock state.Block, fromBlockSide model.BlockSide) RoutePredicate {
	return getAllPossibleRoutesFromBlock(fromBlock).And(func(ctx context.Context, r state.Route) bool {
		return r.GetFromBlockSide(ctx) == fromBlockSide
	})
}

// Gets a selection of all possible non-closed routes from the given block + side.
func getAllPossibleNonClosedRoutesFromBlockWithSide(fromBlock state.Block, fromBlockSide model.BlockSide) RoutePredicate {
	return getAllPossibleRoutesFromBlockWithSide(fromBlock, fromBlockSide).And(func(ctx context.Context, r state.Route) bool {
		return !r.GetClosed(ctx)
	})
}
