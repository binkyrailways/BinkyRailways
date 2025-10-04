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
	"fmt"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Create an output for the connection with given name of the given object and add it to the given DeviceFile.
func (f *DeviceFile) createOutput(
	objModel model.BinkyNetObject, namePrefix string,
	connName api.ConnectionName, pinsIndex int) (*Output, error) {

	// Create basic output
	output := Output{}
	output.Id = name(namePrefix + string(objModel.GetObjectID()))
	// Note that Output's have no name
	output.StateTopic = objModel.GetMQTTStateTopic(connName)
	output.CommandTopic = objModel.GetMQTTCommandTopic(connName)

	conn, err := getConnection(objModel, connName)
	if err != nil {
		return nil, err
	}
	pin, err := getPin(objModel, conn, pinsIndex)
	if err != nil {
		return nil, err
	}
	output.Channel = fmt.Sprintf("%d", pin.GetIndex()-1)
	if err := output.applyPlatformConfiguration(f, objModel, pin); err != nil {
		return nil, err
	}
	f.Outputs = append(f.Outputs, output)

	return &f.Outputs[len(f.Outputs)-1], nil
}

// Apply platform specific configuration on this switch
func (output *Output) applyPlatformConfiguration(f *DeviceFile, objModel model.BinkyNetObject, pin model.BinkyNetDevicePin) error {
	if platform, ok := f.platforms[pin.GetDeviceID()]; !ok {
		return fmt.Errorf("Platform not found for device with ID '%s' in %s", pin.GetDeviceID(), objModel.GetDescription())
	} else {
		platform.configureOutput(output)
	}
	return nil
}
