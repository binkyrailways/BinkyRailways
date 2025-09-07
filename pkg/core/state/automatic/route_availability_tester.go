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

type RouteAvailabilityTester interface {
	// Can the given route be taken by the given loc?
	// route: The route being investigated
	// loc: The loc a route should be choosen for
	// locDirection: The direction the loc is facing in the From block of the given <see cref="route"/>.
	// avoidDirectionChanges: If true, the route is considered not available if a direction change is needed.
	// Returns: True if the route can be locked and no sensor in the route is active (outside current route).
	IsAvailableFor(ctx context.Context, route state.Route, loc state.Loc, locDirection model.BlockSide, avoidDirectionChanges bool) state.RouteOption

	// Can the given route be locked for the given loc?
	// Returns: lockedBy, canLock
	CanLock(ctx context.Context, route state.Route, loc state.Loc) (state.Loc, bool)

	// Is there are traffic in the opposite direction of the given to-block of a route?
	HasTrafficInOppositeDirection(ctx context.Context, toBlock state.Block, toBlockSide model.BlockSide, currentLoc state.Loc) bool

	// Is the critical section for the given route free for the given loc?
	IsCriticalSectionFree(ctx context.Context, route state.Route, loc state.Loc) bool

	// Is any of the sensors of the given route active?
	// Sensors that are also in the current route of the given loc are ignored.
	IsAnySensorActive(ctx context.Context, route state.Route, loc state.Loc) bool
}

// Base implementation of RouteAvailabilityTester
type baseRouteAvailabilityTester struct {
	RouteAvailabilityTester
	Railway state.Railway
}

var _ RouteAvailabilityTester = &baseRouteAvailabilityTester{}

// Can the given route be taken by the given loc?
// route: The route being investigated
// loc: The loc a route should be choosen for
// locDirection: The direction the loc is facing in the From block of the given <see cref="route"/>.
// avoidDirectionChanges: If true, the route is considered not available if a direction change is needed.
// Returns: True if the route can be locked and no sensor in the route is active (outside current route).
func (rt *baseRouteAvailabilityTester) IsAvailableFor(ctx context.Context, route state.Route, loc state.Loc, locDirection model.BlockSide, avoidDirectionChanges bool) state.RouteOption {
	// Can route be blocked?
	lockedBy, canLock := rt.RouteAvailabilityTester.CanLock(ctx, route, loc)
	if !canLock {
		// Cannot loc
		extra := ""
		if lockedBy != nil {
			extra = lockedBy.GetDescription()
		}
		return state.RouteOption{
			Route:  route,
			Reason: state.RouteImpossibleReasonLocked,
			Extra:  extra,
		}
	}

	// Is route closed?
	if route.GetClosed(ctx) {
		return state.RouteOption{
			Route:  route,
			Reason: state.RouteImpossibleReasonClosed,
		}
	}

	// Is target blocked closed?
	toBlock := route.GetTo()
	if toBlock.GetClosed().GetActual(ctx) || toBlock.GetClosed().GetRequested(ctx) {
		return state.RouteOption{
			Route:  route,
			Reason: state.RouteImpossibleReasonDestinationClosed,
		}
	}

	// Check opposite traffic
	maxSteps := rt.Railway.GetBlockCount()
	if blockContainingTraffic, hasTraffic := rt.hasTrafficInOppositeDirection(ctx, route, loc, maxSteps); hasTraffic {
		// Traffic in opposite direction found
		return state.RouteOption{
			Route:  route,
			Reason: state.RouteImpossibleReasonOpposingTraffic,
			Extra:  blockContainingTraffic.GetDescription(),
		}
	}

	// Check critical section of route
	if !rt.RouteAvailabilityTester.IsCriticalSectionFree(ctx, route, loc) {
		// Some route in critical section not free
		return state.RouteOption{
			Route:  route,
			Reason: state.RouteImpossibleReasonCriticalSectionOccupied,
		}
	}

	// Check direction
	if isDirectionChangeNeeded(ctx, route, locDirection) {
		if avoidDirectionChanges {
			// Do not take this route because a direction change would be needed.
			return state.RouteOption{
				Route:  route,
				Reason: state.RouteImpossibleReasonDirectionChangeNeeded,
			}
		}
		if loc.GetChangeDirection(ctx) != model.ChangeDirectionAllow {
			// Loc does not allow direction changes
			if !route.GetFrom().GetIsDeadEnd(ctx) {
				return state.RouteOption{
					Route:  route,
					Reason: state.RouteImpossibleReasonDirectionChangeNeeded,
				}
			}
			// Loc will reverse out of a dead end
		}
		if route.GetFrom().GetChangeDirection(ctx) != model.ChangeDirectionAllow {
			// From block does not allowed direction changes
			return state.RouteOption{
				Route:  route,
				Reason: state.RouteImpossibleReasonDirectionChangeNeeded,
			}
		}
	}

	// Check permissions
	if !route.GetPermissions(ctx).Evaluate(ctx, loc) {
		// Loc not allowed by permissions
		return state.RouteOption{
			Route:  route,
			Reason: state.RouteImpossibleReasonNoPermission,
		}
	}

	// Check sensor states
	if rt.RouteAvailabilityTester.IsAnySensorActive(ctx, route, loc) {
		// Route is not available
		return state.RouteOption{
			Route:  route,
			Reason: state.RouteImpossibleReasonSensorActive,
		}
	}

	// Route is available
	return state.RouteOption{
		Route:      route,
		IsPossible: true,
		Reason:     state.RouteImpossibleReasonNone,
	}
}

// Can the given route be locked for the given loc?
// Returns: lockedBy, canLock
func (rt *baseRouteAvailabilityTester) CanLock(ctx context.Context, route state.Route, loc state.Loc) (state.Loc, bool) {
	return route.CanLock(ctx, loc)
}

// Is there are traffic in the opposite direction of the given to-block of a route?
func (rt *baseRouteAvailabilityTester) HasTrafficInOppositeDirection(ctx context.Context, toBlock state.Block, toBlockSide model.BlockSide, currentLoc state.Loc) bool {
	loc := toBlock.GetLockedBy(ctx)
	if (loc != nil) && (loc != currentLoc) {
		// Check current route
		locRoute := loc.GetCurrentRoute().GetActual(ctx)
		if (locRoute != nil) && (locRoute.GetRoute().GetTo() == toBlock) {
			locEnterSide := loc.GetCurrentBlockEnterSide().GetActual(ctx)
			if locEnterSide != toBlockSide {
				// We found opposite traffic
				if !canChangeDirectionIn(ctx, loc, toBlock) {
					// The loc cannot change direction in to block, so there is absolutely opposite traffic.
					return true
				}
			}
		}
		// Check next route
		nextRoute := loc.GetNextRoute().GetActual(ctx)
		if (nextRoute != nil) && (nextRoute.GetTo() == toBlock) {
			locEnterSide := loc.GetCurrentBlockEnterSide().GetActual(ctx)
			if locEnterSide != toBlockSide {
				// We found opposite traffic
				if !canChangeDirectionIn(ctx, loc, toBlock) {
					// The loc cannot change direction in to block, so there is absolutely opposite traffic.
					return true
				}
			}
		}
	}
	return false

}

// Is the critical section for the given route free for the given loc?
func (rt *baseRouteAvailabilityTester) IsCriticalSectionFree(ctx context.Context, route state.Route, loc state.Loc) bool {
	return route.GetCriticalSection(ctx).AllFree(ctx, loc)
}

// Is any of the sensors of the given route active?
// Sensors that are also in the current route of the given loc are ignored.
func (rt *baseRouteAvailabilityTester) IsAnySensorActive(ctx context.Context, route state.Route, loc state.Loc) bool {
	isActive := SensorPredicate(func(ctx context.Context, sensor state.Sensor) bool {
		return sensor.GetActive().GetActual(ctx)
	})
	// Is current block is set, exclude all sensors that belong to this block
	if currentBlock := loc.GetCurrentBlock().GetActual(ctx); currentBlock != nil {
		currentBlockID := currentBlock.GetID()
		isNotPartOfBlock := SensorPredicate(func(ctx context.Context, s state.Sensor) bool {
			sensorBlock := s.GetModel().GetBlock()
			return sensorBlock == nil || sensorBlock.GetID() != currentBlockID
		})
		isActive = isActive.And(isNotPartOfBlock)
	}
	currentRoute := loc.GetCurrentRoute().GetActual(ctx)
	if currentRoute == nil {
		// There must be no active sensor
		return isActive.Any(ctx, route)
	}

	// The loc has a current route.
	// There must not be any active sensor that is not listed in the current route.
	notContained := SensorPredicate(func(ctx context.Context, s state.Sensor) bool {
		return !currentRoute.GetRoute().ContainsSensor(ctx, s)
	})
	return isActive.And(notContained).Any(ctx, route)
	//	return activeSensors.Any(x => !currentRoute.Route.Contains(x));
}

// Is there are traffic in the opposite direction of the given route (not including the given loc).
func (rt *baseRouteAvailabilityTester) hasTrafficInOppositeDirection(ctx context.Context, route state.Route, currentLoc state.Loc, stepsRemaining int) (state.Block, bool) {
	if stepsRemaining <= 0 {
		// We've taken to long to find opposing traffic
		return nil, false
	}
	toBlock := route.GetTo()
	if rt.RouteAvailabilityTester.HasTrafficInOppositeDirection(ctx, toBlock, route.GetToBlockSide(), currentLoc) {
		return toBlock, true
	}

	// Check next routes
	nextRoutes := getAllPossibleNonClosedRoutesFromBlock(toBlock).GetRoutes(ctx, rt.Railway)
	//var nextRoutes = railwayState.GetAllPossibleNonClosedRoutesFromBlock(toBlock, locDirection).ToList();

	if len(nextRoutes) == 0 {
		// No next routes at all, we do no longer care about opposing traffic
		return nil, false
	}
	if len(nextRoutes) > 1 {
		// Multiple next routes, we stop now
		return nil, false
	}
	// Only one route possible, check that for opposing traffic
	return rt.hasTrafficInOppositeDirection(ctx, nextRoutes[0], currentLoc, stepsRemaining-1)
}
