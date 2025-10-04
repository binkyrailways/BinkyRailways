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

// Create a servo for the connection with given name of the given object and add it to the given DeviceFile.
func (f *DeviceFile) createServo(
	objModel model.BinkyNetObject, namePrefix string,
	connName api.ConnectionName, pinsIndex int) (*Servo, error) {

	// Create basic servo
	servo := Servo{}
	servo.Id = name(namePrefix + string(objModel.GetObjectID()))
	// Note that Servo's have no name
	servo.AutoDetachTime = "4s"
	servo.TransitionLength = "3s"
	servo.MinLevel = "0%"
	servo.MaxLevel = "100%"
	servo.IdleLevel = "50%"
	f.Servos = append(f.Servos, servo)

	return &f.Servos[len(f.Servos)-1], nil
}
