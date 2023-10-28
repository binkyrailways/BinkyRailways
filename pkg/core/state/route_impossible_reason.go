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

type RouteImpossibleReason uint8

const (
	// Route is possible
	RouteImpossibleReasonNone RouteImpossibleReason = 0

	// Route is locked by another loc
	RouteImpossibleReasonLocked RouteImpossibleReason = 1

	// Route is closed
	RouteImpossibleReasonClosed RouteImpossibleReason = 2

	// Destination block is closed
	RouteImpossibleReasonDestinationClosed RouteImpossibleReason = 3

	// A sensor in the route is active
	RouteImpossibleReasonSensorActive RouteImpossibleReason = 4

	// There is opposing traffic in future routes
	RouteImpossibleReasonOpposingTraffic RouteImpossibleReason = 5

	// A change in direction of the loc is needed for this block
	RouteImpossibleReasonDirectionChangeNeeded RouteImpossibleReason = 6

	// Loc has no permission for this route
	RouteImpossibleReasonNoPermission RouteImpossibleReason = 7

	// Critical section is occupied
	RouteImpossibleReasonCriticalSectionOccupied RouteImpossibleReason = 8

	// Future deadlock
	RouteImpossibleReasonDeadLock RouteImpossibleReason = 9
)
