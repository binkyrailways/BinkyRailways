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
	"github.com/binkyrailways/BinkyRailways/pkg/core/ptr"
)

// Create a number for the connection with given name of the given object and add it to the given DeviceFile.
func (platform *devicePlatform) createNumber(
	objModel model.BinkyNetObject, namePrefix string) (*Number, error) {

	// Create basic sensor
	number := Number{}
	number.Platform = "template"
	number.Id = name(namePrefix + string(objModel.GetObjectID()))
	number.Name = name(namePrefix + string(objModel.GetObjectID()))
	number.MinValue = ptr.To(-100)
	number.MaxValue = ptr.To(100)
	number.Step = ptr.To(1)
	//number.StateTopic = objModel.GetMQTTStateTopic(api.ConnectionNameServo)
	number.CommandTopic = objModel.GetMQTTCommandTopic(api.ConnectionNameServo)

	// Add to file
	f := platform.deviceFile
	f.Numbers = append(f.Numbers, number)

	return &f.Numbers[len(f.Numbers)-1], nil
}
