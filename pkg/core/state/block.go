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

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Block specifies the state of a single block.
type Block interface {
	ModuleEntity
	Lockable

	// Is this equal to other?
	Equals(other Block) bool

	// Gets the underlying model
	GetModel() model.Block

	// Probability (in percentage) that a loc that is allowed to wait in this block
	// will actually wait.
	// When set to 0, no locs will wait (unless there is no route available).
	// When set to 100, all locs (that are allowed) will wait.
	GetWaitProbability(context.Context) int

	// Minimum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
	GetMinimumWaitTime(context.Context) int

	// Maximum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
	GetMaximumWaitTime(context.Context) int

	// Gets the predicate used to decide which locs are allowed to wait in this block.
	GetWaitPermissions() LocPredicate

	// By default the front of the block is on the right of the block.
	// When this property is set, that is reversed to the left of the block.
	// Setting this property will only alter the display behavior of the block.
	GetReverseSides(context.Context) bool

	// Is it allowed for locs to change direction in this block?
	GetChangeDirection(context.Context) model.ChangeDirection

	// Must reversing locs change direction (back to normal) in this block?
	GetChangeDirectionReversingLocs(context.Context) bool

	// Gets all sensors that are either an "entering" or a "reached" sensor for a route
	// that leads to this block.
	ForEachSensor(context.Context, func(Sensor))

	// Gets the current state of this block
	GetState(context.Context) BlockState

	// Is this block closed for traffic?
	GetClosed() BoolProperty

	// Can a loc only leave this block at the same side it got in?
	GetIsDeadEnd(context.Context) bool

	// Is this block considered a station?
	GetIsStation(context.Context) bool

	// Gets the state of the group this block belongs to.
	// Can be nil.
	GetBlockGroup(context.Context) BlockGroup

	// Is there a loc waiting in this block?
	GetHasWaitingLoc(context.Context) bool
}
