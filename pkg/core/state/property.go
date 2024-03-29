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

import (
	"context"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Property contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type Property interface {
	ActualProperty

	// Is the request value equal to the actual value?
	IsConsistent(context.Context) bool
}

// TypedProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type TypedProperty[T any] interface {
	Property
	TypedActualProperty[T]

	// Gets / sets the requested value
	GetRequested(context.Context) T
	SetRequested(context.Context, T) error
	// Subscribe to requested changes
	SubscribeRequestChanges(func(context.Context, T)) context.CancelFunc
}

// BoolProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type BoolProperty interface {
	TypedProperty[bool]
}

// IntProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type IntProperty interface {
	TypedProperty[int]
}

// LocDirectionProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type LocDirectionProperty interface {
	TypedProperty[LocDirection]
}

// SwitchDirectionProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type SwitchDirectionProperty interface {
	TypedProperty[model.SwitchDirection]
}
