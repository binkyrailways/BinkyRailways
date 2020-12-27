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

// LocSpeedBehavior indicates how a route event changes the speed of the occupying loc.
type LocSpeedBehavior string

const (
	// LocSpeedBehaviorDefault indicates the speed change is controlled by state behavior
	LocSpeedBehaviorDefault LocSpeedBehavior = "Default"
	// LocSpeedBehaviorNoChange indicates no change in speed
	LocSpeedBehaviorNoChange LocSpeedBehavior = "NoChange"
	// LocSpeedBehaviorMedium indicates that the speed is set to medium speed.
	LocSpeedBehaviorMedium LocSpeedBehavior = "Medium"
	// LocSpeedBehaviorMinimum indicates that the speed is set to minimum speed.
	LocSpeedBehaviorMinimum LocSpeedBehavior = "Minimum"
	// LocSpeedBehaviorMaximum indicates that the speed is set to maximum speed.
	LocSpeedBehaviorMaximum LocSpeedBehavior = "Maximum"
)
