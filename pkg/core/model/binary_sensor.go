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

// BinarySensor is a device that signals an event on the railway with a state of "on" or "off".
type BinarySensor interface {
	Sensor
	// IActionTriggerSource

	/// <summary>
	/// Trigger fired when the sensor becomes active.
	/// </summary>
	//      IActionTrigger ActivateTrigger { get; }

	/// <summary>
	/// Trigger fired when the sensor becomes in-active.
	/// </summary>
	//        IActionTrigger DeActivateTrigger { get; }
}
