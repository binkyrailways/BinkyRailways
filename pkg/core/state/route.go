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

// Route specifies the state of a single route.
type Route interface {
	ModuleEntity
	Lockable

	// Gets the underlying model
	GetModel() model.Route

	// Speed of locs when going this route.
	// This value is a percentage of the maximum / medium speed of the loc.
	// <value>0..100</value>
	GetSpeed() int

	// Probability (in percentage) that a loc will take this route.
	// When multiple routes are available to choose from the route with the highest probability will have the highest
	// chance or being chosen.
	// <value>0..100</value>
	GetChooseProbability() int

	// Gets the source block.
	GetFrom() Block

	// Side of the <see cref="From"/> block at which this route will leave that block.
	GetFromBlockSide() model.BlockSide

	// Gets the destination block.
	GetTo() Block

	// Side of the <see cref="To"/> block at which this route will enter that block.
	GetToBlockSide() model.BlockSide

	// Does this route require any switches to be in the non-straight state?
	GetHasNonStraightSwitches() bool

	// Is the given sensor listed as one of the "entering destination" sensors of this route?
	IsEnteringDestinationSensor(Sensor, Loc) bool

	// Is the given sensor listed as one of the "entering destination" sensors of this route?
	IsReachedDestinationSensor(Sensor, Loc) bool

	// Does this route contains the given block (either as from, to or crossing)
	ContainsBlock(Block) bool

	// Does this route contains the given sensor (either as entering or reached)
	ContainsSensor(Sensor) bool

	// Does this route contains the given junction
	ContainsJunction(Junction) bool

	// Gets the number of sensors that are listed as entering/reached sensor of this route.
	GetSensorCount(context.Context) int
	// Gets all sensors that are listed as entering/reached sensor of this route.
	ForEachSensor(func(Sensor))

	// All routes that must be free before this route can be taken.
	GetCriticalSection() CriticalSectionRoutes

	// Gets all events configured for this route.
	ForEachEvent(func(RouteEvent))

	// Gets the predicate used to decide which locs are allowed to use this route.
	GetPermissions() LocPredicate

	// Is this route open for traffic or not?
	// Setting to true, allows for maintance etc. on this route.
	GetClosed() bool

	// Maximum time in seconds that this route should take.
	// If a loc takes this route and exceeds this duration, a warning is given.
	GetMaxDuration() int

	// Prepare all junctions in this route, such that it can be taken.
	Prepare()

	// Are all junctions set in the state required by this route?
	GetIsPrepared() bool

	// Create a specific route state for the given loc.
	CreateStateForLoc(loc Loc) RouteForLoc

	// Fire the actions attached to the entering destination trigger.
	FireEnteringDestinationTrigger(Loc)

	// Fire the actions attached to the destination reached trigger.
	FireDestinationReachedTrigger(Loc)
}
