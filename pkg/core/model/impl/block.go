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
	"encoding/xml"
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/refs"
)

type block struct {
	blockFields
}

type blockFields struct {
	positionedModuleEntity
	WaitProbability              *int
	MinimumWaitTime              *int
	MaximumWaitTime              *int
	ReverseSides                 *bool
	ChangeDirection              *model.ChangeDirection
	ChangeDirectionReversingLocs *bool
	StationMode                  *model.StationMode
	BlockGroupID                 string               `xml:"BlockGroup,omitempty"`
	WaitPermissions              locStandardPredicate `xml:"WaitPermissions"`
}

var _ model.Block = &block{}

// newBlock initialize a new block
func newBlock() *block {
	b := &block{}
	b.positionedModuleEntity.Initialize(32, 16)
	b.WaitPermissions.SetContainer(b)
	return b
}

// UnmarshalXML unmarshals a block.
func (b *block) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&b.blockFields, &start); err != nil {
		return err
	}
	b.blockFields.SetContainer(b)
	return nil
}

// Probability (in percentage) that a loc that is allowed to wait in this block
// will actually wait.
// When set to 0, no locs will wait (unless there is no route available).
// When set to 100, all locs (that are allowed) will wait.
func (b *block) GetWaitProbability() int {
	return refs.IntValue(b.WaitProbability, model.DefaultBlockWaitProbability)
}
func (b *block) SetWaitProbability(value int) error {
	if b.GetWaitProbability() != value {
		b.WaitProbability = refs.NewInt(value)
		b.OnModified()
	}
	return nil
}

// Minimum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
func (b *block) GetMinimumWaitTime() int {
	return refs.IntValue(b.MinimumWaitTime, model.DefaultBlockMinimumWaitTime)
}
func (b *block) SetMinimumWaitTime(value int) error {
	if b.GetMinimumWaitTime() != value {
		b.MinimumWaitTime = refs.NewInt(value)
		b.OnModified()
	}
	return nil
}

// Maximum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
func (b *block) GetMaximumWaitTime() int {
	return refs.IntValue(b.MaximumWaitTime, model.DefaultBlockMaximumWaitTime)
}
func (b *block) SetMaximumWaitTime(value int) error {
	if b.GetMaximumWaitTime() != value {
		b.MaximumWaitTime = refs.NewInt(value)
		b.OnModified()
	}
	return nil
}

// Gets the predicate used to decide which locs are allowed to wait in this block.
func (b *block) GetWaitPermissions() model.LocStandardPredicate {
	return &b.WaitPermissions
}

// By default the front of the block is on the right of the block.
// When this property is set, that is reversed to the left of the block.
// Setting this property will only alter the display behavior of the block.
func (b *block) GetReverseSides() bool {
	return refs.BoolValue(b.ReverseSides, model.DefaultBlockReverseSides)
}
func (b *block) SetReverseSides(value bool) error {
	if b.GetReverseSides() != value {
		b.ReverseSides = refs.NewBool(value)
		b.OnModified()
	}
	return nil
}

// Is it allowed for locs to change direction in this block?
func (b *block) GetChangeDirection() model.ChangeDirection {
	return refs.ChangeDirectionValue(b.ChangeDirection, model.DefaultBlockChangeDirection)
}
func (b *block) SetChangeDirection(value model.ChangeDirection) error {
	if b.GetChangeDirection() != value {
		b.ChangeDirection = refs.NewChangeDirection(value)
		b.OnModified()
	}
	return nil
}

// Must reversing locs change direction (back to normal) in this block?
func (b *block) GetChangeDirectionReversingLocs() bool {
	return refs.BoolValue(b.ChangeDirectionReversingLocs, model.DefaultBlockChangeDirectionReversingLocs)
}
func (b *block) SetChangeDirectionReversingLocs(value bool) error {
	if b.GetChangeDirectionReversingLocs() != value {
		b.ChangeDirectionReversingLocs = refs.NewBool(value)
		b.OnModified()
	}
	return nil
}

// Determines how the system decides if this block is part of a station
func (b *block) GetStationMode() model.StationMode {
	return refs.StationModeValue(b.StationMode, model.DefaultBlockStationMode)
}
func (b *block) SetStationMode(value model.StationMode) error {
	if b.GetStationMode() != value {
		b.StationMode = refs.NewStationMode(value)
		b.OnModified()
	}
	return nil
}

// Is this block considered a station?
func (b *block) GetIsStation() bool {
	switch b.GetStationMode() {
	case model.StationModeAlways:
		return true
	case model.StationModeNever:
		return false
	default:
		if b.GetChangeDirection() == model.ChangeDirectionAllow {
			return b.GetWaitProbability() >= 50
		}
		return b.GetWaitProbability() >= 75
	}
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
