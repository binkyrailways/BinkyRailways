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
	"math"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Loc specifies the state of a single loc
type Loc interface {
	Entity

	// Gets the model of this loc
	GetModel() model.Loc

	// All settings of this loc will be reset, because the loc is taken of the track.
	SubscribeBeforeReset(func(context.Context))

	// All settings of this loc have been reset, because the loc is taken of the track.
	SubscribeAfterReset(func(context.Context))

	// Address of the entity
	GetAddress(context.Context) model.Address

	// Percentage of speed steps for the slowest speed of this loc.
	// Value between 1 and 100.
	GetSlowSpeed(context.Context) int

	// Percentage of speed steps for the medium speed of this loc.
	// Value between 1 and 100.
	GetMediumSpeed(context.Context) int

	// Percentage of speed steps for the maximum speed of this loc.
	// Value between 1 and 100.
	GetMaximumSpeed(context.Context) int

	// Is this loc controlled by the automatic loc controller?
	GetControlledAutomatically() BoolProperty

	// Gets the number of speed steps supported by this loc.
	GetSpeedSteps(context.Context) int

	// Gets/sets the image of the given loc.
	// <value>Null if there is no image.</value>
	// <remarks>Image must be png, bmp, gif, jpg, wmf or emf</remarks>
	//Stream Image { get; }

	// Gets the name of the given function.
	// GetFunctionName(LocFunction function, out bool isCustom) string;

	// Is it allowed for this loc to change direction?
	GetChangeDirection(context.Context) model.ChangeDirection

	// Gets the name of the person that owns this loc.
	GetOwner(context.Context) string

	// Is it allowed to set the ControlledAutomatically property to true?
	GetCanSetAutomaticControl(context.Context) bool

	// The current state of this loc in the automatic loc controller.
	GetAutomaticState() ActualAutoLocStateProperty

	// Gets the route that this loc is currently taking.
	// Do not assign this property directly, instead use the assign methods.
	GetCurrentRoute() ActualRouteForLocProperty

	// Should the loc wait when the current route has finished?
	GetWaitAfterCurrentRoute() ActualBoolProperty

	// Time when this loc will exceed the maximum duration of the current route.
	GetDurationExceedsCurrentRouteTime() ActualTimeProperty

	// Is the maximum duration of the current route this loc is taken exceeded?
	GetIsCurrentRouteDurationExceeded(context.Context) bool

	// Gets the route that this loc will take when the current route has finished.
	// This property is only set by the automatic loc controller.
	GetNextRoute() ActualRouteProperty

	// Gets the block that the loc is currently in.
	GetCurrentBlock() ActualBlockProperty

	// Gets the side at which the current block was entered.
	GetCurrentBlockEnterSide() ActualBlockSideProperty

	// Time when this loc will start it's next route.
	GetStartNextRouteTime() ActualTimeProperty

	// Route options as considered last by the automatic train controller.
	//IActualStateProperty<IRouteOption[]> LastRouteOptions { get; }

	// Gets/sets a selector used to select the next route from a list of possible routes.
	// If no route selector is set, a default will be created.
	GetRouteSelector(context.Context) RouteSelector
	SetRouteSelector(context.Context, RouteSelector) error

	// Current speed of this loc as percentage of the speed steps of the loc.
	// Value between 0 and 100.
	// Setting this value will result in a request to its command station to alter the speed.
	GetSpeed() IntProperty

	// Gets a human readable representation of the speed of the loc.
	GetSpeedText(context.Context) string

	// Gets a human readable representation of the state of the loc.
	GetStateText(context.Context) string

	// Gets the actual speed of the loc in speed steps
	// Value between 0 and the maximum number of speed steps supported by this loc.
	// Setting this value will result in a request to its command station to alter the speed.
	GetSpeedInSteps() IntProperty

	// Current direction of this loc.
	// Setting this value will result in a request to its command station to alter the direction.
	GetDirection() LocDirectionProperty

	// Is this loc reversing out of a dead end?
	// This can only be true for locs that are not allowed to change direction.
	GetReversing() ActualBoolProperty

	// Directional lighting of the loc.
	// Setting this value will result in a request to its command station to alter the value.
	GetF0() BoolProperty

	// Return the state of a function.
	// <returns>True if such a state exists, false otherwise</returns>
	//bool TryGetFunctionState(LocFunction function, out IStateProperty<bool> state);

	// Return all functions that have state.
	//IEnumerable<LocFunction> Functions { get; }

	// Try to assign the given loc to the given block.
	// Assigning is only possible when the loc is not controlled automatically and
	// the block can be assigned by the given loc.
	// If the loc is already assigned to another block, this assignment is removed
	// and the block on that block is unlocked.
	// <param name="block">The new block to assign to. If null, the loc will only be unassigned from the current block.</param>
	// <param name="currentBlockEnterSide">The site to which the block is entered (invert of facing)</param>
	// <returns>Nil on success, error otherwise</returns>
	AssignTo(ctx context.Context, block Block, currentBlockEnterSide model.BlockSide) error

	// Gets command station specific (advanced) info for this loc.
	GetCommandStationInfo(context.Context) string

	// Forcefully reset of settings of this loc.
	// This should be used when a loc is taken of the track.
	Reset(context.Context)

	// Save the current state to the state persistence.
	PersistState(context.Context)

	// Gets zero or more blocks that were recently visited by this loc.
	// The first block was last visited.
	ForEachRecentlyVisitedBlock(context.Context, func(Block))

	// Behavior of the last event triggered by this loc.
	GetLastEventBehavior(ctx context.Context) RouteEventBehavior
	SetLastEventBehavior(ctx context.Context, value RouteEventBehavior) error

	// Is the speed behavior of the last event set to default?
	GetIsLastEventBehaviorSpeedDefault(context.Context) bool
}

// Has the given loc a next route configured?
func HasNextRoute(ctx context.Context, loc Loc) bool {
	return (loc.GetNextRoute().GetActual(ctx) != nil)
}

// GetMaximumSpeedForRoute returns the maximum speed of the given loc on the given route.
func GetMaximumSpeedForRoute(ctx context.Context, loc Loc, route Route) int {
	var speed int
	if loc.GetReversing().GetActual(ctx) {
		speed = loc.GetMediumSpeed(ctx)
	} else {
		speed = loc.GetMaximumSpeed(ctx)
	}
	routeSpeed := float64(route.GetSpeed(ctx)) / 100.0
	return int(math.Max(float64(speed)*routeSpeed, float64(loc.GetSlowSpeed(ctx))))
}

// GetMediumSpeedForRoute returns the medium speed of the given loc on the given route.
func GetMediumSpeedForRoute(ctx context.Context, loc Loc, route Route) int {
	speed := loc.GetMediumSpeed(ctx)
	routeSpeed := float64(route.GetSpeed(ctx)) / 100.0
	return int(math.Max(float64(speed)*routeSpeed, float64(loc.GetSlowSpeed(ctx))))
}
