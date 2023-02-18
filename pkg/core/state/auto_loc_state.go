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

import "fmt"

// AutoLocState describes the state of a loc in the automatic control mode.
type AutoLocState uint8

const (
	// Loc has not been assigned a route, but is ready to be assigned and run that route.
	// If the loc is no longer is automatic mode, it will be removed from the automatic loc controller.
	// If no suitable route can be found, the loc will stay in this state.
	// When the loc has been assigned a route, the route will be prepared and the state will change
	// to <see cref="WaitingForAssignedRouteReady"/>.
	AssignRoute AutoLocState = 0

	// The loc that was reversing is changing direction back to normal.
	// Once the direction is consistent, the state will change to <see cref="AssignRoute"/>.
	ReversingWaitingForDirectionChange AutoLocState = 1

	// The loc has been assigned a route and it waiting for this route to become ready.
	// Typically all junctions in the route will be set in the correct position now.
	// When the route is ready, the state will change to <see cref="Running"/>.
	WaitingForAssignedRouteReady AutoLocState = 2

	// The loc is running the assigned route.
	// The state of the loc will not change until a sensor trigger is received.
	Running AutoLocState = 3

	// The loc has triggered one of the 'entering destination' sensors of the assigned route.
	// No changes are made to the loc state when switching to this state.
	EnterSensorActivated AutoLocState = 4

	// The loc has triggered one of the 'entering destination' sensors of the assigned route.
	// The state of the loc will not change until a 'reached destination' sensor trigger is received.
	EnteringDestination AutoLocState = 5

	// The loc has triggered one of the 'reached destination' sensors of the assigned route.
	// No changes are made to the loc state when switching to this state.
	ReachedSensorActivated AutoLocState = 6

	// The loc has triggered one of the 'reached destination' sensors of the assigned route.
	// If the destination let's the loc wait, a timeout is started and the state is changed to
	// <see cref="WaitingForDestinationTimeout"/>.
	// Otherwise the state will change to <see cref="AssignRoute"/>.
	// If the loc is no longer is automatic mode, it will be removed from the automatic loc controller.
	ReachedDestination AutoLocState = 7

	// The loc has stopped at the destination and is waiting for a timeout until it can be assigned
	// a new route.
	WaitingForDestinationTimeout AutoLocState = 8

	// The loc has stopped at the destination and is waiting for a requirement on the group that contains the destination block.
	WaitingForDestinationGroupMinimum AutoLocState = 9
)

// String convers the given state into a human readable string.
func (s AutoLocState) String() string {
	switch s {
	case AssignRoute:
		return "AssignRoute"
	case ReversingWaitingForDirectionChange:
		return "ReversingWaitingForDirectionChange"
	case WaitingForAssignedRouteReady:
		return "WaitingForAssignedRouteReady"
	case Running:
		return "Running"
	case EnterSensorActivated:
		return "EnterSensorActivated"
	case EnteringDestination:
		return "EnteringDestination"
	case ReachedSensorActivated:
		return "ReachedSensorActivated"
	case ReachedDestination:
		return "ReachedDestination"
	case WaitingForDestinationTimeout:
		return "WaitingForDestinationTimeout"
	case WaitingForDestinationGroupMinimum:
		return "WaitingForDestinationGroupMinimum"
	default:
		return fmt.Sprintf("Unknown (%d)", int(s))
	}
}

// AcceptSensorSpeedBehavior returns true if it is acceptable in
// the given state to honor a speed behavior change of a sensor.
func (s AutoLocState) AcceptSensorSpeedBehavior() bool {
	switch s {
	case Running, EnterSensorActivated, EnteringDestination:
		return true
	default:
		return false
	}
}
