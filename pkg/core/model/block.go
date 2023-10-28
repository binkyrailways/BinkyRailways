// Copyright 2020 Ewout Prangsma
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

package model

// Block is a place in the railway which is occupied by a single train and where a train can stop.
type Block interface {
	EndPoint

	// Probability (in percentage) that a loc that is allowed to wait in this block
	// will actually wait.
	// When set to 0, no locs will wait (unless there is no route available).
	// When set to 100, all locs (that are allowed) will wait.
	GetWaitProbability() int
	SetWaitProbability(value int) error

	// Minimum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
	GetMinimumWaitTime() int
	SetMinimumWaitTime(value int) error

	// Maximum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
	GetMaximumWaitTime() int
	SetMaximumWaitTime(value int) error

	// Gets the predicate used to decide which locs are allowed to wait in this block.
	GetWaitPermissions() LocStandardPredicate
	SetWaitPermissions(LocStandardPredicate) error

	// By default the front of the block is on the right of the block.
	// When this property is set, that is reversed to the left of the block.
	// Setting this property will only alter the display behavior of the block.
	GetReverseSides() bool
	SetReverseSides(value bool) error

	// Is it allowed for locs to change direction in this block?
	GetChangeDirection() ChangeDirection
	SetChangeDirection(value ChangeDirection) error

	// Must reversing locs change direction (back to normal) in this block?
	GetChangeDirectionReversingLocs() bool
	SetChangeDirectionReversingLocs(value bool) error

	// Determines how the system decides if this block is part of a station
	GetStationMode() StationMode
	SetStationMode(value StationMode) error

	// Is this block considered a station?
	GetIsStation() bool

	// The block group that this block belongs to (if any).
	GetBlockGroup() BlockGroup
	SetBlockGroup(value BlockGroup) error

	// Ensure implementation implements Block
	ImplementsBlock()
}
