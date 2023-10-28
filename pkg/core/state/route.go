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

	// Is this equal to other?
	Equals(other Route) bool

	// Gets the underlying model
	GetModel() model.Route

	// Speed of locs when going this route.
	// This value is a percentage of the maximum / medium speed of the loc.
	// <value>0..100</value>
	GetSpeed(context.Context) int

	// Probability (in percentage) that a loc will take this route.
	// When multiple routes are available to choose from the route with the highest probability will have the highest
	// chance or being chosen.
	// <value>0..100</value>
	GetChooseProbability(context.Context) int

	// Gets the source block.
	GetFrom(context.Context) Block

	// Side of the <see cref="From"/> block at which this route will leave that block.
	GetFromBlockSide(context.Context) model.BlockSide

	// Gets the destination block.
	GetTo(context.Context) Block

	// Side of the <see cref="To"/> block at which this route will enter that block.
	GetToBlockSide(context.Context) model.BlockSide

	// Does this route require any switches to be in the non-straight state?
	GetHasNonStraightSwitches(context.Context) bool

	// Is the given sensor listed as one of the "entering destination" sensors of this route?
	IsEnteringDestinationSensor(context.Context, Sensor, Loc) bool

	// Is the given sensor listed as one of the "entering destination" sensors of this route?
	IsReachedDestinationSensor(context.Context, Sensor, Loc) bool

	// Does this route contains the given block (either as from, to or crossing)
	ContainsBlock(context.Context, Block) bool

	// Does this route contains the given sensor (either as entering or reached)
	ContainsSensor(context.Context, Sensor) bool

	// Does this route contains the given junction
	ContainsJunction(context.Context, Junction) bool

	// Does this route contains the given output
	ContainsOutput(context.Context, Output) bool

	// Returns true if this route has an output with a different state than the given state.
	HasConflictingOutput(context.Context, model.OutputWithState) bool

	// Gets the number of sensors that are listed as entering/reached sensor of this route.
	GetSensorCount(context.Context) int
	// Gets all sensors that are listed as entering/reached sensor of this route.
	ForEachSensor(context.Context, func(Sensor))

	// All routes that must be free before this route can be taken.
	GetCriticalSection(context.Context) CriticalSectionRoutes

	// Gets all events configured for this route.
	ForEachEvent(context.Context, func(RouteEvent))

	// Gets the predicate used to decide which locs are allowed to use this route.
	GetPermissions(context.Context) LocPredicate

	// Is this route open for traffic or not?
	// Setting to true, allows for maintance etc. on this route.
	GetClosed(context.Context) bool

	// Maximum time in seconds that this route should take.
	// If a loc takes this route and exceeds this duration, a warning is given.
	GetMaxDuration(context.Context) int

	// Prepare all junctions in this route, such that it can be taken.
	Prepare(context.Context)

	// Are all junctions set in the state required by this route?
	GetIsPrepared(context.Context) bool

	// Create a specific route state for the given loc.
	CreateStateForLoc(ctx context.Context, loc Loc) RouteForLoc

	// Fire the actions attached to the entering destination trigger.
	FireEnteringDestinationTrigger(context.Context, Loc)

	// Fire the actions attached to the destination reached trigger.
	FireDestinationReachedTrigger(context.Context, Loc)
}
