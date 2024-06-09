// Copyright 2023 Ewout Prangsma
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

type blockAndSide struct {
	Block     state.Block
	EnterSide model.BlockSide
}

// Initialize a new blockAndSide from given route.
func newBlockAndSide(ctx context.Context, rt state.Route) blockAndSide {
	return blockAndSide{
		Block:     rt.GetTo(),
		EnterSide: rt.GetToBlockSide(),
	}
}

// Equals returns true when bs & other have the same values
func (bs blockAndSide) Equals(other blockAndSide) bool {
	return bs.Block == other.Block &&
		bs.EnterSide == other.EnterSide
}

type blockAndSides []blockAndSide

// Contains returns true if the list contains an entry
// with given values.
func (list blockAndSides) Contains(entry blockAndSide) bool {
	for _, x := range list {
		if x.Equals(entry) {
			return true
		}
	}
	return false
}

// Build the set of critical routes from the given route.
func buildCriticalSectionRoutes(ctx context.Context, route state.Route, rw state.Railway) ([]state.Route, error) {
	var blocks blockAndSides
	iterator := newBlockAndSide(ctx, route)
	for {
		// Are there any routes to the opposite side of the iterator block?
		if !anyRoutesTo(ctx, rw, iterator.Block, iterator.EnterSide.Invert()) {
			// No routes leading into the opposite side of the to block.
			// No further critical section
			break

		}
		if len(blocks) > 0 {
			// Are there more then 'exits' from the iterator
			iteratorExits := getNextBlocks(ctx, rw, iterator.Block, iterator.EnterSide)
			if len(iteratorExits) > 1 {
				// Multiple routes in the opposite direction possible
				break
			}
		}
		// Take routes to the iterator into the critical section
		blocks = append(blocks, iterator)

		// Find the blocks where we can go to from the iterator.
		nextBlocks := getNextBlocks(ctx, rw, iterator.Block, iterator.EnterSide.Invert())
		if len(nextBlocks) != 1 {
			// We've found multiple or no routes, stop now
			break
		}
		// Only 1 route found, continue
		iterator = nextBlocks[0]
		if blocks.Contains(iterator) {
			// Circle found, stop now
			break
		}
	}

	// Now build a list of all routes targeting the blocks (and sides) in the block list.
	var routes []state.Route
	for _, b := range blocks {
		targetBlock := b.Block
		targetSide := b.EnterSide.Invert()
		rw.ForEachRoute(func(r state.Route) {
			if r.GetTo() == targetBlock &&
				r.GetToBlockSide() == targetSide {
				routes = append(routes, r)
			}
		})
	}

	// Look for the reverse of this route (if any)
	if reverse := getReverseRoute(ctx, rw, route); reverse != nil {
		routes = append(routes, reverse)
	}

	return routes, nil
}

// Are there any routes leading to the given block that enter the to block
// at the given to side?
func anyRoutesTo(ctx context.Context, rw state.Railway, toBlock state.Block, toSide model.BlockSide) bool {
	result := false
	rw.ForEachRoute(func(r state.Route) {
		if !result {
			if r.GetTo() == toBlock &&
				r.GetToBlockSide() == toSide {
				result = true
			}
		}
	})
	return result
}

// Create a list of blocks reachable from the given block.
func getNextBlocks(ctx context.Context, rw state.Railway, fromBlock state.Block, fromSide model.BlockSide) []blockAndSide {
	result := make([]blockAndSide, 0, rw.GetRouteCount(ctx))
	rw.ForEachRoute(func(r state.Route) {
		if r.GetFrom() == fromBlock &&
			r.GetFromBlockSide() == fromSide {
			result = append(result, newBlockAndSide(ctx, r))
		}
	})
	return result
}

// Find the route that is the reverse of the given route.
// Returns nil if not found.
func getReverseRoute(ctx context.Context, rw state.Railway, route state.Route) state.Route {
	// Get reversed from-to
	from := route.GetTo()
	to := route.GetFrom()
	fromBlockSide := route.GetToBlockSide()
	toBlockSide := route.GetFromBlockSide()

	if (from == nil) || (to == nil) {
		return nil
	}

	var result state.Route
	rw.ForEachRoute(func(r state.Route) {
		if result == nil {
			if r.GetTo() == to &&
				r.GetFrom() == from &&
				r.GetToBlockSide() == toBlockSide &&
				r.GetFromBlockSide() == fromBlockSide {
				result = r
			}
		}
	})
	return result
}
