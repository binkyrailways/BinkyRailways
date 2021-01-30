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

// Property contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type Property interface {
	ActualProperty

	// Fired when the requested value has changed.
	RequestedChanged() model.EventHandler

	// Is the request value equal to the actual value?
	IsConsistent() bool
}

// BoolProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type BoolProperty interface {
	Property
	ActualBoolProperty

	// Gets / sets the requested value
	GetRequested() bool
	SetRequested(value bool) error
}

// IntProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type IntProperty interface {
	Property
	ActualIntProperty

	// Gets / sets the requested value
	GetRequested() int
	SetRequested(value int) error
}

// LocDirectionProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type LocDirectionProperty interface {
	Property
	ActualLocDirectionProperty

	// Gets / sets the requested value
	GetRequested() LocDirection
	SetRequested(value LocDirection) error
}