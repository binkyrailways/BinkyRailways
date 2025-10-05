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
func (fs *DeviceFileSet) createOutput(
	objModel model.BinkyNetObject, namePrefix string,
	connName api.ConnectionName, pinsIndex int) (*Output, *devicePlatform, error) {

	// Get connection & pin
	conn, err := getConnection(objModel, connName)
	if err != nil {
		return nil, nil, err
	}
	pin, err := getPin(objModel, conn, pinsIndex)
	if err != nil {
		return nil, nil, err
	}

	// Find platform
	platform, err := fs.getPlatform(pin)
	if err != nil {
		return nil, nil, err
	}

	// Create basic output
	output := Output{}
	output.Id = model.NormalizeName(namePrefix + string(objModel.GetObjectID()))
	// Note that Output's have no name
	output.StateTopic = objModel.GetMQTTStateTopic(connName)
	output.CommandTopic = objModel.GetMQTTCommandTopic(connName)
	output.Channel = fmt.Sprintf("%d", pin.GetIndex()-1)
	platform.ConfigureOutput(&output)

	// Add to file
	f := platform.deviceFile
	f.Outputs = append(f.Outputs, output)

	return &f.Outputs[len(f.Outputs)-1], platform, nil
}
