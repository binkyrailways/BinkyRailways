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
	"math"
	"math/rand"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type defaultRouteSelector struct{}

var defaultRouteSelectorInstance state.RouteSelector = &defaultRouteSelector{}

type routeChance struct {
	Route  state.Route
	Chance int
}

type routeChances []routeChance

// Take a random choice between one of the options.
// Each option has a probability.
func (rcs routeChances) Gamble() state.Route {
	boundaries := make([]int, len(rcs))
	max := 0
	for i, rc := range rcs {
		max += rc.Chance + 1
		boundaries[i] = max
	}

	value := rand.Intn(max + 1)
	for i, rc := range rcs {
		if value <= boundaries[i] {
			return rc.Route
		}
	}
	// We should never get here
	return rcs[len(rcs)-1].Route
}

/// Select one of the given possible routes.
/// Returns null if no route should be taken.
/// <param name="possibleRoutes">A list of routes to choose from</param>
/// <param name="loc">The loc to choose for</param>
/// <param name="fromBlock">The block from which the next route will leave</param>
/// <param name="locDirection">The direction the loc is facing in the <see cref="fromBlock"/>.</param>
func (rs *defaultRouteSelector) SelectRoute(ctx context.Context, possibleRoutes []state.Route, loc state.Loc, fromBlock state.Block, locDirection model.BlockSide) state.Route {
	switch len(possibleRoutes) {
	case 0:
		// No available routes
		return nil
	case 1:
		// Only 1 possibility
		return possibleRoutes[0]
	}

	// Get all options with their choose probability
	options := make(routeChances, len(possibleRoutes))
	for i, pr := range possibleRoutes {
		options[i] = routeChance{
			Route:  pr,
			Chance: pr.GetChooseProbability(ctx),
		}
	}

	// Lower the probability for routes towards blocks that we've just visited.
	offset := float64(2.0)
	loc.ForEachRecentlyVisitedBlock(ctx, func(ctx context.Context, b state.Block) error {
		multiplier := float64(1.0) - (1.0 / offset)
		for i, opt := range options {
			if opt.Route.GetTo(ctx) == b {
				// Lower probability of this route
				options[i].Chance = int(math.Trunc(float64(opt.Chance) * multiplier))
			}
		}
		offset += 1
		return nil
	})

	// If there are options with probability 0, remove them, unless all options are like that.
	allZeroChance := true
	for _, opt := range options {
		if opt.Chance > 0 {
			allZeroChance = false
			break
		}
	}
	if !allZeroChance {
		// Remove all options with zero chance
		for i := 0; i < len(options); {
			if options[i].Chance == 0 {
				// Remove option
				options = append(options[:i], options[i+1:]...)
			} else {
				// Keep option
				i++
			}
		}
	}

	// Take a random route
	return options.Gamble()
}

/// Called when the loc has entered the given to-block of the current route.
func (rs *defaultRouteSelector) BlockEntered(ctx context.Context, loc state.Loc, block state.Block) {
	// Do nothing
}

/// Called when the loc has reached the given to-block of the current route.
func (rs *defaultRouteSelector) BlockReached(ctx context.Context, loc state.Loc, block state.Block) {
	// Do nothing
}
