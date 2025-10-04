// Copyright 2025 Ewout Prangsma
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

package esphome

import (
	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Add an object of type ServoSwitch
func addServoSwitch(f *DeviceFile, objModel model.BinkyNetObject) error {
	// Build output
	output, err := f.createOutput(objModel, "servo_output_", api.ConnectionNameServo, 0)
	if err != nil {
		return err
	}
	output.StateTopic = ""
	output.CommandTopic = ""

	// Build servo
	servo, err := f.createServo(objModel, "servo_", api.ConnectionNameServo, 0)
	if err != nil {
		return err
	}
	servo.Output = output.Id

	// Build number component
	number, err := f.createNumber(objModel, "", api.ConnectionNameServo, 0)
	if err != nil {
		return err
	}
	number.SetAction = []Action{{
		"servo.write": map[string]any{
			"id":    servo.Id,
			"level": yamlLambda("return x / 100.0;"),
		},
	}}

	// Create relay switches (if any)
	if hasConnection(objModel, api.ConnectionNamePhaseStraightRelay) {
		if _, err := f.createSwitch(objModel, "straight_relay_", api.ConnectionNamePhaseStraightRelay, 0); err != nil {
			return err
		}
	}
	if hasConnection(objModel, api.ConnectionNamePhaseOffRelay) {
		if _, err := f.createSwitch(objModel, "off_relay_", api.ConnectionNamePhaseOffRelay, 0); err != nil {
			return err
		}
	}

	return nil
}
