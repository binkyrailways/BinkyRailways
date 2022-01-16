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

package impl

import (
	"context"
	"fmt"
	"strings"
	"time"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// Loc adds implementation functions to state.Loc.
type Loc interface {
	Entity
	state.Loc

	// Add a state object to the list of entities locked by this locomotive.
	AddLockedEntity(context.Context, Lockable)
	// Remove a state object from the list of entities locked by this locomotive.
	RemoveLockedEntity(context.Context, Lockable)
}

type loc struct {
	entity

	commandStation                  CommandStation
	waitAfterCurrentRoute           actualBoolProperty
	durationExceedsCurrentRouteTime actualTimeProperty
	autoLocState                    actualAutoLocStateProperty
	nextRoute                       actualRouteProperty
	currentBlock                    actualBlockProperty
	currentBlockEnterSide           actualBlockSideProperty
	startNextRouteTime              actualTimeProperty
	speed                           speedProperty
	speedInSteps                    intProperty
	direction                       locDirectionProperty
	reversing                       actualBoolProperty
	f0                              boolProperty
	controlledAutomatically         boolProperty
	currentRoute                    actualRouteForLocProperty
	lockedEntities                  []Lockable
	lastEventBehavior               state.RouteEventBehavior
	routeSelector                   state.RouteSelector
}

// Create a new entity
func newLoc(en model.Loc, railway Railway) Loc {
	l := &loc{
		entity: newEntity(en, railway),
	}
	l.waitAfterCurrentRoute.Configure(l, railway, railway)
	l.durationExceedsCurrentRouteTime.Configure(l, railway, railway)
	l.autoLocState.Configure(l, railway, railway)
	l.nextRoute.Configure(l, railway, railway)
	l.currentBlock.Configure(l, railway, railway)
	l.currentBlockEnterSide.Configure(l, railway, railway)
	l.startNextRouteTime.Configure(l, railway, railway)
	l.speed.loc = l
	l.speedInSteps.Configure(l, railway, railway)
	l.speedInSteps.OnRequestedChanged = func(ctx context.Context, value int) {
		if l.commandStation != nil {
			l.commandStation.SendLocSpeedAndDirection(ctx, l)
		}
	}
	l.direction.Configure(l, railway, railway)
	l.direction.OnRequestedChanged = func(ctx context.Context, value state.LocDirection) {
		if l.commandStation != nil {
			l.commandStation.SendLocSpeedAndDirection(ctx, l)
		}
	}
	l.reversing.Configure(l, railway, railway)
	l.f0.Configure(l, railway, railway)
	l.f0.OnRequestedChanged = func(ctx context.Context, value bool) {
		if l.commandStation != nil {
			l.commandStation.SendLocSpeedAndDirection(ctx, l)
		}
	}
	l.controlledAutomatically.Configure(l, railway, railway)
	l.currentRoute.Configure(l, railway, railway)
	return l
}

// getRoute returns the entity as Route.
func (l *loc) getLoc() model.Loc {
	return l.GetEntity().(model.Loc)
}

// Gets the model of this loc
func (l *loc) GetModel() model.Loc {
	return l.getLoc()
}

// Try to prepare the entity for use.
// Returns nil when the entity is successfully prepared,
// returns an error otherwise.
func (l *loc) TryPrepareForUse(ctx context.Context, _ state.UserInterface, _ state.Persistence) error {
	if l.GetAddress(ctx).IsEmpty() {
		return nil
	}
	var err error
	l.commandStation, err = l.GetRailwayImpl().SelectCommandStation(ctx, l.getLoc())
	if err != nil {
		return err
	}
	if l.commandStation == nil {
		return fmt.Errorf("Loc does not have a commandstation attached.")
	}
	l.commandStation.RegisterLoc(l)
	return nil
}

// All settings of this loc will be reset, because the loc is taken of the track.
//BeforeReset() model.EventHandler

// All settings of this loc have been reset, because the loc is taken of the track.
//AfterReset() model.EventHandler

// Address of the entity
func (l *loc) GetAddress(context.Context) model.Address {
	return l.getLoc().GetAddress()
}

// Percentage of speed steps for the slowest speed of this loc.
// Value between 1 and 100.
func (l *loc) GetSlowSpeed(context.Context) int {
	return l.getLoc().GetSlowSpeed()
}

// Percentage of speed steps for the medium speed of this loc.
// Value between 1 and 100.
func (l *loc) GetMediumSpeed(context.Context) int {
	return l.getLoc().GetMediumSpeed()
}

// Percentage of speed steps for the maximum speed of this loc.
// Value between 1 and 100.
func (l *loc) GetMaximumSpeed(context.Context) int {
	return l.getLoc().GetMaximumSpeed()
}

// Is this loc controlled by the automatic loc controller?
func (l *loc) GetControlledAutomatically() state.BoolProperty {
	return &l.controlledAutomatically
}

// Gets the number of speed steps supported by this loc.
func (l *loc) GetSpeedSteps(context.Context) int {
	return l.getLoc().GetSpeedSteps()
}

// Gets/sets the image of the given loc.
// <value>Null if there is no image.</value>
// <remarks>Image must be png, bmp, gif, jpg, wmf or emf</remarks>
//Stream Image { get; }

// Gets the name of the given function.
// GetFunctionName(LocFunction function, out bool isCustom) string;

// Is it allowed for this loc to change direction?
func (l *loc) GetChangeDirection(context.Context) model.ChangeDirection {
	return l.getLoc().GetChangeDirection()
}

// Gets the name of the person that owns this loc.
func (l *loc) GetOwner(context.Context) string {
	return l.getLoc().GetOwner()
}

// Is it allowed to set the ControlledAutomatically property to true?
func (l *loc) GetCanSetAutomaticControl(ctx context.Context) bool {
	// Current block must be set
	return l.GetCurrentBlock().GetActual(ctx) != nil
}

// The current state of this loc in the automatic loc controller.
func (l *loc) GetAutomaticState() state.ActualAutoLocStateProperty {
	return &l.autoLocState
}

// Gets the route that this loc is currently taking.
// Do not assign this property directly, instead use the assign methods.
func (l *loc) GetCurrentRoute() state.ActualRouteForLocProperty {
	return &l.currentRoute
}

// Should the loc wait when the current route has finished?
func (l *loc) GetWaitAfterCurrentRoute() state.ActualBoolProperty {
	return &l.waitAfterCurrentRoute
}

// Time when this loc will exceed the maximum duration of the current route.
func (l *loc) GetDurationExceedsCurrentRouteTime() state.ActualTimeProperty {
	return &l.durationExceedsCurrentRouteTime
}

// Is the maximum duration of the current route this loc is taken exceeded?
func (l *loc) GetIsCurrentRouteDurationExceeded(ctx context.Context) bool {
	return time.Now().After(l.GetDurationExceedsCurrentRouteTime().GetActual(ctx))
}

// Gets the route that this loc will take when the current route has finished.
// This property is only set by the automatic loc controller.
func (l *loc) GetNextRoute() state.ActualRouteProperty {
	return &l.nextRoute
}

// Gets the block that the loc is currently in.
func (l *loc) GetCurrentBlock() state.ActualBlockProperty {
	return &l.currentBlock
}

// Gets the side at which the current block was entered.
func (l *loc) GetCurrentBlockEnterSide() state.ActualBlockSideProperty {
	return &l.currentBlockEnterSide
}

// Time when this loc will start it's next route.
func (l *loc) GetStartNextRouteTime() state.ActualTimeProperty {
	return &l.startNextRouteTime
}

// Route options as considered last by the automatic train controller.
//IActualStateProperty<IRouteOption[]> LastRouteOptions { get; }

// Gets/sets a selector used to select the next route from a list of possible routes.
// If no route selector is set, a default will be created.
func (l *loc) GetRouteSelector(ctx context.Context) state.RouteSelector {
	if l.routeSelector != nil {
		return l.routeSelector
	}
	return defaultRouteSelectorInstance
}
func (l *loc) SetRouteSelector(ctx context.Context, selector state.RouteSelector) error {
	l.routeSelector = selector
	return nil
}

// Current speed of this loc as percentage of the speed steps of the loc.
// Value between 0 and 100.
// Setting this value will result in a request to its command station to alter the speed.
func (l *loc) GetSpeed() state.IntProperty {
	return &l.speed
}

// Gets a human readable representation of the speed of the loc.
func (l *loc) GetSpeedText(ctx context.Context) string {
	speed := l.GetSpeed().GetActual(ctx)
	direction := l.GetDirection().GetActual(ctx)
	return fmt.Sprintf("%d %s", speed, strings.ToLower(direction.String()))
}

// Gets a human readable representation of the state of the loc.
func (l *loc) GetStateText(ctx context.Context) string {
	return "" // TODO
}

// Gets the actual speed of the loc in speed steps
// Value between 0 and the maximum number of speed steps supported by this loc.
// Setting this value will result in a request to its command station to alter the speed.
func (l *loc) GetSpeedInSteps() state.IntProperty {
	return &l.speedInSteps
}

// Current direction of this loc.
// Setting this value will result in a request to its command station to alter the direction.
func (l *loc) GetDirection() state.LocDirectionProperty {
	return &l.direction
}

// Is this loc reversing out of a dead end?
// This can only be true for locs that are not allowed to change direction.
func (l *loc) GetReversing() state.ActualBoolProperty {
	return &l.reversing
}

// Directional lighting of the loc.
// Setting this value will result in a request to its command station to alter the value.
func (l *loc) GetF0() state.BoolProperty {
	return &l.f0
}

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
func (l *loc) AssignTo(ctx context.Context, block state.Block, currentBlockEnterSide model.BlockSide) error {
	ca := l.GetControlledAutomatically()
	if ca.GetActual(ctx) {
		return fmt.Errorf("Loc already controlled automatically")
	}
	if ca.GetRequested(ctx) {
		return fmt.Errorf("Loc about to be controlled automatically")
	}

	// No change?
	current := l.GetCurrentBlock().GetActual(ctx)
	if current == block {
		// Already assigned to requested block
		return nil
	}

	// Try to lock new block
	if block != nil {
		if _, canLock := block.CanLock(ctx, l); !canLock {
			return fmt.Errorf("Cannot lock block %s", block.GetDescription())
		}
	}

	// Unassign from current block
	if current != nil {
		if err := current.ValidateLockedBy(ctx, l); err != nil {
			return err
		}
		current.Unlock(ctx, nil)
	}
	l.GetCurrentBlock().SetActual(ctx, nil)

	// Now assign to new block (if any)
	if block != nil {
		block.Lock(ctx, l)
		l.GetCurrentBlockEnterSide().SetActual(ctx, currentBlockEnterSide)
		l.GetCurrentBlock().SetActual(ctx, block)
	} else {
		// Unassign -> unlock all
		l.UnlockAll(ctx)
	}

	return nil
}

// Gets command station specific (advanced) info for this loc.
func (l *loc) GetCommandStationInfo(ctx context.Context) string {
	return "" // TODO
}

// Forcefully reset of settings of this loc.
// This should be used when a loc is taken of the track.
func (l *loc) Reset(ctx context.Context) {
	// TODO
}

// Save the current state to the state persistence.
func (l *loc) PersistState(ctx context.Context) {
	// TODO
}

// Gets zero or more blocks that were recently visited by this loc.
// The first block was last visited.
func (l *loc) ForEachRecentlyVisitedBlock(ctx context.Context, cb func(state.Block)) {
	// TODO
}

// Get behavior of the last event triggered by this loc.
func (l *loc) GetLastEventBehavior(ctx context.Context) state.RouteEventBehavior {
	var result state.RouteEventBehavior
	l.GetRailway().Exclusive(ctx, func(ctx context.Context) error {
		result = l.lastEventBehavior
		return nil
	})
	return result
}

// Set behavior of the last event triggered by this loc.
func (l *loc) SetLastEventBehavior(ctx context.Context, value state.RouteEventBehavior) error {
	return l.GetRailway().Exclusive(ctx, func(ctx context.Context) error {
		l.lastEventBehavior = value
		return nil
	})
}

// Is the speed behavior of the last event set to default?
func (l *loc) GetIsLastEventBehaviorSpeedDefault(ctx context.Context) bool {
	b := l.GetLastEventBehavior(ctx)
	if b == nil {
		return false
	}
	return b.GetSpeedBehavior() == model.LocSpeedBehaviorDefault
}

// Add a state object to the list of entities locked by this locomotive.
func (l *loc) AddLockedEntity(ctx context.Context, x Lockable) {
	l.lockedEntities = append(l.lockedEntities, x)
}

// Remove a state object from the list of entities locked by this locomotive.
func (l *loc) RemoveLockedEntity(ctx context.Context, x Lockable) {
	for i, e := range l.lockedEntities {
		if e == x {
			// Remove entry
			l.lockedEntities = append(l.lockedEntities[:i], l.lockedEntities[i+1:]...)
		}
	}
}

// Unlock all entities locked by me
func (l *loc) UnlockAll(ctx context.Context) {
	for len(l.lockedEntities) > 0 {
		l.lockedEntities[0].Unlock(ctx, nil)
	}
}
