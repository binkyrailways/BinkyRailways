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
)

// Switch adds implementation methods to model.Switch
type Switch interface {
	ModuleEntity
	model.Switch
}

type stdSwitch struct {
	junction

	Address          model.Address         `xml:"Address"`
	FeedbackAddress  model.Address         `xml:"FeedbackAddress,omitempty"`
	HasFeedback      bool                  `xml:"HasFeedback,omitempty"`
	SwitchDuration   int                   `xml:"SwitchDuration,omitempty"`
	Invert           bool                  `xml:"Invert,omitempty"`
	InvertFeedback   bool                  `xml:"InvertFeedback,omitempty"`
	InitialDirection model.SwitchDirection `xml:"InitialDirection,omitempty"`
}

var _ Switch = &stdSwitch{}

func newSwitch() *stdSwitch {
	sw := &stdSwitch{
		HasFeedback:      false,
		SwitchDuration:   1000,
		Invert:           false,
		InvertFeedback:   false,
		InitialDirection: model.SwitchDirectionStraight,
	}
	sw.junction.Initialize(16, 12)
	return sw
}

// Get the Address of the entity
func (sw *stdSwitch) GetAddress() model.Address {
	return sw.Address
}

// Set the Address of the entity
func (sw *stdSwitch) SetAddress(value model.Address) error {
	if !sw.Address.Equals(value) {
		sw.Address = value
		sw.OnModified()
	}
	return nil
}

// Call the given callback for all (non-empty) addresses configured in this
// entity with the direction their being used.
func (sw *stdSwitch) ForEachAddressUsage(cb func(model.AddressUsage)) {
	if !sw.Address.IsEmpty() {
		cb(model.AddressUsage{
			Address:   sw.Address,
			Direction: model.AddressDirectionOutput,
		})
	}
}

// Does this switch send a feedback when switched?
func (sw *stdSwitch) GetHasFeedback() bool {
	return sw.HasFeedback
}
func (sw *stdSwitch) SetHasFeedback(value bool) error {
	if sw.HasFeedback != value {
		sw.HasFeedback = value
		sw.OnModified()
	}
	return nil
}

// Address of the feedback unit of the entity
func (sw *stdSwitch) GetFeedbackAddress() model.Address {
	return sw.FeedbackAddress
}
func (sw *stdSwitch) SetFeedbackAddress(value model.Address) error {
	if !sw.FeedbackAddress.Equals(value) {
		sw.FeedbackAddress = value
		sw.OnModified()
	}
	return nil
}

// Time (in ms) it takes for the switch to move from one direction to the other?
// This property is only used when <see cref="HasFeedback"/> is false.
func (sw *stdSwitch) GetSwitchDuration() int {
	return sw.SwitchDuration
}
func (sw *stdSwitch) SetSwitchDuration(value int) error {
	if sw.SwitchDuration != value {
		sw.SwitchDuration = value
		sw.OnModified()
	}
	return nil
}

// If set, the straight/off commands are inverted.
func (sw *stdSwitch) GetInvert() bool {
	return sw.Invert
}
func (sw *stdSwitch) SetInvert(value bool) error {
	if sw.Invert != value {
		sw.Invert = value
		sw.OnModified()
	}
	return nil
}

// If there is a different feedback address and this is set, the straight/off feedback states are inverted.
func (sw *stdSwitch) GetInvertFeedback() bool {
	return sw.InvertFeedback
}
func (sw *stdSwitch) SetInvertFeedback(value bool) error {
	if sw.InvertFeedback != value {
		sw.InvertFeedback = value
		sw.OnModified()
	}
	return nil
}

// At which direction should the switch be initialized?
func (sw *stdSwitch) GetInitialDirection() model.SwitchDirection {
	return sw.InitialDirection
}
func (sw *stdSwitch) SetInitialDirection(value model.SwitchDirection) error {
	if sw.InitialDirection != value {
		sw.InitialDirection = value
		sw.OnModified()
	}
	return nil
}
