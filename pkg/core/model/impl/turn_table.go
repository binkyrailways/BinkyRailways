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
	"context"
	"iter"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/refs"
)

// TurnTable adds implementation methods to model.TurnTable
type TurnTable interface {
	ModuleEntity
	model.TurnTable
}

type turnTable struct {
	junction

	PositionAddress1 model.Address `xml:"PositionAddress1"`
	PositionAddress2 model.Address `xml:"PositionAddress2"`
	PositionAddress3 model.Address `xml:"PositionAddress3"`
	PositionAddress4 model.Address `xml:"PositionAddress4"`
	PositionAddress5 model.Address `xml:"PositionAddress5"`
	PositionAddress6 model.Address `xml:"PositionAddress6"`
	InvertPositions  *bool         `xml:"InvertPositions,omitempty"`
	WriteAddress     model.Address `xml:"WriteAddress"`
	InvertWrite      *bool         `xml:"InvertWrite,omitempty"`
	BusyAddress      model.Address `xml:"BusyAddress"`
	InvertBusy       *bool         `xml:"InvertBusy,omitempty"`
	FirstPosition    *int          `xml:"FirstPosition,omitempty"`
	LastPosition     *int          `xml:"LastPosition,omitempty"`
	InitialPosition  *int          `xml:"InitialPosition,omitempty"`
}

var _ TurnTable = &turnTable{}

func newTurnTable() *turnTable {
	sw := &turnTable{}
	sw.EnsureID()
	sw.SetDescription("New turntable")
	sw.junction.Initialize(16, 12)
	return sw
}

// GetEntityType returns the type of this entity
func (sw *turnTable) GetEntityType() string {
	return TypeTurnTable
}

// Accept a visit by the given visitor
func (sw *turnTable) Accept(v model.EntityVisitor) interface{} {
	return v.VisitTurnTable(sw)
}

// ForEachAddress iterates all addresses in this entity and any child entities.
func (sw *turnTable) ForEachAddress(cb func(addr model.Address, onUpdate func(context.Context, model.Address) error)) {
	cb(sw.PositionAddress1, sw.SetPositionAddress1)
	cb(sw.PositionAddress2, sw.SetPositionAddress2)
	cb(sw.PositionAddress3, sw.SetPositionAddress3)
	cb(sw.PositionAddress4, sw.SetPositionAddress4)
	cb(sw.PositionAddress5, sw.SetPositionAddress5)
	cb(sw.PositionAddress6, sw.SetPositionAddress6)
}

// Get the Address of the entity
func (sw *turnTable) GetAddress() model.Address {
	return sw.GetPositionAddress1()
}

// Set the Address of the entity
func (sw *turnTable) SetAddress(ctx context.Context, value model.Address) error {
	return sw.SetPositionAddress1(ctx, value)
}

// Return a sequence of all (non-empty) addresses configured in this
// entity with the direction their being used.
func (sw *turnTable) AllAddressUsages() iter.Seq[model.AddressUsage] {
	return func(yield func(model.AddressUsage) bool) {
		op := func(a model.Address, direction model.AddressDirection) bool {
			if !a.IsEmpty() {
				return yield(model.AddressUsage{
					Address:   a,
					Direction: direction,
				})
			}
			return true
		}
		if !op(sw.PositionAddress1, model.AddressDirectionOutput) {
			return
		}
		if !op(sw.PositionAddress2, model.AddressDirectionOutput) {
			return
		}
		if !op(sw.PositionAddress3, model.AddressDirectionOutput) {
			return
		}
		if !op(sw.PositionAddress4, model.AddressDirectionOutput) {
			return
		}
		if !op(sw.PositionAddress5, model.AddressDirectionOutput) {
			return
		}
		if !op(sw.PositionAddress6, model.AddressDirectionOutput) {
			return
		}
		if !op(sw.WriteAddress, model.AddressDirectionOutput) {
			return
		}
		if !op(sw.BusyAddress, model.AddressDirectionInput) {
			return
		}
	}
}

// Address of first position bit.
// This is an output signal.
func (sw *turnTable) GetPositionAddress1() model.Address {
	return sw.PositionAddress1
}
func (sw *turnTable) SetPositionAddress1(ctx context.Context, value model.Address) error {
	if !sw.PositionAddress1.Equals(value) {
		sw.PositionAddress1 = value
		sw.OnModified()
	}
	return nil
}

// Address of second position bit.
// This is an output signal.
func (sw *turnTable) GetPositionAddress2() model.Address {
	return sw.PositionAddress2
}
func (sw *turnTable) SetPositionAddress2(ctx context.Context, value model.Address) error {
	if !sw.PositionAddress2.Equals(value) {
		sw.PositionAddress2 = value
		sw.OnModified()
	}
	return nil
}

// Address of third position bit.
// This is an output signal.
func (sw *turnTable) GetPositionAddress3() model.Address {
	return sw.PositionAddress3
}
func (sw *turnTable) SetPositionAddress3(ctx context.Context, value model.Address) error {
	if !sw.PositionAddress3.Equals(value) {
		sw.PositionAddress3 = value
		sw.OnModified()
	}
	return nil
}

// Address of fourth position bit.
// This is an output signal.
func (sw *turnTable) GetPositionAddress4() model.Address {
	return sw.PositionAddress4
}
func (sw *turnTable) SetPositionAddress4(ctx context.Context, value model.Address) error {
	if !sw.PositionAddress4.Equals(value) {
		sw.PositionAddress4 = value
		sw.OnModified()
	}
	return nil
}

// Address of fifth position bit.
// This is an output signal.
func (sw *turnTable) GetPositionAddress5() model.Address {
	return sw.PositionAddress5
}
func (sw *turnTable) SetPositionAddress5(ctx context.Context, value model.Address) error {
	if !sw.PositionAddress5.Equals(value) {
		sw.PositionAddress5 = value
		sw.OnModified()
	}
	return nil
}

// Address of sixth position bit.
// This is an output signal.
func (sw *turnTable) GetPositionAddress6() model.Address {
	return sw.PositionAddress6
}
func (sw *turnTable) SetPositionAddress6(ctx context.Context, value model.Address) error {
	if !sw.PositionAddress6.Equals(value) {
		sw.PositionAddress6 = value
		sw.OnModified()
	}
	return nil
}

// If set, the straight/off commands used for position addresses are inverted.
func (sw *turnTable) GetInvertPositions() bool {
	return refs.BoolValue(sw.InvertPositions, model.DefaultTurnTableInvertPositions)
}
func (sw *turnTable) SetInvertPositions(value bool) error {
	if sw.GetInvertPositions() != value {
		sw.InvertPositions = refs.NewBool(value)
		sw.OnModified()
	}
	return nil
}

// Address of the line used to indicate a "write address".
// This is an output signal.
func (sw *turnTable) GetWriteAddress() model.Address {
	return sw.WriteAddress
}
func (sw *turnTable) SetWriteAddress(value model.Address) error {
	if !sw.WriteAddress.Equals(value) {
		sw.WriteAddress = value
		sw.OnModified()
	}
	return nil
}

// If set, the straight/off command used for "write address" line is inverted.
func (sw *turnTable) GetInvertWrite() bool {
	return refs.BoolValue(sw.InvertWrite, model.DefaultTurnTableInvertWrite)
}
func (sw *turnTable) SetInvertWrite(value bool) error {
	if sw.GetInvertWrite() != value {
		sw.InvertWrite = refs.NewBool(value)
		sw.OnModified()
	}
	return nil
}

// Address of the line used to indicate a "change of position in progress".
// This is an input signal.
func (sw *turnTable) GetBusyAddress() model.Address {
	return sw.BusyAddress
}
func (sw *turnTable) SetBusyAddress(value model.Address) error {
	if !sw.BusyAddress.Equals(value) {
		sw.BusyAddress = value
		sw.OnModified()
	}
	return nil
}

// If set, the input level used for "busy" line is inverted.
func (sw *turnTable) GetInvertBusy() bool {
	return refs.BoolValue(sw.InvertBusy, model.DefaultTurnTableInvertBusy)
}
func (sw *turnTable) SetInvertBusy(value bool) error {
	if sw.GetInvertBusy() != value {
		sw.InvertBusy = refs.NewBool(value)
		sw.OnModified()
	}
	return nil
}

// First position number. Typically 1.
func (sw *turnTable) GetFirstPosition() int {
	return refs.IntValue(sw.FirstPosition, model.DefaultTurnTableFirstPosition)
}
func (sw *turnTable) SetFirstPosition(value int) error {
	if sw.GetFirstPosition() != value {
		sw.FirstPosition = refs.NewInt(value)
		sw.OnModified()
	}
	return nil
}

// Last position number. Typically 63.
func (sw *turnTable) GetLastPosition() int {
	return refs.IntValue(sw.LastPosition, model.DefaultTurnTableLastPosition)
}
func (sw *turnTable) SetLastPosition(value int) error {
	if sw.GetLastPosition() != value {
		sw.LastPosition = refs.NewInt(value)
		sw.OnModified()
	}
	return nil
}

// Position number used to initialize the turntable with?
func (sw *turnTable) GetInitialPosition() int {
	return refs.IntValue(sw.InitialPosition, model.DefaultTurnTableInitialPosition)
}
func (sw *turnTable) SetInitialPosition(value int) error {
	if sw.GetInitialPosition() != value {
		sw.InitialPosition = refs.NewInt(value)
		sw.OnModified()
	}
	return nil
}
