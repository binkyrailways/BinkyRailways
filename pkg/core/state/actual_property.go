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
	GetActual() bool
	SetActual(value bool) error
}

// ActualIntProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualIntProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual() int
	SetActual(value int) error
}

// ActualTimeProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualTimeProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual() time.Time
	SetActual(value time.Time) error
}

// ActualAutoLocStateProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualAutoLocStateProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual() AutoLocState
	SetActual(value AutoLocState) error
}

// ActualLocDirectionProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualLocDirectionProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual() LocDirection
	SetActual(value LocDirection) error
}

// ActualSwitchDirectionProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualSwitchDirectionProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual() model.SwitchDirection
	SetActual(value model.SwitchDirection) error
}

// ActualBlockSideProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualBlockSideProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual() model.BlockSide
	SetActual(value model.BlockSide) error
}

// ActualBlockProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualBlockProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual() Block
	SetActual(value Block) error
}

// ActualRouteProperty contains the value of a property in a state object.
// The value contains an actual value.
type ActualRouteProperty interface {
	ActualProperty

	// Gets / sets the actual value
	GetActual() Route
	SetActual(value Route) error
}
