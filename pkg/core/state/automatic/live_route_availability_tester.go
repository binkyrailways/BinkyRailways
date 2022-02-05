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
type liveRouteAvailabilityTester struct {
	baseRouteAvailabilityTester
}

var _ RouteAvailabilityTester = &liveRouteAvailabilityTester{}

// NewLiveRouteAvailabilityTester constructs a new route availability tester
func NewLiveRouteAvailabilityTester(railway state.Railway) *liveRouteAvailabilityTester {
	rt := &liveRouteAvailabilityTester{
		baseRouteAvailabilityTester: baseRouteAvailabilityTester{
			Railway: railway,
		},
	}
	rt.baseRouteAvailabilityTester.RouteAvailabilityTester = rt
	return rt
}

// Can the given route be taken by the given loc?
// route: The route being investigated
// loc: The loc a route should be choosen for
// locDirection: The direction the loc is facing in the From block of the given <see cref="route"/>.
// avoidDirectionChanges: If true, the route is considered not available if a direction change is needed.
// Returns: True if the route can be locked and no sensor in the route is active (outside current route).
func (rt *liveRouteAvailabilityTester) IsAvailableFor(ctx context.Context, route state.Route, loc state.Loc, locDirection model.BlockSide, avoidDirectionChanges bool) state.RouteOption {
	// Perform standard testing first
	result := rt.baseRouteAvailabilityTester.IsAvailableFor(ctx, route, loc, locDirection, avoidDirectionChanges)
	if !result.IsPossible {
		return result
	}

	// Now test future possibilities of the entire railway to detect possible deadlocks.
	genSet := newFutureAlternativeSet(ctx, rt, route, loc)
	if !genSet.Test(ctx) {
		// Not Ok
		return state.RouteOption{
			Route:  route,
			Reason: state.RouteImpossibleReasonDeadLock,
		}
	}
	return result
}

// Set of alternatives.
// At least one generation must still be alive after X alternatives.
type futureAlternativeSet struct {
	alternatives   []*futureAlternative
	minGenerations int
	autoLocs       []state.Loc
	testLoc        state.Loc
}

func newFutureAlternativeSet(ctx context.Context, live *liveRouteAvailabilityTester, route state.Route, loc state.Loc) *futureAlternativeSet {
	autoLocs := LocPredicate(func(ctx context.Context, l state.Loc) bool {
		return l.GetControlledAutomatically().GetActual(ctx) &&
			l.GetCurrentBlock().GetActual(ctx) != nil
	})
	fas := &futureAlternativeSet{
		minGenerations: live.Railway.GetBlockCount(ctx),
		testLoc:        loc,
		autoLocs:       autoLocs.GetLocs(ctx, live.Railway),
	}
	tester := NewFutureRouteAvailabilityTester(ctx, live.Railway)
	tester.TakeRoute(ctx, route, loc)
	fas.alternatives = append(fas.alternatives, &futureAlternative{tester, 0})
	return fas
}

func (fas *futureAlternativeSet) Add(alternative *futureAlternative) {
	fas.alternatives = append(fas.alternatives, alternative)
}

// Run the future generation test.
// Returns: True if there is a dead-lock free future or the test loc has reached a station.
func (fas *futureAlternativeSet) Test(ctx context.Context) bool {
	for len(fas.alternatives) > 0 {
		g := fas.alternatives[0]
		if g.generation >= fas.minGenerations {
			// We've found a dead-lock free alternative.
			return true
		}
		removed := false
		for (g.generation < fas.minGenerations) && (!removed) {
			stationReached, anyLocMoved := g.Increment(ctx, fas, fas.autoLocs, fas.testLoc)
			if stationReached {
				// We're done
				return true
			}
			if !anyLocMoved {
				// This generation has a deadlock.
				fas.alternatives = fas.alternatives[1:]
				removed = true
			}
		}
	}
	// Are there alternatives left?
	if len(fas.alternatives) == 0 {
		// Oops, deadlock
		return false
	}
	// There are deadlock free alternatives
	return true
}

// Represent a possible future state.
type futureAlternative struct {
	state      *futureRouteAvailabilityTester
	generation int
}

/// Increment the generation and move all locs.
// Returns: stationReached, atLeastOneLocHasMoved
func (fa *futureAlternative) Increment(ctx context.Context, genSet *futureAlternativeSet, locs []state.Loc, testLoc state.Loc) (bool, bool) {
	fa.generation++
	stationReached := false

	// Try to move the test loc first
	moved := fa.MoveLoc(ctx, genSet, testLoc)
	// Are we in a block that is considered a station for the loc?
	var block = fa.state.getCurrentBlock(testLoc)
	if (block != nil) && (isStationFor(ctx, block, testLoc)) {
		// Yes, the loc may stop here
		stationReached = true
		return stationReached, true
	}
	if moved {
		// We've moved so still no deadlock
		return stationReached, true
	}
	// Try one of the other locs
	for _, loc := range locs {
		if loc != testLoc {
			moved = fa.MoveLoc(ctx, genSet, loc)
			if moved {
				// Something moved
				return stationReached, true
			}
		}
	}
	return stationReached, false
}

// Try to move the given loc to a new block.
// Returns: True if the loc has moved, false if there are no routes available.
func (fa *futureAlternative) MoveLoc(ctx context.Context, genSet *futureAlternativeSet, loc state.Loc) bool {
	// Get possible routes
	currentBlock := fa.state.getCurrentBlock(loc)
	currentBlockEnterSide := fa.state.getCurrentBlockEnterSide(loc)
	locDirection := currentBlockEnterSide.Invert()
	avoidDirectionChanges := (loc.GetChangeDirection(ctx) == model.ChangeDirectionAvoid)
	routeFromBlock := getAllPossibleNonClosedRoutesFromBlock(currentBlock)
	routeOptions := routeFromBlock.And(func(ctx context.Context, r state.Route) bool {
		return fa.state.IsAvailableFor(ctx, r, loc, locDirection, avoidDirectionChanges).IsPossible
	})
	possibleRoutes := routeOptions.GetRoutes(ctx, fa.state.Railway)

	// If no routes, then return
	if len(possibleRoutes) == 0 {
		return false
	}
	if len(possibleRoutes) > 1 {
		// Create alternate generations
		for _, r := range possibleRoutes[1:] {
			alternateState := fa.state.Clone()
			alternateState.TakeRoute(ctx, r, loc)
			genSet.Add(&futureAlternative{alternateState, fa.generation})
		}
	}
	// Take the first route
	firstRoute := possibleRoutes[0]
	fa.state.TakeRoute(ctx, firstRoute, loc)
	return true
}
