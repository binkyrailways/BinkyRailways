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

package impl

import (
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type block struct {
	positionedEntity

	WaitProbability              int
	MinimumWaitTime              int
	MaximumWaitTime              int
	ReverseSides                 bool
	ChangeDirection              model.ChangeDirection
	ChangeDirectionReversingLocs bool
	IsStation                    bool
	StationMode                  model.StationMode
	BlockGroupID                 string `xml:"BlockGroup"`
}

var _ model.Block = &block{}

// newBlock initialize a new block
func newBlock() *block {
	b := &block{
		WaitProbability:              model.DefaultBlockWaitProbability,
		MinimumWaitTime:              model.DefaultBlockMinimumWaitTime,
		MaximumWaitTime:              model.DefaultBlockMaximumWaitTime,
		ReverseSides:                 model.DefaultBlockReverseSides,
		ChangeDirection:              model.DefaultBlockChangeDirection,
		ChangeDirectionReversingLocs: model.DefaultBlockChangeDirectionReversingLocs,
		StationMode:                  model.DefaultBlockStationMode,
	}
	b.positionedEntity.Initialize(32, 16)
	return b
}

// Probability (in percentage) that a loc that is allowed to wait in this block
// will actually wait.
// When set to 0, no locs will wait (unless there is no route available).
// When set to 100, all locs (that are allowed) will wait.
func (b *block) GetWaitProbability() int {
	return b.WaitProbability
}
func (b *block) SetWaitProbability(value int) error {
	if b.WaitProbability != value {
		b.WaitProbability = value
		b.OnModified()
	}
	return nil
}

// Minimum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
func (b *block) GetMinimumWaitTime() int {
	return b.MinimumWaitTime
}
func (b *block) SetMinimumWaitTime(value int) error {
	if b.MinimumWaitTime != value {
		b.MinimumWaitTime = value
		b.OnModified()
	}
	return nil
}

// Maximum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
func (b *block) GetMaximumWaitTime() int {
	return b.MaximumWaitTime
}
func (b *block) SetMaximumWaitTime(value int) error {
	if b.MaximumWaitTime != value {
		b.MaximumWaitTime = value
		b.OnModified()
	}
	return nil
}

// Gets the predicate used to decide which locs are allowed to wait in this block.
//        ILocStandardPredicate WaitPermissions { get; }

// By default the front of the block is on the right of the block.
// When this property is set, that is reversed to the left of the block.
// Setting this property will only alter the display behavior of the block.
func (b *block) GetReverseSides() bool {
	return b.ReverseSides
}
func (b *block) SetReverseSides(value bool) error {
	if b.ReverseSides != value {
		b.ReverseSides = value
		b.OnModified()
	}
	return nil
}

// Is it allowed for locs to change direction in this block?
func (b *block) GetChangeDirection() model.ChangeDirection {
	return b.ChangeDirection
}
func (b *block) SetChangeDirection(value model.ChangeDirection) error {
	if b.ChangeDirection != value {
		b.ChangeDirection = value
		b.OnModified()
	}
	return nil
}

// Must reversing locs change direction (back to normal) in this block?
func (b *block) GetChangeDirectionReversingLocs() bool {
	return b.ChangeDirectionReversingLocs
}
func (b *block) SetChangeDirectionReversingLocs(value bool) error {
	if b.ChangeDirectionReversingLocs != value {
		b.ChangeDirectionReversingLocs = value
		b.OnModified()
	}
	return nil
}

// Determines how the system decides if this block is part of a station
func (b *block) GetStationMode() model.StationMode {
	return b.StationMode
}
func (b *block) SetStationMode(value model.StationMode) error {
	if b.StationMode != value {
		b.StationMode = value
		b.OnModified()
	}
	return nil
}

// Is this block considered a station?
func (b *block) GetIsStation() bool {
	return b.IsStation
}
func (b *block) SetIsStation(value bool) error {
	if b.IsStation != value {
		b.IsStation = value
		b.OnModified()
	}
	return nil
}

// The block group that this block belongs to (if any).
func (b *block) GetBlockGroup() model.BlockGroup {
	if b.BlockGroupID == "" {
		return nil
	}
	bg, _ := b.GetModule().GetBlockGroups().Get(b.BlockGroupID)
	return bg
}
func (b *block) SetBlockGroup(value model.BlockGroup) error {
	id := ""
	var module model.Module
	if value != nil {
		id = value.GetID()
		module = value.GetModule()
	}
	if b.BlockGroupID != id {
		if b.GetModule() != module {
			return fmt.Errorf("Invalid module")
		}
		b.BlockGroupID = id
		b.OnModified()
	}
	return nil
}
