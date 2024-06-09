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

package automatic

import (
	"context"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// Choose an available route from the given block.
// Param: loc The loc a route should be choosen for
// Param: fromBlock The starting block of the route
// Param: locDirection The direction the loc is facing in the <see cref="fromBlock"/>
// Param: avoidDirectionChanges If true, routes requiring a direction change will not be considered, unless there is no alternative.
// Returns: Nil if not route is available.
func (alc *automaticLocController) chooseRoute(ctx context.Context, loc state.Loc, currentRoute state.Route, fromBlock state.Block, locDirection model.BlockSide, avoidDirectionChanges bool) state.Route {
	// Gather all non-closed routes from given from-block
	routeFromFromBlock := getAllPossibleNonClosedRoutesFromBlock(fromBlock)
	// Filter out routes that are currently not available (for any reason)
	var routeOptions []state.RouteOption
	possibleRouteOptions := routeFromFromBlock.And(func(ctx context.Context, r state.Route) bool {
		ro := alc.routeAvailabilityTester.IsAvailableFor(ctx, r, loc, locDirection, avoidDirectionChanges)
		alc.log.Debug().
			Str("loc", loc.GetDescription()).
			Str("route", r.GetDescription()).
			Bool("available", ro.IsPossible).
			Str("reason", ro.GetReasonDescription()).
			Msg("Route availability result")
		routeOptions = append(routeOptions, ro)
		return ro.IsPossible
	})
	// Filter out routes that have a conflicting output against the current route
	locState := loc.GetAutomaticState().GetActual(ctx)
	routeOptionsWithoutConflictingOutputs := possibleRouteOptions.And(func(ctx context.Context, r state.Route) bool {
		if currentRoute == nil {
			// No conflict because there is no current route
			return true
		}
		if locState == state.AssignRoute {
			// Loc is waiting to be assigned a route.
			// We do not yet care about conflicting outputs
			return true
		}
		if locState >= state.EnterSensorActivated {
			// Loc is already reached destination (or further).
			// We no longer care about conflicting outputs
			return true
		}
		hasConflictingOutput := false
		r.GetModel().GetOutputs().ForEach(func(ows model.OutputWithState) {
			if !hasConflictingOutput {
				if currentRoute.HasConflictingOutput(ctx, ows) {
					hasConflictingOutput = true
				}
			}
		})
		return !hasConflictingOutput
	})
	possibleRoutes := routeOptionsWithoutConflictingOutputs.GetRoutes(ctx, alc.railway)
	loc.GetLastRouteOptions().SetActual(ctx, routeOptions)

	if len(possibleRoutes) > 0 {
		// Use the route selector to choose a next route
		selector := loc.GetRouteSelector(ctx)
		selected := selector.SelectRoute(ctx, possibleRoutes, loc, fromBlock, locDirection)
		if selected == nil {
			alc.log.Info().
				Str("loc", loc.GetDescription()).
				Str("from", fromBlock.GetDescription()).
				Str("fromSide", locDirection.String()).
				Interface("availableRoutes", possibleRoutes).
				Msg("No route selected for loc")
		} else {
			alc.log.Info().
				Str("loc", loc.GetDescription()).
				Str("selected", selected.GetDescription()).
				Str("from", fromBlock.GetDescription()).
				Str("fromSide", locDirection.String()).
				Interface("availableRoutes", possibleRoutes).
				Msg("Selected route for loc")
		}
		return selected
	}

	alc.log.Info().
		Str("loc", loc.GetDescription()).
		Str("from", fromBlock.GetDescription()).
		Str("fromSide", locDirection.String()).
		Msg("No possible routes for loc")

	// No available routes
	return nil
}

// Try to choose a next route for the given loc, unless the target block
// of the current route is set to wait.
// Returns: True if a next route has been chosen.
func (alc *automaticLocController) chooseNextRoute(ctx context.Context, loc state.Loc) bool {
	// Should we wait in the destination block?
	route := loc.GetCurrentRoute().GetActual(ctx)
	if (route == nil) ||
		(loc.GetWaitAfterCurrentRoute().GetActual(ctx)) ||
		(state.HasNextRoute(ctx, loc)) ||
		!alc.shouldControlAutomatically(ctx, loc) {
		// No need to choose a next route
		return false
	}

	// The loc can continue (if a free route is found)
	nextRoute := alc.chooseRoute(ctx, loc, route.GetRoute(), route.GetRoute().GetTo(), route.GetRoute().GetToBlockSide().Invert(), true)
	if nextRoute == nil {
		// No route available
		return false
	}

	// We have a next route
	nextRoute.Lock(ctx, loc)
	loc.GetNextRoute().SetActual(ctx, nextRoute)

	// Set all junctions & outputs correct
	nextRoute.Prepare(ctx)
	return true
}
