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

import "github.com/binkyrailways/BinkyRailways/pkg/core/model"

// Switch specifies the state of a single standard switch
type Switch interface {
	Junction

	// Address of the entity
	GetAddress() model.Address

	// Address of the feedback line of the entity
	GetFeedbackAddress() model.Address

	// Does this switch send a feedback when switched?
	GetHasFeedback() bool

	// Time (in ms) it takes for the switch to move from one direction to the other?
	// This property is only used when <see cref="HasFeedback"/> is false.
	GetSwitchDuration() int

	// If set, the straight/off commands are inverted.
	GetInvert() bool

	// If set, the straight/off feedback states are inverted.
	GetInvertFeedback() bool

	// Direction of the switch.
	//IStateProperty<SwitchDirection> Direction { get; }
}
