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

// Route availability tester for the current state of the railway.
type futureRouteAvailabilityTester struct {
	baseRouteAvailabilityTester
	locStates map[string]*futureLocState
}

type futureLocState struct {
	Loc                   state.Loc
	CurrentBlock          state.Block
	CurrentBlockEnterSide model.BlockSide
}

var _ RouteAvailabilityTester = &futureRouteAvailabilityTester{}

// NewFutureRouteAvailabilityTester constructs a new route availability tester
func NewFutureRouteAvailabilityTester(ctx context.Context, railway state.Railway) *futureRouteAvailabilityTester {
	// Prepare tester
	rt := &futureRouteAvailabilityTester{
		baseRouteAvailabilityTester: baseRouteAvailabilityTester{
			Railway: railway,
		},
		locStates: make(map[string]*futureLocState),
	}
	rt.baseRouteAvailabilityTester.RouteAvailabilityTester = rt
	// Initialize loc states
	railway.ForEachLoc(func(l state.Loc) {
		rt.locStates[l.GetID()] = &futureLocState{
			Loc:                   l,
			CurrentBlock:          l.GetCurrentBlock().GetActual(ctx),
			CurrentBlockEnterSide: l.GetCurrentBlockEnterSide().GetActual(ctx),
		}
	})
	return rt
}

func (s *futureLocState) Clone() *futureLocState {
	clone := &futureLocState{}
	*clone = *s
	return clone
}

func (s *futureLocState) ChangeTo(currentBlock state.Block, currentBlockEnterSide model.BlockSide) {
	s.CurrentBlock = currentBlock
	s.CurrentBlockEnterSide = currentBlockEnterSide
}

// Clone creates a deep copy
func (rt *futureRouteAvailabilityTester) Clone() *futureRouteAvailabilityTester {
	clone := &futureRouteAvailabilityTester{
		baseRouteAvailabilityTester: baseRouteAvailabilityTester{
			Railway: rt.Railway,
		},
		locStates: make(map[string]*futureLocState),
	}
	clone.baseRouteAvailabilityTester.RouteAvailabilityTester = clone
	// Initialize loc states
	for k, v := range rt.locStates {
		clone.locStates[k] = v.Clone()
	}
	return clone
}

// Let the given loc take the given route.
func (rt *futureRouteAvailabilityTester) TakeRoute(ctx context.Context, route state.Route, loc state.Loc) {
	rt.locStates[loc.GetID()].ChangeTo(route.GetTo(), route.GetToBlockSide())
}

// Gets the current block of the given loc.
func (rt *futureRouteAvailabilityTester) getCurrentBlock(loc state.Loc) state.Block {
	return rt.locStates[loc.GetID()].CurrentBlock
}

// Gets the current block enter side of the given loc.
func (rt *futureRouteAvailabilityTester) getCurrentBlockEnterSide(loc state.Loc) model.BlockSide {
	return rt.locStates[loc.GetID()].CurrentBlockEnterSide
}

// Gets the loc that has locked the given block.
func (rt *futureRouteAvailabilityTester) getLockedBy(ctx context.Context, block state.Block) state.Loc {
	for _, v := range rt.locStates {
		if v.CurrentBlock == block {
			return v.Loc
		}
	}
	return nil
}

// Can the given route be locked for the given loc?
// Returns: lockedBy, canLock
func (rt *futureRouteAvailabilityTester) CanLock(ctx context.Context, route state.Route, loc state.Loc) (state.Loc, bool) {
	lockedBy := rt.getLockedBy(ctx, route.GetTo())
	return lockedBy, lockedBy == nil || lockedBy == loc
}

// Is there are traffic in the opposite direction of the given to-block of a route?
func (rt *futureRouteAvailabilityTester) HasTrafficInOppositeDirection(ctx context.Context, toBlock state.Block, toBlockSide model.BlockSide, currentLoc state.Loc) bool {
	loc := rt.getLockedBy(ctx, toBlock)
	if (loc != nil) && (loc != currentLoc) {
		// Check direction
		locEnterSide := rt.getCurrentBlockEnterSide(loc)
		if locEnterSide != toBlockSide {
			// loc is in opposing direction
			if !canChangeDirectionIn(ctx, loc, toBlock) {
				// The loc cannot change direction in to block, so there is absolutely opposite traffic.
				return true
			}
		} else {
			// Loc is in same direction, we're ok
			return false
		}
	}
	return false
}

// Is the critical section for the given route free for the given loc?
func (rt *futureRouteAvailabilityTester) IsCriticalSectionFree(ctx context.Context, route state.Route, loc state.Loc) bool {
	return true
}

// Is any of the sensors of the given route active?
// Sensors that are also in the current route of the given loc are ignored.
func (rt *futureRouteAvailabilityTester) IsAnySensorActive(ctx context.Context, route state.Route, loc state.Loc) bool {
	return false
}
