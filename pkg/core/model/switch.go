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

import "context"

// Switch is a standard two-way left/right switch
type Switch interface {
	Junction
	AddressEntity

	// Is this switch turning to the left?
	// Otherwise it is turning to the right.
	GetIsLeft() bool
	SetIsLeft(value bool) error

	// Does this switch send a feedback when switched?
	GetHasFeedback() bool
	SetHasFeedback(value bool) error

	// Address of the feedback unit of the entity
	GetFeedbackAddress() Address
	SetFeedbackAddress(ctx context.Context, value Address) error

	// Time (in ms) it takes for the switch to move from one direction to the other?
	// This property is only used when <see cref="HasFeedback"/> is false.
	GetSwitchDuration() int
	SetSwitchDuration(value int) error

	// If set, the straight/off commands are inverted.
	GetInvert() bool
	SetInvert(value bool) error

	// If there is a different feedback address and this is set, the straight/off feedback states are inverted.
	GetInvertFeedback() bool
	SetInvertFeedback(value bool) error

	// At which direction should the switch be initialized?
	GetInitialDirection() SwitchDirection
	SetInitialDirection(value SwitchDirection) error
}
