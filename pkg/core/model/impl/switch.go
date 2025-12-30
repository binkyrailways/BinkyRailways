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

// Switch adds implementation methods to model.Switch
type Switch interface {
	ModuleEntity
	model.Switch
}

type stdSwitch struct {
	junction

	Address          model.Address          `xml:"Address"`
	FeedbackAddress  model.Address          `xml:"FeedbackAddress,omitempty"`
	HasFeedback      *bool                  `xml:"HasFeedback,omitempty"`
	IsLeft           *bool                  `xml:"IsLeft,omitempty"`
	SwitchDuration   *int                   `xml:"SwitchDuration,omitempty"`
	Invert           *bool                  `xml:"Invert,omitempty"`
	InvertFeedback   *bool                  `xml:"InvertFeedback,omitempty"`
	InitialDirection *model.SwitchDirection `xml:"InitialDirection,omitempty"`
}

var _ Switch = &stdSwitch{}

func newSwitch() *stdSwitch {
	sw := &stdSwitch{}
	sw.EnsureID()
	sw.SetDescription("New switch")
	sw.junction.Initialize(16, 12)
	return sw
}

// GetEntityType returns the type of this entity
func (sw *stdSwitch) GetEntityType() string {
	return TypeSwitch
}

// Accept a visit by the given visitor
func (sw *stdSwitch) Accept(v model.EntityVisitor) interface{} {
	return v.VisitSwitch(sw)
}

// ForEachAddress iterates all addresses in this entity and any child entities.
func (sw *stdSwitch) ForEachAddress(cb func(addr model.Address, onUpdate func(context.Context, model.Address) error)) {
	cb(sw.Address, sw.SetAddress)
	cb(sw.FeedbackAddress, sw.SetFeedbackAddress)
}

// Get the Address of the entity
func (sw *stdSwitch) GetAddress() model.Address {
	return sw.Address
}

// Set the Address of the entity
func (sw *stdSwitch) SetAddress(ctx context.Context, value model.Address) error {
	if !sw.Address.Equals(value) {
		if sw.GetDescription() == sw.Address.Value {
			sw.Description = value.Value
		}
		sw.Address = value
		sw.OnModified()
	}
	return nil
}

// Return a sequence of all (non-empty) addresses configured in this
// entity with the direction their being used.
func (sw *stdSwitch) AllAddressUsages() iter.Seq[model.AddressUsage] {
	return func(yield func(model.AddressUsage) bool) {
		if !sw.Address.IsEmpty() {
			if !yield(model.AddressUsage{
				Address:   sw.Address,
				Direction: model.AddressDirectionOutput,
			}) {
				return
			}
		}
		if !sw.FeedbackAddress.IsEmpty() {
			if !yield(model.AddressUsage{
				Address:   sw.FeedbackAddress,
				Direction: model.AddressDirectionInput,
			}) {
				return
			}
		}
	}
}

// Is this switch turning to the left?
// Otherwise it is turning to the right.
func (sw *stdSwitch) GetIsLeft() bool {
	return refs.BoolValue(sw.IsLeft, false)
}
func (sw *stdSwitch) SetIsLeft(value bool) error {
	if sw.GetIsLeft() != value {
		sw.IsLeft = refs.NewBool(value)
		sw.OnModified()
	}
	return nil

}

// Does this switch send a feedback when switched?
func (sw *stdSwitch) GetHasFeedback() bool {
	return refs.BoolValue(sw.HasFeedback, true)
}
func (sw *stdSwitch) SetHasFeedback(value bool) error {
	if sw.GetHasFeedback() != value {
		sw.HasFeedback = refs.NewBool(value)
		sw.OnModified()
	}
	return nil
}

// Address of the feedback unit of the entity
func (sw *stdSwitch) GetFeedbackAddress() model.Address {
	return sw.FeedbackAddress
}
func (sw *stdSwitch) SetFeedbackAddress(ctx context.Context, value model.Address) error {
	if !sw.FeedbackAddress.Equals(value) {
		sw.FeedbackAddress = value
		sw.OnModified()
	}
	return nil
}

// Time (in ms) it takes for the switch to move from one direction to the other?
// This property is only used when <see cref="HasFeedback"/> is false.
func (sw *stdSwitch) GetSwitchDuration() int {
	return refs.IntValue(sw.SwitchDuration, 1000)
}
func (sw *stdSwitch) SetSwitchDuration(value int) error {
	if sw.GetSwitchDuration() != value {
		sw.SwitchDuration = refs.NewInt(value)
		sw.OnModified()
	}
	return nil
}

// If set, the straight/off commands are inverted.
func (sw *stdSwitch) GetInvert() bool {
	return refs.BoolValue(sw.Invert, false)
}
func (sw *stdSwitch) SetInvert(value bool) error {
	if sw.GetInvert() != value {
		sw.Invert = refs.NewBool(value)
		sw.OnModified()
	}
	return nil
}

// If there is a different feedback address and this is set, the straight/off feedback states are inverted.
func (sw *stdSwitch) GetInvertFeedback() bool {
	return refs.BoolValue(sw.InvertFeedback, false)
}
func (sw *stdSwitch) SetInvertFeedback(value bool) error {
	if sw.GetInvertFeedback() != value {
		sw.InvertFeedback = refs.NewBool(value)
		sw.OnModified()
	}
	return nil
}

// At which direction should the switch be initialized?
func (sw *stdSwitch) GetInitialDirection() model.SwitchDirection {
	return refs.SwitchDirectionValue(sw.InitialDirection, model.SwitchDirectionStraight)
}
func (sw *stdSwitch) SetInitialDirection(value model.SwitchDirection) error {
	if sw.GetInitialDirection() != value {
		sw.InitialDirection = refs.NewSwitchDirection(value)
		sw.OnModified()
	}
	return nil
}
