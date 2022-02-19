// Copyright 2022 Ewout Prangsma
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

type RouteOption struct {
	Route      Route
	IsPossible bool
	Reason     RouteImpossibleReason
	Extra      string
}

func (ro RouteOption) GetReasonDescription() string {
	if ro.IsPossible {
		return "Route is possible"
	}
	switch ro.Reason {
	case RouteImpossibleReasonLocked:
		return fmt.Sprintf("Route is locked: %s", ro.Extra)
	case RouteImpossibleReasonSensorActive:
		return "Sensor active"
	case RouteImpossibleReasonClosed:
		return "Route closed"
	case RouteImpossibleReasonDestinationClosed:
		return "Route destination closed"
	case RouteImpossibleReasonOpposingTraffic:
		return fmt.Sprintf("Opposing traffic: %s", ro.Extra)
	case RouteImpossibleReasonNone:
		return "Route is possible"
	case RouteImpossibleReasonDirectionChangeNeeded:
		return "Route requires direction change"
	case RouteImpossibleReasonNoPermission:
		return "No permission"
	case RouteImpossibleReasonCriticalSectionOccupied:
		return "Critical section occupied"
	case RouteImpossibleReasonDeadLock:
		return "Deadlock"
	default:
		return fmt.Sprintf("Unknown reason %d", ro.Reason)
	}
}

func (ro RouteOption) String() string {
	return fmt.Sprintf("%s - %s", ro.Route.GetDescription(), ro.GetReasonDescription())
}

// Equals returns true if the given route options have the same values.
func (ro RouteOption) Equals(other RouteOption) bool {
	return ro.Route == other.Route &&
		ro.IsPossible == other.IsPossible &&
		ro.Reason == other.Reason &&
		ro.Extra == other.Extra
}
