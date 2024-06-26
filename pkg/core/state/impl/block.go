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

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"go.uber.org/multierr"
)

// Block adds implementation functions to state.Block.
type Block interface {
	Entity
	state.Block
}

type block struct {
	entity
	lockable

	closed          boolProperty
	waitPermissions state.LocPredicate
	junctions       []Junction
	blockGroup      BlockGroup
	deadEnd         bool
}

// Create a new entity
func newBlock(en model.Block, railway Railway) Block {
	b := &block{
		entity: newEntity(railway.Logger().With().Str("block", en.GetDescription()).Logger(), en, railway),
	}
	b.lockable = newLockable(railway, b, func(ctx context.Context, cb func(state.Lockable) error) error {
		// iterate children
		for _, x := range b.junctions {
			cb(x)
		}
		return nil
	})
	b.SubscribeLockedByChanges(func(ctx context.Context, owner state.Loc) {
		if owner == nil {
			// Unlocked
			b.updateClosed(ctx)
		}
	})
	b.closed.Configure("closed", b, nil, railway, railway)
	b.closed.SubscribeRequestChanges(func(ctx context.Context, value bool) {
		b.updateClosed(ctx)
	})
	var err error
	b.waitPermissions, err = newLocPredicate(railway, en.GetWaitPermissions())
	if err != nil {
		panic(err)
	}
	return b
}

// getBlock returns the entity as Block.
func (b *block) getBlock() model.Block {
	return b.GetEntity().(model.Block)
}

// Is this equal to other?
func (b *block) Equals(other state.Block) bool {
	return b == other
}

// Try to prepare the entity for use.
// Returns nil when the entity is successfully prepared,
// returns an error otherwise.
func (b *block) TryPrepareForUse(ctx context.Context, ui state.UserInterface, _ state.Persistence) error {
	// TODO
	/*
			            // Load state
		            this.statePersistence = statePersistence;
		            bool closed;
		            if (statePersistence.TryGetBlockState(RailwayState, this, out closed))
		            {
		                Closed.Requested = closed;
		            }
	*/
	// Connect junctions
	bModel := b.getBlock()
	m := bModel.GetModule()
	var merr error
	m.GetJunctions().ForEach(func(j model.Junction) {
		if j.GetBlock() == bModel {
			if jState, err := b.railway.ResolveJunction(ctx, j); err != nil {
				multierr.AppendInto(&merr, err)
			} else {
				b.junctions = append(b.junctions, jState)
			}
		}
	})
	if merr != nil {
		return merr
	}
	// Connect block group
	if bgModel := bModel.GetBlockGroup(); bgModel != nil {
		if bgState, err := b.railway.ResolveBlockGroup(ctx, bgModel); err != nil {
			return err
		} else {
			b.blockGroup = bgState
		}
	}
	return nil
}

// Wrap up the preparation fase.
func (b *block) FinalizePrepare(ctx context.Context) {
	hasRoutesToBack := state.RoutePredicate(func(ctx context.Context, r state.Route) bool {
		return r.GetToBlockSide() == model.BlockSideBack && r.GetTo() == b
	}).AnyRoutes(ctx, b.railway)
	hasRoutesToFront := state.RoutePredicate(func(ctx context.Context, r state.Route) bool {
		return r.GetToBlockSide() == model.BlockSideFront && r.GetTo() == b
	}).AnyRoutes(ctx, b.railway)
	hasRoutesFromBack := state.RoutePredicate(func(ctx context.Context, r state.Route) bool {
		return r.GetFromBlockSide() == model.BlockSideBack && r.GetFrom() == b
	}).AnyRoutes(ctx, b.railway)
	hasRoutesFromFront := state.RoutePredicate(func(ctx context.Context, r state.Route) bool {
		return r.GetFromBlockSide() == model.BlockSideFront && r.GetFrom() == b
	}).AnyRoutes(ctx, b.railway)

	b.deadEnd = (hasRoutesToBack && !hasRoutesFromFront) ||
		(hasRoutesToFront && !hasRoutesFromBack)

	b.log.Debug().Bool("deadend", b.deadEnd).Msg("Block fully prepared")
}

// Update the actual closed status
func (b *block) updateClosed(ctx context.Context) {
	if !b.closed.IsConsistent(ctx) {
		if !b.IsLocked(ctx) {
			b.closed.SetActual(ctx, b.closed.GetRequested(ctx))
		}
	}
}

// Unique ID of the module containing this entity
func (b *block) GetModuleID() string {
	return b.getBlock().GetModule().GetID()
}

// Gets the underlying model
func (b *block) GetModel() model.Block {
	return b.getBlock()
}

// Probability (in percentage) that a loc that is allowed to wait in this block
// will actually wait.
// When set to 0, no locs will wait (unless there is no route available).
// When set to 100, all locs (that are allowed) will wait.
func (b *block) GetWaitProbability(context.Context) int {
	return b.getBlock().GetWaitProbability()
}

// Minimum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
func (b *block) GetMinimumWaitTime(context.Context) int {
	return b.getBlock().GetMinimumWaitTime()
}

// Maximum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
func (b *block) GetMaximumWaitTime(context.Context) int {
	return b.getBlock().GetMaximumWaitTime()
}

// Gets the predicate used to decide which locs are allowed to wait in this block.
func (b *block) GetWaitPermissions() state.LocPredicate {
	return b.waitPermissions
}

// By default the front of the block is on the right of the block.
// When this property is set, that is reversed to the left of the block.
// Setting this property will only alter the display behavior of the block.
func (b *block) GetReverseSides(context.Context) bool {
	return b.getBlock().GetReverseSides()
}

// Is it allowed for locs to change direction in this block?
func (b *block) GetChangeDirection(context.Context) model.ChangeDirection {
	if b.deadEnd {
		return model.ChangeDirectionAllow
	}
	return b.getBlock().GetChangeDirection()
}

// Must reversing locs change direction (back to normal) in this block?
func (b *block) GetChangeDirectionReversingLocs(context.Context) bool {
	return b.getBlock().GetChangeDirectionReversingLocs()
}

// Gets all sensors that are either an "entering" or a "reached" sensor for a route
// that leads to this block.
func (b *block) ForEachSensor(ctx context.Context, cb func(state.Sensor)) {

}

// Gets the current state of this block
func (b *block) GetState(ctx context.Context) state.BlockState {
	loc := b.GetLockedBy(ctx)
	if loc != nil {
		// Locked
		if loc.GetCurrentBlock().GetActual(ctx) == b {
			// Block is in use
			return state.BlockStateOccupied
		}
		currentRoute := loc.GetCurrentRoute().GetActual(ctx)
		if (currentRoute != nil) && (currentRoute.GetRoute().GetTo() == b) {
			if loc.GetAutomaticState().GetActual(ctx) == state.EnteringDestination {
				// Loc is entering this block
				return state.BlockStateEntering
			}
			// Loc is on route to this block
			return state.BlockStateDestination
		}
		return state.BlockStateLocked
	}

	// Closed?
	if b.GetClosed().GetActual(ctx) {
		return state.BlockStateClosed
	}

	// Not locked
	anySensorActive := false
	b.ForEachSensor(ctx, func(s state.Sensor) {
		if !anySensorActive && s.GetActive().GetActual(ctx) {
			anySensorActive = true
		}
	})
	if anySensorActive {
		return state.BlockStateOccupiedUnexpected
	}

	return state.BlockStateFree
}

// Is this block closed for traffic?
func (b *block) GetClosed() state.BoolProperty {
	return &b.closed
}

// Can a loc only leave this block at the same side it got in?
func (b *block) GetIsDeadEnd(context.Context) bool {
	return b.deadEnd
}

// Is this block considered a station?
func (b *block) GetIsStation(context.Context) bool {
	return b.getBlock().GetIsStation()
}

// Gets the state of the group this block belongs to.
// Can be nil.
func (b *block) GetBlockGroup(context.Context) state.BlockGroup {
	return b.blockGroup
}

// Is there a loc waiting in this block?
func (b *block) GetHasWaitingLoc(ctx context.Context) bool {
	if b.GetState(ctx) != state.BlockStateOccupied {
		return false
	}
	loc := b.GetLockedBy(ctx)
	if loc == nil {
		return false
	}
	automaticState := loc.GetAutomaticState().GetActual(ctx)
	switch automaticState {
	case state.AssignRoute:
		return loc.GetSpeed().GetRequested(ctx) == 0
	case state.WaitingForDestinationTimeout, state.WaitingForDestinationGroupMinimum:
		return true
	}
	return false
}
