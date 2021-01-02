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
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/refs"
)

// BlockSignal adds implementation methods to model.BlockSignal
type BlockSignal interface {
	ModuleEntity
	model.BlockSignal
}

type blockSignal struct {
	signal

	Address1      model.Address          `xml:"Address1"`
	Address2      model.Address          `xml:"Address2"`
	Address3      model.Address          `xml:"Address3"`
	Address4      model.Address          `xml:"Address4"`
	RedPattern    *int                   `xml:"RedPattern,omitempty"`
	GreenPattern  *int                   `xml:"GreenPattern,omitempty"`
	YellowPattern *int                   `xml:"YellowPattern,omitempty"`
	WhitePattern  *int                   `xml:"WhitePattern,omitempty"`
	BlockID       BlockRef               `xml:"Block,omitempty"`
	Position      *model.BlockSide       `xml:"Position,omitempty"`
	Type          *model.BlockSignalType `xml:"Type,omitempty"`
}

var _ BlockSignal = &blockSignal{}

func newBlockSignal() *blockSignal {
	bs := &blockSignal{}
	bs.signal.Initialize()
	return bs
}

// Accept a visit by the given visitor
func (bs *blockSignal) Accept(v model.EntityVisitor) interface{} {
	return v.VisitBlockSignal(bs)
}

// Get the Address of the entity
func (bs *blockSignal) GetAddress() model.Address {
	return bs.GetAddress1()
}

// Set the Address of the entity
func (bs *blockSignal) SetAddress(value model.Address) error {
	return bs.SetAddress1(value)
}

// Call the given callback for all (non-empty) addresses configured in this
// entity with the direction their being used.
func (bs *blockSignal) ForEachAddressUsage(cb func(model.AddressUsage)) {
	if !bs.Address1.IsEmpty() {
		cb(model.AddressUsage{
			Address:   bs.Address1,
			Direction: model.AddressDirectionOutput,
		})
	}
	if !bs.Address2.IsEmpty() {
		cb(model.AddressUsage{
			Address:   bs.Address2,
			Direction: model.AddressDirectionOutput,
		})
	}
	if !bs.Address3.IsEmpty() {
		cb(model.AddressUsage{
			Address:   bs.Address3,
			Direction: model.AddressDirectionOutput,
		})
	}
	if !bs.Address4.IsEmpty() {
		cb(model.AddressUsage{
			Address:   bs.Address4,
			Direction: model.AddressDirectionOutput,
		})
	}
}

// First address
// This is an output signal.
func (bs *blockSignal) GetAddress1() model.Address {
	return bs.Address1
}
func (bs *blockSignal) SetAddress1(value model.Address) error {
	if !bs.Address1.Equals(value) {
		bs.Address1 = value
		bs.OnModified()
	}
	return nil
}

// Second address
// This is an output signal.
func (bs *blockSignal) GetAddress2() model.Address {
	return bs.Address2
}
func (bs *blockSignal) SetAddress2(value model.Address) error {
	if !bs.Address2.Equals(value) {
		bs.Address2 = value
		bs.OnModified()
	}
	return nil
}

// Third address
// This is an output signal.
func (bs *blockSignal) GetAddress3() model.Address {
	return bs.Address3
}
func (bs *blockSignal) SetAddress3(value model.Address) error {
	if !bs.Address3.Equals(value) {
		bs.Address3 = value
		bs.OnModified()
	}
	return nil
}

// Fourth address
// This is an output signal.
func (bs *blockSignal) GetAddress4() model.Address {
	return bs.Address4
}
func (bs *blockSignal) SetAddress4(value model.Address) error {
	if !bs.Address4.Equals(value) {
		bs.Address4 = value
		bs.OnModified()
	}
	return nil
}

// Is the Red color available?
func (bs *blockSignal) GetIsRedAvailable() bool {
	return bs.GetRedPattern() != model.BlockSignalPatternDisabled
}

// Bit pattern used for color Red.
func (bs *blockSignal) GetRedPattern() int {
	return refs.IntValue(bs.RedPattern, model.DefaultBlockSignalRedPattern)
}
func (bs *blockSignal) SetRedPattern(value int) error {
	if bs.GetRedPattern() != value {
		bs.RedPattern = refs.NewInt(value)
		bs.OnModified()
	}
	return nil
}

// Is the Green color available?
func (bs *blockSignal) GetIsGreenAvailable() bool {
	return bs.GetGreenPattern() != model.BlockSignalPatternDisabled
}

// Bit pattern used for color Green.
func (bs *blockSignal) GetGreenPattern() int {
	return refs.IntValue(bs.GreenPattern, model.DefaultBlockSignalGreenPattern)
}
func (bs *blockSignal) SetGreenPattern(value int) error {
	if bs.GetGreenPattern() != value {
		bs.GreenPattern = refs.NewInt(value)
		bs.OnModified()
	}
	return nil
}

// Is the Yellow color available?
func (bs *blockSignal) GetIsYellowAvailable() bool {
	return bs.GetYellowPattern() != model.BlockSignalPatternDisabled
}

// Bit pattern used for color Yellow.
func (bs *blockSignal) GetYellowPattern() int {
	return refs.IntValue(bs.YellowPattern, model.DefaultBlockSignalYellowPattern)
}
func (bs *blockSignal) SetYellowPattern(value int) error {
	if bs.GetYellowPattern() != value {
		bs.YellowPattern = refs.NewInt(value)
		bs.OnModified()
	}
	return nil
}

// Is the White color available?
func (bs *blockSignal) GetIsWhiteAvailable() bool {
	return bs.GetWhitePattern() != model.BlockSignalPatternDisabled
}

// Bit pattern used for color White.
func (bs *blockSignal) GetWhitePattern() int {
	return refs.IntValue(bs.WhitePattern, model.DefaultBlockSignalWhitePattern)
}
func (bs *blockSignal) SetWhitePattern(value int) error {
	if bs.GetWhitePattern() != value {
		bs.WhitePattern = refs.NewInt(value)
		bs.OnModified()
	}
	return nil
}

// The block this signal protects.
func (bs *blockSignal) GetBlock() model.Block {
	x, _ := bs.BlockID.Get(bs.GetModule())
	return x
}
func (bs *blockSignal) SetBlock(value model.Block) error {
	return bs.BlockID.Set(value, bs.GetModule(), bs.OnModified)
}

// Side of the block where the signal is located.
func (bs *blockSignal) GetPosition() model.BlockSide {
	return refs.BlockSideValue(bs.Position, model.DefaultBlockSignalPosition)
}
func (bs *blockSignal) SetPosition(value model.BlockSide) error {
	if bs.GetPosition() != value {
		bs.Position = refs.NewBlockSide(value)
		bs.OnModified()
	}
	return nil
}

// Type of signal
func (bs *blockSignal) GetType() model.BlockSignalType {
	return refs.BlockSignalTypeValue(bs.Type, model.DefaultBlockSignalType)
}
func (bs *blockSignal) SetType(value model.BlockSignalType) error {
	if bs.GetType() != value {
		bs.Type = refs.NewBlockSignalType(value)
		bs.OnModified()
	}
	return nil
}
