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

// Route from one block to another.
type Route interface {
	ModuleEntity
	ActionTriggerSource

	// Starting point of the route
	GetFrom() EndPoint
	SetFrom(value EndPoint) error

	// Side of the <see cref="From"/> block at which this route will leave that block.
	GetFromBlockSide() BlockSide
	SetFromBlockSide(value BlockSide) error

	// End point of the route
	GetTo() EndPoint
	SetTo(value EndPoint) error

	// Side of the <see cref="To"/> block at which this route will enter that block.
	GetToBlockSide() BlockSide
	SetToBlockSide(value BlockSide) error

	// Set of junctions with their states that are crossed when taking this route.
	GetCrossingJunctions() JunctionWithStateSet

	// Set of events that change the state of the route and it's running loc.
	GetEvents() RouteEventSet

	// Speed of locs when going this route.
	// This value is a percentage of the maximum / medium speed of the loc.
	// <value>0..100</value>
	GetSpeed() int
	SetSpeed(value int) error

	// Probability (in percentage) that a loc will take this route.
	// When multiple routes are available to choose from the route with the highest probability will have the highest
	// chance or being chosen.
	// <value>0..100</value>
	GetChooseProbability() int
	SetChooseProbability(value int) error

	/// Gets the predicate used to decide which locs are allowed to use this route.
	GetPermissions() LocStandardPredicate

	// Is this rout open for traffic or not?
	// Setting to true, allows for maintance etc. on this route.
	GetClosed() bool
	SetClosed(value bool) error

	// Maximum time in seconds that this route should take.
	// If a loc takes this route and exceeds this duration, a warning is given.
	GetMaxDuration() int
	SetMaxDuration(value int) error

	// Trigger fired when a loc has starts entering the destination of this route.
	GetEnteringDestinationTrigger() ActionTrigger

	// Trigger fired when a loc has reached the destination of this route.
	GetDestinationReachedTrigger() ActionTrigger

	// Ensure implementation implements Route
	ImplementsRoute()
}
