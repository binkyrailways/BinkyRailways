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
	// Gets the name of the property
	GetName() string
}

// TypedActualProperty contains the value of a property in a state object.
// The value contains an actual value.
type TypedActualProperty[T interface{}] interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual(context.Context) T
	SetActual(context.Context, T) (bool, error)
	// Subscribe to actual changes
	SubscribeActualChanges(func(context.Context, T)) context.CancelFunc
}

// ActualBoolProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualBoolProperty interface {
	TypedActualProperty[bool]
}

// ActualIntProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualIntProperty interface {
	TypedActualProperty[int]
}

// ActualTimeProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualTimeProperty interface {
	TypedActualProperty[time.Time]
}

// ActualAutoLocStateProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualAutoLocStateProperty interface {
	TypedActualProperty[AutoLocState]
}

// ActualLocDirectionProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualLocDirectionProperty interface {
	TypedActualProperty[LocDirection]
}

// ActualSwitchDirectionProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualSwitchDirectionProperty interface {
	TypedActualProperty[model.SwitchDirection]
}

// ActualBlockSideProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualBlockSideProperty interface {
	TypedActualProperty[model.BlockSide]
}

// ActualBlockProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualBlockProperty interface {
	TypedActualProperty[Block]
}

// ActualRouteProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualRouteProperty interface {
	TypedActualProperty[Route]
}

// ActualRouteForLocProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualRouteForLocProperty interface {
	TypedActualProperty[RouteForLoc]
}

// ActualRouteOptionsProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualRouteOptionsProperty interface {
	TypedActualProperty[RouteOptions]
}
