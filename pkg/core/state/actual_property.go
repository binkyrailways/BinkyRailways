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
	"time"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// ActualProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualProperty interface {
}

// ActualBoolProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualBoolProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual(context.Context) bool
	SetActual(context.Context, bool) error
	// Subscribe to actual changes
	SubscribeActualChanges(func(context.Context, bool))
}

// ActualIntProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualIntProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual(context.Context) int
	SetActual(context.Context, int) error
	// Subscribe to actual changes
	SubscribeActualChanges(func(context.Context, int))
}

// ActualTimeProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualTimeProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual(context.Context) time.Time
	SetActual(context.Context, time.Time) error
	// Subscribe to actual changes
	SubscribeActualChanges(func(context.Context, time.Time))
}

// ActualAutoLocStateProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualAutoLocStateProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual(context.Context) AutoLocState
	SetActual(context.Context, AutoLocState) error
	// Subscribe to actual changes
	SubscribeActualChanges(func(context.Context, AutoLocState))
}

// ActualLocDirectionProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualLocDirectionProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual(context.Context) LocDirection
	SetActual(context.Context, LocDirection) error
	// Subscribe to actual changes
	SubscribeActualChanges(func(context.Context, LocDirection))
}

// ActualSwitchDirectionProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualSwitchDirectionProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual(context.Context) model.SwitchDirection
	SetActual(context.Context, model.SwitchDirection) error
	// Subscribe to actual changes
	SubscribeActualChanges(func(context.Context, model.SwitchDirection))
}

// ActualBlockSideProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualBlockSideProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual(context.Context) model.BlockSide
	SetActual(context.Context, model.BlockSide) error
	// Subscribe to actual changes
	SubscribeActualChanges(func(context.Context, model.BlockSide))
}

// ActualBlockProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualBlockProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual(context.Context) Block
	SetActual(context.Context, Block) error
	// Subscribe to actual changes
	SubscribeActualChanges(func(context.Context, Block))
}

// ActualRouteProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualRouteProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual(context.Context) Route
	SetActual(context.Context, Route) error
	// Subscribe to actual changes
	SubscribeActualChanges(func(context.Context, Route))
}

// ActualRouteForLocProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualRouteForLocProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual(context.Context) RouteForLoc
	SetActual(context.Context, RouteForLoc) error
	// Subscribe to actual changes
	SubscribeActualChanges(func(context.Context, RouteForLoc))
}
